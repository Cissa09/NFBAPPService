using System.Text.Json.Serialization;

namespace NFB.DTOs
{
    public class User
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("avatar")]
        public string AvatarUrl { get; set; }
    }
}
