using LettercaixaAPI.Models;

namespace LettercaixaAPI.DTOs
{
    public class ProfileDisplay
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? ProfilePicture { get; set; }
        public DateTime? Birth { get; set; }
        public string Username { get; set; } = null!;
    }
}
