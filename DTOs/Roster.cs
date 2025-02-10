using System.Text.Json.Serialization;

namespace NFB.DTOs
{
    public class Roster
    {
        [JsonPropertyName("roster_id")]
        public int RosterId { get; set; }

        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; }

        [JsonPropertyName("players")]
        public List<string> Players { get; set; } // IDs dos jogadores no time

        [JsonPropertyName("settings")]
        public RosterSettings Settings { get; set; }
    }

    public class Player
    {
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonPropertyName("team")]
        public string Team { get; set; }      
    }

    public class EnrichedRoster
    {
        public int RosterId { get; set; }
        public string OwnerId { get; set; }
        public RosterSettings Settings { get; set; }

        // Aqui os IDs são substituídos pelos detalhes completos dos jogadores
        public List<Player> Players { get; set; }
    }

    public class RosterSettings
    {
        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("fpts")]
        public double PointsFor { get; set; }
    }
}
