namespace Ecommerce_Project.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public int Role { get; set; }
        public bool IsActive { get; set; }
    }
}
