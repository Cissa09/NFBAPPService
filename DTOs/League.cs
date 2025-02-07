using System.Text.Json.Serialization;

namespace NFB.DTOs
{
    public class League
    {
        [JsonPropertyName("league_id")] // Nome correto do campo na API
        public string LeagueId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("season")]
        public int Season { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }        
    }
}
