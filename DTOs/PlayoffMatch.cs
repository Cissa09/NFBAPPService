using System.Text.Json;
using System.Text.Json.Serialization;

namespace NFB.DTOs
{
    public class PlayoffMatch
    {
        [JsonPropertyName("m")]
        public int MatchupId { get; set; }

        [JsonPropertyName("r")]
        public int Round { get; set; }

        [JsonPropertyName("w")]
        public int WinnerRosterId { get; set; }

        [JsonPropertyName("t1")]
        public int Team1RosterId { get; set; }

        [JsonPropertyName("t2")]
        public int Team2RosterId { get; set; }

        [JsonPropertyName("p")]
        public int? PhaseNumber { get; set; } // Agora é um número (ex: 7, 3)
    }

    public class PlayoffParticipant
    {
        [JsonPropertyName("roster_id")]
        public int RosterId { get; set; }
    }

    public class PlayoffResult
    {
        public PlayoffStanding Champion { get; set; }
        public List<PlayoffStanding> Standings { get; set; }
    }

    public class PlayoffStanding
    {
        public int RosterId { get; set; }
        public string OwnerName { get; set; }
        public int Position { get; set; }
    }
}
