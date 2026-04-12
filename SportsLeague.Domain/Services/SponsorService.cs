using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _sponsorRepository;
        private readonly IGenericRepository<TournamentSponsor> _tournamentSponsorRepository;
        private readonly ILogger<SponsorService> _logger;

        public SponsorService(
            ISponsorRepository sponsorRepository,
            IGenericRepository<TournamentSponsor> tournamentSponsorRepository,
            ILogger<SponsorService> logger)
        {
            _sponsorRepository = sponsorRepository;
            _tournamentSponsorRepository = tournamentSponsorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Sponsor>> GetAllAsync()
        {
            _logger.LogInformation("Retrieving all sponsors");
            return await _sponsorRepository.GetAllAsync();
        }

        public async Task<Sponsor?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving sponsor with ID: {SponsorId}", id);
            var sponsor = await _sponsorRepository.GetByIdAsync(id);
            if (sponsor == null)
                _logger.LogWarning("Sponsor with ID {SponsorId} not found", id);
            return sponsor;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {

            var existingSponsors = await _sponsorRepository.FindAsync(s => s.Name.ToLower() == sponsor.Name.ToLower());

            if (existingSponsors.Any())
            {
                _logger.LogWarning("Attempted to create a sponsor with a duplicate name: {Name}", sponsor.Name);
                throw new Exception("Ya existe un patrocinador con ese nombre.");
            }

            _logger.LogInformation("Creating sponsor: {Name}", sponsor.Name);

            return await _sponsorRepository.CreateAsync(sponsor);
        }

        public async Task UpdateAsync(int id, Sponsor sponsor)
        {
            var existing = await _sponsorRepository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Update failed: Sponsor with ID {SponsorId} not found", id);
                throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");
            }

            existing.Name = sponsor.Name;
            existing.Company = sponsor.Company;
            existing.ContactPhone = sponsor.ContactPhone;
            existing.WebsiteUrl = sponsor.WebsiteUrl;
            existing.Category = sponsor.Category;

            _logger.LogInformation("Updating sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var exists = await _sponsorRepository.ExistsAsync(id);
            if (!exists)
            {
                _logger.LogWarning("Delete failed: Sponsor with ID {SponsorId} not found", id);
                throw new KeyNotFoundException($"No se encontró el patrocinador con ID {id}");
            }

            _logger.LogInformation("Deleting sponsor with ID: {SponsorId}", id);
            await _sponsorRepository.DeleteAsync(id);
        }

        public async Task<TournamentSponsor> AddSponsorToTournamentAsync(TournamentSponsor relation)
        {
            _logger.LogInformation("Linking sponsor {SponsorId} to tournament {TournamentId}",
                relation.SponsorId, relation.TournamentId);

            return await _tournamentSponsorRepository.CreateAsync(relation);
        }
    }
}