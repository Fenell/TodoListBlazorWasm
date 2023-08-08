using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.ViewModel
{
	public class Response<T>
	{
		public bool IsSuccess { get; set; }
		public string Message{ get; set; }
		public T Data { get; set; }
	}
}
