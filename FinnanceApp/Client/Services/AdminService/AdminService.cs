using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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
        public async Task<ServiceResponse<string>> ActivateUser(int id)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/admin/activateuser", id);
            return await responseMessage.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }
        public async Task<ServiceResponse<string>> GrantAdmin(int id)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/admin/GrantAdmin", id);
            return await responseMessage.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }
        public async Task<ServiceResponse<string>> Deactive(int id)
        {
            var responseMessage = await _http.PostAsJsonAsync("api/admin/DeactiveUser", id);
            return await responseMessage.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }
    }
}