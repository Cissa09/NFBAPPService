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
