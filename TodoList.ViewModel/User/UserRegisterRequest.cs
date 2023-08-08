using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoList.ViewModel.User
{
	public class UserRegisterRequest
	{
		[Display(Name = "Tên")]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "Họ")]
		[Required]
		public string LastName { get; set; }


		[Display(Name = "Số điện thoại")]
		[Required]
		public string PhoneNumber { get; set; }

		[Display(Name = "Tài khoản")]
		[Required]
		public string UserName { get; set; }

		[Display(Name = "Mật khẩu")]
		[Required]
		public string Password { get; set; }

		[Display(Name = "Xác nhận lại mật khẩu")]
		public string ComfirmPassword { get; set; }
	}
}
