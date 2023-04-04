using Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAuthService
    {
        IUserService UserService { get; set; }

        Task<AuthenticationReturn> Authenticate(string email, string password);
    }
}
