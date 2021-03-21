using FinnanceApp.Shared.Models;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Data
{
    public interface IAuthRepo
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExist(string email);
        Task<ServiceResponse<string>> Login(string email, string passowrd, bool RememberMe);
        Task<ServiceResponse<string>> activatte(string key);

        Task<ServiceResponse<int>> EditProfile(EditProfile profile);

        Task<ServiceResponse<string>> DeleteUser(User user);

        Task DeleteInactiveUser();

    }
}
