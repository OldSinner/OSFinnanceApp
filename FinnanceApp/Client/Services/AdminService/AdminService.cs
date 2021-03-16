using System.Net.Http;

using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;
using System.Net.Http.Json;

namespace FinnanceApp.Client.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly HttpClient _http;

        public IList<User> userList { get; set; } = new List<User>();
        public AdminService(HttpClient http)
        {
            _http = http;
        }
        public async Task GetUserList()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/admin/user");
            if (response.isSuccess)
            {
                userList.Clear();
                userList = response.Data;
            }

        }
    }
}