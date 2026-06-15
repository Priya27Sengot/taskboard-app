namespace TaskboardAPI.Models
{
    public class APIResponse
    {
        public bool Success { get; set; }
        public string Description { get; set; }
        public dynamic Data {  get; set; }
    }
    
}
