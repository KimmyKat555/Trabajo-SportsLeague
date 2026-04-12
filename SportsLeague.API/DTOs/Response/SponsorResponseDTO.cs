namespace SportsLeague.API.DTOs.Response
{
    public class SponsorResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? WebsiteUrl { get; set; }
        public string Category { get; set; } 
        public DateTime CreatedAt { get; set; }
    }
}
