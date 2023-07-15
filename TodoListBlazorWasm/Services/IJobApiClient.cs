using TodoList.ViewModel;

namespace TodoListBlazorWasm.Services
{
	public interface IJobApiClient
	{
		Task<List<JobDto>> GetAllJobs();
	}
}
