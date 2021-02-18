using System.Runtime.Intrinsics.X86;
using System.Net.Http;
using System.Net;
using System.Collections.Generic;
using FinnanceApp.Shared.Models;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace FinnanceApp.Client.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<Category> category { get; set; } = new List<Category>();

        public async Task GetCategory()
        {
            category.Clear();
            category = await _httpClient.GetFromJsonAsync<List<Category>>("/api/MontlyBill/Category");
            
        }
    }
}