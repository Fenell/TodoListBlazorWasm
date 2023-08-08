using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using TodoList.ViewModel;
using TodoList.ViewModel.Jobs;

namespace TodoListBlazorWasm.Services
{
    public class JobApiClient : IJobApiClient
	{
		private readonly HttpClient _client;

		public JobApiClient(HttpClient client)
		{
			_client = client;
		}
		public async Task<Response<PagedList<JobDto>>> GetAllJobs(JobListSearch jobSearch)
		{
			//var uri = $"api/Jobs?name={jobSearch.Name}&asigneeId={jobSearch.AsigneeId}&priority={jobSearch.Priority}";

			//var result = await _client.GetFromJsonAsync<List<JobDto>>(uri);
			//var response = new Response<PagedList<JobDto>>();
			
			//if (result == null)
			//{
			//	response.IsSuccess = false;
			//	response.Message = "List empty";
			//}
			//else
			//{
			//	response.IsSuccess = true;
			//	response.Data = result;
			//}

			//return response;

			var queryStringParam = new Dictionary<string, string>()
			{
				["pageNumber"] = jobSearch.PageNumber.ToString()
			};
            if (!string.IsNullOrEmpty(jobSearch.Name))
                queryStringParam.Add("name", jobSearch.Name);

			if (jobSearch.AsigneeId.HasValue)
				queryStringParam.Add("asigneeId", jobSearch.AsigneeId.ToString());

			if (jobSearch.Priority.HasValue)
				queryStringParam.Add("priority", jobSearch.Priority.ToString());

            var url = QueryHelpers.AddQueryString("/api/jobs", queryStringParam);

            var resul = await _client.GetFromJsonAsync<PagedList<JobDto>>(url);

			return new Response<PagedList<JobDto>>()
			{
				IsSuccess = resul != null,
				Data = resul
			};
		}

		public async Task<Response<JobDto>> GetJobById(string jobId)
		{
			var result = await _client.GetFromJsonAsync<JobDto>($"api/Jobs/{jobId}");
			var response = new Response<JobDto>();

			if (result == null)
			{
				response.IsSuccess = false;
				response.Message = "Cannot find job with id:" + jobId;
			}
			else
			{
				response.Data = result;
				response.IsSuccess = true;
			}

			return response;
		}
		
		public async Task<Response<bool>> CreateJob(JobCreateRequest request)
		{
			var result = await _client.PostAsJsonAsync("api/jobs/", request);

			return new Response<bool>()
			{
				IsSuccess = true,
				Message = "Create success"
			};
		}
			
		public async Task<Response<bool>> UpdateJob(string jobId, JobUpdateRequest request)
		{
			var result = await _client.PutAsJsonAsync($"api/jobs/{jobId}", request);

			return new Response<bool>()
			{
				IsSuccess = true,
				Message = "Update success"
			};
		}

		public async Task<Response<bool>> UpdateAssignJob(Guid jobId, AssignJobUpdate request)
		{
			var result = await _client.PutAsJsonAsync($"api/jobs/update-assign-job/{jobId}", request);

			if (result.IsSuccessStatusCode)
			{
				return new Response<bool>() { IsSuccess = true };
			}

			return new Response<bool>() { IsSuccess = false };
		}

		public async Task<Response<bool>> DeleteJob(Guid jobId)
		{
			var result = await _client.DeleteAsync($"api/Jobs/{jobId}");

			return new Response<bool>()
			{
				IsSuccess = result.IsSuccessStatusCode,
			};
		}
	}
}
