using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Server.Services.AdminService
{
    public interface IAdminService
    {
         Task<ServiceResponse<List<User>>> GetUserList();
    }
}