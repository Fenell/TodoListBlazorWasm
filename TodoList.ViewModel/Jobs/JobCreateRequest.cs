using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModel.Enums;

namespace TodoList.ViewModel.Jobs
{
    public class JobCreateRequest
    {
        [Required(ErrorMessage = "Không được để trống tên")]
        [MaxLength(100, ErrorMessage = "Không quá 100 ký tự")]
        public string Name { get; set; }
        public Guid? AssigneeeId { get; set; }

        [Required(ErrorMessage = "Không được để trống độ ưu tiên")]
        public Priority? Priority { get; set; }
    }
}
