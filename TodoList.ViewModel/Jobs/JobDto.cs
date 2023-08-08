using TodoList.ViewModel.Enums;

namespace TodoList.ViewModel.Jobs
{
    public class JobDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AssigneeName { get; set; }
        public DateTime CreatedDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }
}