using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;
using TodoList.ViewModel;
using TodoList.ViewModel.User;

namespace TodoList.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class LoginsController : ControllerBase
	{
		private readonly IUserReposititory _userReposititory;

		public LoginsController(IUserReposititory userReposititory)
		{
			_userReposititory = userReposititory;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			var result = await _userReposititory.LoginAsync(request);

			if (!result.IsSuccess)
				return BadRequest(new Response<string>() { IsSuccess = result.IsSuccess, Message = result.Message });

			return Ok(new Response<string>() { IsSuccess = true, Data = result.Data, Message = result.Message});
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
		{
			
			var result = await _userReposititory.RegisterAsync(request);

			if (!result.IsSuccess)
				return BadRequest(new Response<bool>() { IsSuccess = false, Message = result.Message });

			return Ok(new Response<UserRegisterRequest>() { IsSuccess = true, Data = result.Data });
		}
	}
}
