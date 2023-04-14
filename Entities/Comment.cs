using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Comment
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("user")]
        public virtual User? User { get; set; }

        [JsonPropertyName("postId")]
        public Guid PostId { get; set; }

        [JsonPropertyName("post")]
        public virtual Post? Post { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        public Comment()
        {

        }

        public Comment(Guid _Id, Guid _UserId, Guid _PostId, string _Content)
        {
            Id = _Id;
            UserId = _UserId;
            PostId = _PostId;
            Content = _Content;
        }
    }
}