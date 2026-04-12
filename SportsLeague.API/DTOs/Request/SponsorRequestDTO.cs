namespace SportsLeague.API.DTOs.Request
{
    public class SponsorRequestDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? WebsiteUrl { get; set; }
        public int Category { get; set; } 
    }
}
