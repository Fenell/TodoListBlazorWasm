using TodoList.ViewModel;
using TodoList.ViewModel.Jobs;

namespace TodoListBlazorWasm.Services
{
    public interface IJobApiClient
	{
		Task<Response<PagedList<JobDto>>> GetAllJobs(JobListSearch jobSearch);

		Task<Response<JobDto>> GetJobById(string jobId);

		Task<Response<bool>> CreateJob(JobCreateRequest request);

		Task<Response<bool>> UpdateJob(string jobId, JobUpdateRequest request);

		Task<Response<bool>> UpdateAssignJob (Guid jobId, AssignJobUpdate request);

		Task<Response<bool>> DeleteJob(Guid jobId);
	}
}
