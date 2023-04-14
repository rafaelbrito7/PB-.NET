using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Entities
{
    public class UsersFollowers
    {
        [Key]
        public Guid Id { get; set; }
        
        [JsonPropertyName("userFollowedId")]
        public Guid UserFollowedId { get; set; }

        [JsonPropertyName("userFollowed")]
        public User? UserFollowed { get; set; } 

        [JsonPropertyName("userFollowerId")]
        public Guid UserFollowerId { get; set; }

        [JsonPropertyName("userFollower")]
        public User? UserFollower { get; set; }
    }
}

