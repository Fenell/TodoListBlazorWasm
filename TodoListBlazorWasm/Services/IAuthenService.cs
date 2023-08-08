using TodoList.ViewModel;
using TodoList.ViewModel.User;

namespace TodoListBlazorWasm.Services
{
	public interface IAuthenService
	{
		Task<Response<string>> Login(LoginRequest request);

		Task Logout();
	}
}
