using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Services
{
    public interface IMatchLineupService
    {
        Task<MatchLineup> AddAsync(int matchId, int playerId, bool isStarter, string position);
        Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId);
        Task<IEnumerable<MatchLineup>> GetByTeamAsync(int matchId, int teamId);
        Task DeleteAsync(int id);
    }
}
