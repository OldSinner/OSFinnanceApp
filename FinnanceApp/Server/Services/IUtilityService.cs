using FinnanceApp.Shared.Models;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}
