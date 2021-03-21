using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

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