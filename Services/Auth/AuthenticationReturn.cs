using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthenticationReturn
    {
        public bool Status { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }
    }
}
