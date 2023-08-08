using TodoList.ViewModel;

namespace TodoListBlazorWasm.Services
{
	public interface IUserApiClient
	{
		Task<List<UserDto>> GetUserList();
	}
}
