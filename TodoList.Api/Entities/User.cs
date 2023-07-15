using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TodoList.Api.Entities
{
	public class User:IdentityUser<Guid>
	{
		[Required, MaxLength(100)]
		public string FirstName { get; set; }
		[Required, MaxLength(100)]
		public string LastName{ get; set; }

		public virtual List<Job>? Jobs{ get; set; }
	}
}
