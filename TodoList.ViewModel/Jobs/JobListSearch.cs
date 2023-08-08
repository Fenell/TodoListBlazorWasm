using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.ViewModel.Enums;
using TodoList.ViewModel.SeedWork;

namespace TodoList.ViewModel.Jobs
{
    public class JobListSearch : PagingParameters
    {
        public string Name { get; set; }
        public Guid? AsigneeId { get; set; }
        public Priority? Priority { get; set; }
    }
}
