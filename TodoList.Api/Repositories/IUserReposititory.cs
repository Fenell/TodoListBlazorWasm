using TodoList.ViewModel;
using TodoList.ViewModel.User;

namespace TodoList.Api.Repositories
{
    public interface IUserReposititory
	{
		Task<Response<string>> LoginAsync(LoginRequest request);

		Task<Response<UserRegisterRequest>> RegisterAsync(UserRegisterRequest request);
		Task<List<UserDto>> GetUserList();
	}
}
