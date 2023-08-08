using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TodoList.Api.Entities;
using TodoList.Api.Extenstions;
using Job = TodoList.Api.Entities.Job;

namespace TodoList.Api.Data
{
	public class TodoListDbContext : IdentityDbContext<User, Role, Guid>
	{
		protected TodoListDbContext()
		{
		}

		public TodoListDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<IdentityUserLogin<Guid>>().HasKey(a => a.UserId);
			builder.Entity<IdentityUserRole<Guid>>().HasKey(x => new { x.RoleId, x.UserId });
			builder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);
			builder.Seed();
		}

		public DbSet<Job> Jobs { get; set; }

	}
}
