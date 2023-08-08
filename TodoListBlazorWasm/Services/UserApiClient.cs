using System.Net.Http.Json;
using TodoList.ViewModel;

namespace TodoListBlazorWasm.Services
{
    public class UserApiClient : IUserApiClient
    {
	    private readonly HttpClient _client;

	    public UserApiClient(HttpClient client)
	    {
            _client = client;
	    }
        public async Task<List<UserDto>> GetUserList()
        {
	        var result = await _client.GetFromJsonAsync<List<UserDto>>("api/Users");

	        return result;
        }
    }
}
