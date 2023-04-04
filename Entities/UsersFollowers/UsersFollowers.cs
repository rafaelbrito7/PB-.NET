using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UsersFollowers
    {
        public Guid UserId { get; set; }
        public User UserOrFollower { get; set; }
        public Guid FollowerId { get; set; }
    }
}

