using TodoList.Api.Entities;
using TodoList.ViewModel;

namespace TodoList.Api.Repositories
{
	public interface IJobRepository
	{
		Task<List<JobDto>> GetListAsync();

		Task<JobDto> GetByIdAsync(Guid id);

		Task<Job> CreateAsync (JobCreateRequest request);

		Task<Job> UpdateAsync (JobUpdateRequest request);

		Task<Job> DeleteAsync (Guid id);
	}
}
