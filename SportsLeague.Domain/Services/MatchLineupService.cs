using Microsoft.Extensions.Logging;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.Domain.Services
{
    public class MatchLineupService : IMatchLineupService
    {
        private readonly IMatchLineupRepository _lineupRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<MatchLineupService> _logger;

        public MatchLineupService(
            IMatchLineupRepository lineupRepository,
            IMatchRepository matchRepository,
            IPlayerRepository playerRepository,
            ILogger<MatchLineupService> logger)
        {
            _lineupRepository = lineupRepository;
            _matchRepository = matchRepository;
            _playerRepository = playerRepository;
            _logger = logger;
        }

        public async Task<MatchLineup> AddAsync(int matchId, int playerId, bool isStarter, string position)
        {
            var match = await _matchRepository.GetByIdAsync(matchId)
                ?? throw new KeyNotFoundException($"No se encontró el partido con ID {matchId}");

            if (match.Status != Domain.Enums.MatchStatus.Scheduled)
                throw new InvalidOperationException("Solo se pueden registrar alineaciones en partidos programados (0)");

            var player = await _playerRepository.GetByIdAsync(playerId)
                ?? throw new KeyNotFoundException($"No se encontró el jugador con ID {playerId}");

            if (player.TeamId != match.HomeTeamId && player.TeamId != match.AwayTeamId)
                throw new InvalidOperationException("El jugador no pertenece a ninguno de los equipos del partido");

            var existing = await _lineupRepository.GetByMatchAsync(matchId);
            if (existing.Any(l => l.PlayerId == playerId))
                throw new InvalidOperationException("El jugador ya está registrado en la alineación de este partido");

            if (isStarter)
            {
                int startersCount = await _lineupRepository.CountStartersAsync(matchId, player.TeamId);
                if (startersCount >= 11)
                    throw new InvalidOperationException("El equipo ya tiene 11 titulares registrados en este partido");
            }

            _logger.LogInformation("Registering player {PlayerId} in match {MatchId}", playerId, matchId);

            var lineup = new MatchLineup
            {
                MatchId = matchId,
                PlayerId = playerId,
                IsStarter = isStarter,
                Position = position
            };

            return await _lineupRepository.AddAsync(lineup);
        }

        public async Task<IEnumerable<MatchLineup>> GetByMatchAsync(int matchId) => await _lineupRepository.GetByMatchAsync(matchId);

        public async Task<IEnumerable<MatchLineup>> GetByTeamAsync(int matchId, int teamId) => await _lineupRepository.GetByTeamAsync(matchId, teamId);

        public async Task DeleteAsync(int id)
        {
            if (!(await _lineupRepository.ExistsAsync(id)))
                throw new KeyNotFoundException($"No se encontró la alineación con ID {id}");

            await _lineupRepository.DeleteAsync(id);
        }
    }
}