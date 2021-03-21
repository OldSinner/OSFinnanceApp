using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.AdminService
{
    public interface IAdminService
    {
        Task<ServiceResponse<List<User>>> GetUserList();

        Task<ServiceResponse<string>> GrantAdmin(int id);

        Task<ServiceResponse<string>> ActivateUser(int id);

        Task<ServiceResponse<string>> SelectAsInactive(int id);
    }
}