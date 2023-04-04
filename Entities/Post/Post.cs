using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Entities
{
    public class Post
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [ForeignKey("user_id")]
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("user")]
        public virtual User User { get; set; }

        [JsonPropertyName("photoUrl")]
        public string PhotoUrl { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public Post()
        {

        }

        public Post(string _PhotoUrl, string _Description)
        {
            PhotoUrl = _PhotoUrl;
            Description = _Description;
        }

        public Post(Guid _Id, string _PhotoUrl, string _Description)
        {
            Id = _Id;
            PhotoUrl = _PhotoUrl;
            Description = _Description;
        }

        public Post(Guid _Id, Guid _UserId, string _PhotoUrl, string _Description)
        {
            Id = _Id;
            UserId = _UserId;
            PhotoUrl = _PhotoUrl;
            Description = _Description;
        }
    }
}
