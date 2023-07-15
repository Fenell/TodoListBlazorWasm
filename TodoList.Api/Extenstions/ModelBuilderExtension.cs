using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Entities;
using TodoList.ViewModel.Enums;

namespace TodoList.Api.Extenstions
{
	public static class ModelBuilderExtension
	{
		private static IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
		public static void Seed(this ModelBuilder modelBuilder)
		{
			var user = new User()
			{
				Id = Guid.NewGuid(),
				FirstName = "Nguyen Van",
				LastName = "A",
				Email = "nguyenA@gmail.com",
				PhoneNumber = "012345678",
				UserName = "admin",
			};
			user.PasswordHash = _passwordHasher.HashPassword(user, "admin@12345");
			modelBuilder.Entity<User>().HasData(user);

			modelBuilder.Entity<Job>().HasData(new Job()
			{
				Id = Guid.NewGuid(),
				Name = "Job 1",
				CreatedDate = DateTime.Now,
				Priority = Priority.High,
				Status = Status.Open,
				AssigneeeId = user.Id
			});
		}
	}
}
