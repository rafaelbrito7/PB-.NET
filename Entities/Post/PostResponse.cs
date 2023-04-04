using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{ 
    public class PostResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public List<CommentResponse> Comments { get; set; }
    }
}
