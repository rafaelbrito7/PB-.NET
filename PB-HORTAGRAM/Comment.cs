using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PB_HORTAGRAM
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("user_id")]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("post_id")]
        public Guid PostId { get; set; }
        public virtual Post Post { get; set; }

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