using System.Text.Json.Serialization;

namespace FeedHub.Domain.Models
{
    public class FeedItem
    {
        public int Id { get; set; }
        public int FeedId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime? PublishAt { get; set; }

        [JsonIgnore]
        public Feed Feed { get; set; }
    }
}
