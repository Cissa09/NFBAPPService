using NFB.DTOs;
using System.Text.Json;

namespace NFB.Services
{
    public class SleeperService
    {
        private readonly HttpClient _httpClient;

        public SleeperService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.sleeper.app/v1/");
        }

        // Busca informações básicas da liga
        public async Task<League> GetLeagueAsync(string leagueId)
        {
            var response = await _httpClient.GetAsync($"league/{leagueId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<League>();
        }
       
        // Método auxiliar para buscar usuário
        public async Task<User> GetUserAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"user/{userId}");
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<User>();
        }

        public async Task<List<EnrichedRoster>> GetEnrichedRostersAsync(string leagueId)
        {
            // 1. Buscar os rosters da liga
            var rostersResponse = await _httpClient.GetAsync($"league/{leagueId}/rosters");
            rostersResponse.EnsureSuccessStatusCode();
            var rosters = await rostersResponse.Content.ReadFromJsonAsync<List<Roster>>();

            // 2. Buscar os dados dos jogadores a partir da API do Sleeper
            // Essa API retorna um dicionário onde a chave é o ID do jogador
            var playersResponse = await _httpClient.GetAsync("https://api.sleeper.app/v1/players/nfl");
            playersResponse.EnsureSuccessStatusCode();
            var playersDict = await playersResponse.Content.ReadFromJsonAsync<Dictionary<string, Player>>();

            // 3. Enriquecer cada roster mapeando os IDs para os dados completos dos jogadores
            var enrichedRosters = rosters.Select(r =>
            {
                var enrichedRoster = new EnrichedRoster
                {
                    RosterId = r.RosterId,
                    OwnerId = r.OwnerId,
                    Settings = r.Settings,
                    Players = r.Players
                        .Select(playerId => playersDict.TryGetValue(playerId, out var player) ? player : null)
                        .Where(player => player != null)
                        .ToList()
                };
                return enrichedRoster;
            }).ToList();

            return enrichedRosters;
        }

        public async Task<List<Standings>> GetStandingsAsync(string leagueId)
        {
            var rosters = await GetEnrichedRostersAsync(leagueId);
            var standings = new List<Standings>();

            foreach (var roster in rosters)
            {
                var standing = new Standings
                {
                    RosterId = roster.RosterId,
                    OwnerId = roster.OwnerId,
                    Wins = roster.Settings.Wins,
                    Losses = roster.Settings.Losses,
                    PointsFor = roster.Settings.PointsFor
                };

                // Busca o nome do dono
                if (!string.IsNullOrEmpty(roster.OwnerId))
                {
                    var user = await GetUserAsync(roster.OwnerId);
                    standing.OwnerName = user?.DisplayName ?? "Sem dono";
                }

                standings.Add(standing);
            }

            // Ordena e atribui posições
            var sortedStandings = standings
                .OrderByDescending(s => s.Wins)
                .ThenByDescending(s => s.PointsFor)
                .ToList();

            for (int i = 0; i < sortedStandings.Count; i++)
            {
                sortedStandings[i].StandingPosition = i + 1;
            }

            return sortedStandings;
        }

        public async Task<List<PlayoffMatch>> GetPlayoffBracketAsync(string leagueId)
        {
            var response = await _httpClient.GetAsync($"league/{leagueId}/winners_bracket");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json); // 👈 Log do JSON bruto
            return await response.Content.ReadFromJsonAsync<List<PlayoffMatch>>();
        }

        public async Task<PlayoffResult> GetPlayoffStandingsAsync(string leagueId)
        {
            var bracket = await GetPlayoffBracketAsync(leagueId);

            // Passo 1: Encontrar a partida do CHAMPIONSHIP
            var championshipMatch = bracket.FirstOrDefault(m =>
                m.Round == 3 // Rodada final (Finals)
                && m.Team1RosterId == 1 && m.Team2RosterId == 11 // IDs dos participantes esperados
            );

            // Se não encontrar, use a última partida da rodada 3
            if (championshipMatch == null)
            {
                var finalMatches = bracket.Where(m => m.Round == 3).ToList();
                championshipMatch = finalMatches.LastOrDefault();
            }

            if (championshipMatch == null)
                return null;

            // Passo 2: Campeão e vice
            var championRosterId = championshipMatch.WinnerRosterId;
            var viceChampionRosterId = (championRosterId == championshipMatch.Team1RosterId)
                ? championshipMatch.Team2RosterId
                : championshipMatch.Team1RosterId;

            // Passo 3: Buscar dados dos managers
            var rosters = await GetEnrichedRostersAsync(leagueId);
            var championRoster = rosters.FirstOrDefault(r => r.RosterId == championRosterId);
            var viceRoster = rosters.FirstOrDefault(r => r.RosterId == viceChampionRosterId);

            var championOwner = await GetUserAsync(championRoster?.OwnerId);
            var viceOwner = await GetUserAsync(viceRoster?.OwnerId);

            // Passo 4: Montar resultado
            var standings = new List<PlayoffStanding>
            {
                new PlayoffStanding
                {
                    RosterId = championRosterId,
                    OwnerName = championOwner?.DisplayName ?? "Sem dono",
                    Position = 1
                },
                new PlayoffStanding
                {
                    RosterId = viceChampionRosterId,
                    OwnerName = viceOwner?.DisplayName ?? "Sem dono",
                    Position = 2
                }
            };

            return new PlayoffResult
            {
                Champion = standings[0],
                Standings = standings
            };
        }
    }
}
