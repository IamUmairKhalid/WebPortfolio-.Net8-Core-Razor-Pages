namespace ResumeWebApp.Models
{
    public class EmailOtp
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string Purpose { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
    }
}
