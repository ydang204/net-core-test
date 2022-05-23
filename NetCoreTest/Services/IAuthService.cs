using NetCoreTest.Entities;
using NetCoreTest.ReqModels;

namespace NetCoreTest.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);

        Task VerifyAccountAsync(string token);
    }
}