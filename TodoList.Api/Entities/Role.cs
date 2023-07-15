using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoList.Api.Entities
{
	public class Role:IdentityRole<Guid>
	{
		[MaxLength(200)]
		public string Description { get; set; }
	}
}
