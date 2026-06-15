namespace TaskboardAPI.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class RegisterUser
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class UserDetails
    {
        public string? Token{ get; set; }
        public int? UserId { get; set; }

        public string? Username { get; set; }
    }
    public class Status
    {
        public int Id { get; set; }
        public int code { get; set; }
        public string description { get; set; }
    }

    public class TaskDropdowns
    {
        public List<Project>? projects { get; set; }
        public List<Members>? users { get; set; }

        public List<Status>? status { get; set; }
    }
    public class Members
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }

    }

    
}
