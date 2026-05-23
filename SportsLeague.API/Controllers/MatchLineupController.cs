using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsLeague.API.DTOs.Request;
using SportsLeague.API.DTOs.Response;
using SportsLeague.Domain.Interfaces.Services;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchLineupController : ControllerBase
    {
        private readonly IMatchLineupService _lineupService;
        private readonly IMapper _mapper;

        public MatchLineupController(IMatchLineupService lineupService, IMapper mapper)
        {
            _lineupService = lineupService;
            _mapper = mapper;
        }

        [HttpPost("match/{matchId}")]
        public async Task<ActionResult<DTOs.Response.MatchLineupDTO>> Add(int matchId, CreateMatchLineupDTO dto)
        {
            try
            {
                var created = await _lineupService.AddAsync(matchId, dto.PlayerId, dto.IsStarter, dto.Position);

                var responseDto = _mapper.Map<MatchLineupDTO>(created);
                return Ok(responseDto);
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
            catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
        }

        [HttpGet("match/{matchId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> GetByMatch(int matchId)
        {
            var lineups = await _lineupService.GetByMatchAsync(matchId);
            return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
        }

        [HttpGet("match/{matchId}/team/{teamId}")]
        public async Task<ActionResult<IEnumerable<MatchLineupDTO>>> GetByTeam(int matchId, int teamId)
        {
            var lineups = await _lineupService.GetByTeamAsync(matchId, teamId);
            return Ok(_mapper.Map<IEnumerable<MatchLineupDTO>>(lineups));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _lineupService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        }
    }
}
