using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> register(UserRegister request);
        Task<ServiceResponse<string>> login(UserLogin request);
        Task<ServiceResponse<string>> activate(string key);

        Task<User> getUser();
        Task<ServiceResponse<int>> EditProfile(EditProfile profile);

    }
}
