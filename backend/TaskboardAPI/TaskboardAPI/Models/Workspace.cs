namespace TaskboardAPI.Models
{
    public class Workspace
    {
        public int WorkspaceId { get; set; }
        public string WorkspaceName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate   { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

    }

    public class CreateWorkspace
    {
        public string WorkspaceName { get; set; }
        public string Description { get; set; }     

    }
    public class WorkspaceList
    {
        public int WorkspaceId { get; set; }
        public string WorkspaceName { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ProjectCount { get; set; }
      
    }
}
