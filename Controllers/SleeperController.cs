using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using NFB.DTOs;
using NFB.Services;

namespace NFB.Controllers
{
    [ApiController]
    [Route("api/sleeper")]
    public class SleeperController : ControllerBase
    {
        private readonly SleeperService _sleeperService;     

        public SleeperController(SleeperService sleeperService)
        {
            _sleeperService = sleeperService;       
        }

        // Endpoint para buscar dados da liga
        [HttpGet("league/{leagueId}")]
        public async Task<IActionResult> GetLeague(string leagueId)
        {
            try
            {
                var league = await _sleeperService.GetLeagueAsync(leagueId);
                return Ok(league);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao acessar a API do Sleeper: {ex.Message}");
            }
        }

        // GET: api/rosters/enriched/{leagueId}
        [HttpGet("enriched/{leagueId}")]
        public async Task<ActionResult<List<EnrichedRoster>>> GetEnrichedRosters(string leagueId)
        {
            var enrichedRosters = await _sleeperService.GetEnrichedRostersAsync(leagueId);
            return Ok(enrichedRosters);
        }

        // Endpoint para buscar a classificação
        [HttpGet("standings/{leagueId}")]
        public async Task<IActionResult> GetStandings(string leagueId)
        {
            try
            {
                var standings = await _sleeperService.GetStandingsAsync(leagueId);
                return Ok(standings);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao buscar standings: {ex.Message}");
            }
        }

        [HttpGet("playoffs/{leagueId}")]
        public async Task<IActionResult> GetPlayoffStandings(string leagueId)
        {
            try
            {
                var result = await _sleeperService.GetPlayoffStandingsAsync(leagueId);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao buscar playoffs: {ex.Message}");
            }
        }
    }
}
