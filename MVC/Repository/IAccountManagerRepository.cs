using Microsoft.AspNetCore.Identity;
using static MVC.Repository.AccountManagerRepository;

namespace MVC
{
    public interface IAccountManagerRepository
    {
        Task<SignInResult> Login(string email, string password);
        Task Logout();
    }
}
