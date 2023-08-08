using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TodoList.Api.Data;
using TodoList.Api.Entities;
using TodoList.ViewModel;
using TodoList.ViewModel.User;

namespace TodoList.Api.Repositories
{
	public class UserRepository : IUserReposititory
	{
		private readonly TodoListDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly SignInManager<User> _signInManager;
		private readonly UserManager<User> _userManager;

		public UserRepository(TodoListDbContext context, IConfiguration configuration, SignInManager<User> signInManager, UserManager<User> userManager)
		{
			_context = context;
			_configuration = configuration;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<Response<string>> LoginAsync(LoginRequest request)
		{
			var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

			if (!result.Succeeded)
				return new Response<string>()
				{
					IsSuccess = false,
					Message = "Tài khoản hoặc mật khẩu không chính xác"
				};

			return new Response<string>()
			{
				IsSuccess = true,
				Message = "Đăng nhập thành công",
				Data = GenerateToken(request)
			};
		}

		public async Task<Response<UserRegisterRequest>> RegisterAsync(UserRegisterRequest request)
		{
			var response = new Response<UserRegisterRequest>();

			var user = await _userManager.FindByNameAsync(request.UserName);

			if (user != null)
			{
				response.IsSuccess = false;
				response.Message = "Tài khoản đã tồn tại";
			}
			else
			{
				var userCreate = new User()
				{
					UserName = request.UserName,
					PhoneNumber = request.PhoneNumber,
					FirstName = request.FirstName,
					LastName = request.LastName,
				};

				var result = await _userManager.CreateAsync(userCreate, request.Password);

				if (!result.Succeeded)
				{
					response.IsSuccess = false;
					foreach (var identityError in result.Errors)
					{
						response.Message += identityError.Description + Environment.NewLine;
					}
				}
				else
				{
					response.IsSuccess = true;
					response.Data = request;
				}
			}

			return response;
		}

		public async Task<List<UserDto>> GetUserList()
		{
			var userDtos = await _context.Users.Select(a => new UserDto()
			{
				Id = a.Id,
				FullName = a.FirstName + " " + a.LastName,
			}).ToListAsync();

			return userDtos;
		}

		private string GenerateToken(LoginRequest request)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secretkey"]));
			var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var claims = new List<Claim>
			{
				new(ClaimTypes.Name, request.UserName)
			};

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: cred);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
