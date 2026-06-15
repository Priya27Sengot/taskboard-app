namespace TaskboardAPI.Models
{
    public class Project
    {
        public int ProjectId  { get; set; }
        public string? ProjectTitle { get; set; }
        public string? Description { get; set; }
        public int WorkspaceId  { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class CreateProjectRequest
    {
        public string? ProjectTitle { get; set; }
        public string? Description { get; set; }
        public int WorkspaceId { get; set; }      

    }

    public class DeleteProjectRequest
    {
        public int ProjectId { get; set; }
        public int WorkspaceId { get; set; }
    }

    public class ProjectList
    {
        public int ProjectId { get; set; }
        public string? ProjectTitle { get; set; }
        public string? Description { get; set; }
        public int WorkspaceId { get; set; }
        public string? WorkspaceName { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
