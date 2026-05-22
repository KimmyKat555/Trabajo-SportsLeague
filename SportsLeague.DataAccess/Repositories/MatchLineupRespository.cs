using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class MatchLineupRepository : GenericRepository<MatchLineup>, IMatchLineupRepository
    {
        public MatchLineupRepository(LeagueDbContext context) : base(context) { }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId)
        {
            return await _dbSet.Where(ml => ml.MatchId == matchId).ToListAsync();
        }

        public async Task<IEnumerable<MatchLineup>> GetByTeamAsync(int matchId, int teamId)
        {
            // Nota: Asegúrate de incluir el Player si necesitas validar el TeamId
            return await _dbSet.Where(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId).ToListAsync();
        }

        public async Task<int> CountStartersAsync(int matchId, int teamId)
        {
            return await _dbSet.CountAsync(ml => ml.MatchId == matchId && ml.Player.TeamId == teamId && ml.IsStarter);
        }
    }
}
