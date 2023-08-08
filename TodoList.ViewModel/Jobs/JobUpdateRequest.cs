using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModel.Enums;

namespace TodoList.ViewModel.Jobs
{
    public class JobUpdateRequest
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }
        public Guid? AssigneeeId { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}
