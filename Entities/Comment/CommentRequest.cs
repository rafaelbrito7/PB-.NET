using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CommentRequest
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Content { get; set; }
    }
}
