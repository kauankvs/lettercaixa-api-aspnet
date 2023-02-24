using Newtonsoft.Json;

namespace LettercaixaAPI.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "id")]
        public int MovieId { get; set; }
        public string Title { get; set; }

        [JsonProperty(PropertyName = "poster_path")]
        public string? PosterPath { get; set; }
    }
}
