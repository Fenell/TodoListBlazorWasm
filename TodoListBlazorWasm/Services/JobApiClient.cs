using System.Net.Http.Json;
using TodoList.ViewModel;

namespace TodoListBlazorWasm.Services
{
	public class JobApiClient:IJobApiClient
	{
		private readonly HttpClient _client;

		public JobApiClient(HttpClient client)
		{
			_client = client;
		}
		public async Task<List<JobDto>> GetAllJobs()
		{
			var result = await _client.GetFromJsonAsync<List<JobDto>>("api/Jobs");

			return result;
		}
	}
}
