
namespace TaskboardAPI.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public int AssignedToUser { get; set; }
        public bool IsActive  { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }

    public class CreateTaskRequest
    {
        public string TaskName { get; set; }
        public int ProjectId { get; set; }
        public int AssignedToUser { get; set; }
        public bool IsActive { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }
    }
    public class TaskList
    {
        public int TaskId { get; set; }
        public string? TaskName { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectTitle { get; set; }
        public string? Status { get; set; }
        public string? UserName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DueDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
