using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModel.Enums;

namespace TodoList.ViewModel
{
	public class JobUpdateRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid? AssigneeeId { get; set; }
		public Priority Priority { get; set; }
		public Status Status { get; set; }
	}
}
