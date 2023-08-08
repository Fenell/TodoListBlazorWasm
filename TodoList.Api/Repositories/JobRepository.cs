using Microsoft.EntityFrameworkCore;
using TodoList.Api.Data;
using TodoList.Api.Entities;
using TodoList.ViewModel;
using TodoList.ViewModel.Enums;
using TodoList.ViewModel.Jobs;
using TodoListBlazorWasm;

namespace TodoList.Api.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly TodoListDbContext _context;

        public JobRepository(TodoListDbContext context)
        {
            _context = context;
        }
        public async Task<PagedList<JobDto>> GetListAsync(JobListSearch jobSearch)
        {
            //Use Include
            //var jobs = await _context.Jobs.Include(a => a.User).Select(c => new JobDto()
            //{
            //	Id = c.Id,
            //	Name = c.Name,
            //	CreatedDate = c.CreatedDate,
            //	Priority = c.Priority,
            //	Status = c.Status,
            //	AssigneeName = $"{c.User.FirstName} {c.User.LastName}",
            //}).ToListAsync();

            //Use Left Join
            var query = from j in _context.Jobs
                        join u in _context.Users on j.AssigneeeId equals u.Id into ju
                        from user in ju.DefaultIfEmpty()
                        select new { j, user };

            if (!string.IsNullOrEmpty(jobSearch.Name))
            {
                query = query.Where(c => c.j.Name.Contains(jobSearch.Name));
            }

            if (jobSearch.AsigneeId.HasValue)
            {
                query = query.Where(c => c.user.Id == jobSearch.AsigneeId);
            }

            if (jobSearch.Priority.HasValue)
            {
                query = query.Where(c => c.j.Priority == jobSearch.Priority);
            }

            var count = await query.CountAsync();

            var data = await query.Skip((jobSearch.PageNumber - 1) * jobSearch.PageSize)
                .Take(jobSearch.PageSize).Select(a => new JobDto()
                {
                    Id = a.j.Id,
                    Name = a.j.Name,
                    CreatedDate = a.j.CreatedDate,
                    Priority = a.j.Priority,
                    Status = a.j.Status,
                    AssigneeName = a.user != null ? $"{a.user.FirstName} {a.user.LastName}" : "N/A"
                }).OrderByDescending(c => c.CreatedDate).ToListAsync();

            return new PagedList<JobDto>(data, count, jobSearch.PageSize, jobSearch.PageNumber);
        }

        public async Task<JobDto> GetByIdAsync(Guid id)
        {
            var job = await _context.Jobs.Include(a => a.User).Select(c => new JobDto()
            {
                Id = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                Priority = c.Priority,
                Status = c.Status,
                AssigneeName = c.User != null ? c.User.FirstName + " " + c.User.LastName : "N/A",
            }).FirstOrDefaultAsync(x => x.Id == id);

            return job;
        }

        public async Task<Job> CreateAsync(JobCreateRequest request)
        {
            var job = new Job()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedDate = DateTime.Now,
                Priority = request.Priority ?? Priority.Low,
                Status = Status.Open,
                AssigneeeId = request.AssigneeeId
            };
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return job;
        }

        public async Task<Job> UpdateAsync(JobUpdateRequest request)
        {
            var jobResult = await _context.Jobs.FindAsync(request.Id);
            jobResult.Name = request.Name;
            jobResult.Priority = request.Priority;
            jobResult.Status = request.Status;
            jobResult.AssigneeeId = request.AssigneeeId;

            _context.Jobs.Update(jobResult);
            await _context.SaveChangesAsync();
            return jobResult;
        }

        public async Task<bool> UpdateAssignJob(Guid id, AssignJobUpdate request)
        {
            try
            {
                var job = await _context.Jobs.FindAsync(id);

                if (job == null)
                    return false;

                job.AssigneeeId = request.UserId;
                _context.Jobs.Update(job);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Job> DeleteAsync(Guid id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return job;
        }
    }
}
