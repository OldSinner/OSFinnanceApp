using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services.AdminService
{
    public interface IAdminService
    {
        IList<User> userList { get; set; }

        Task GetUserList();
        Task<ServiceResponse<string>> ActivateUser(int id);
        Task<ServiceResponse<string>> GrantAdmin(int id);
        Task<ServiceResponse<string>> Deactive(int id);
    }
}