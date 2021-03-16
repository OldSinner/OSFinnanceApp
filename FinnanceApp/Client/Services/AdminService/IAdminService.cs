using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Client.Services.AdminService
{
    public interface IAdminService
    {
       IList<User> userList {get;set;}

       Task GetUserList();
    }
}