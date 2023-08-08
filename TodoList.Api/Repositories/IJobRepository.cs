using TodoList.Api.Entities;
using TodoList.ViewModel;
using TodoList.ViewModel.Jobs;
using TodoListBlazorWasm;

namespace TodoList.Api.Repositories
{
    public interface IJobRepository
	{
		Task<PagedList<JobDto>> GetListAsync(JobListSearch jobSearch);

		Task<JobDto> GetByIdAsync(Guid id);

		Task<Job> CreateAsync (JobCreateRequest request);

		Task<Job> UpdateAsync (JobUpdateRequest request);

		Task<bool> UpdateAssignJob(Guid id, AssignJobUpdate request);
		Task<Job> DeleteAsync (Guid id);
	}
}
