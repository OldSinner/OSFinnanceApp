using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponse<string>> activate(string key)
        {

            var result = await _http.PostAsJsonAsync("api/user/activate", key);
            Console.WriteLine("Sended");
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/user/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }

        public async Task<ServiceResponse<int>> register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/user/registeruser", request);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

    }
}
