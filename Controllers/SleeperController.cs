using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("rosters/{leagueId}")]
        public async Task<IActionResult> GetRosters(string leagueId)
        {
            try
            {
                var rosters = await _sleeperService.GetRostersAsync(leagueId);
                return Ok(rosters);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao buscar rosters: {ex.Message}");
            }
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
