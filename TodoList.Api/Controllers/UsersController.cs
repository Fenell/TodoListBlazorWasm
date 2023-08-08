using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Repositories;

namespace TodoList.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserReposititory _userReposititory;

		public UsersController(IUserReposititory userReposititory)
		{
			_userReposititory = userReposititory;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var userDtos = await _userReposititory.GetUserList();

			return Ok(userDtos);
		}
	}
}
