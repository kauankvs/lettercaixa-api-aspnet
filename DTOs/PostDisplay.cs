namespace LettercaixaAPI.DTOs
{
    public class PostDisplay
    {
        public int ProfileId { get; set; }
        public int MovieId { get; set; }
        public string Comment { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? ProfilePicture { get; set; }

    }
}
