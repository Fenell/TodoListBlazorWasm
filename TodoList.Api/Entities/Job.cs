using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TodoList.ViewModel.Enums;

namespace TodoList.Api.Entities
{
	public class Job
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid? AssigneeeId { get; set; }
		public DateTime CreatedDate { get; set; }
		public Priority Priority { get; set; }
		public Status Status { get; set; }

		[ForeignKey("AssigneeeId")]
		public virtual User User { get; set; }
	}
}
