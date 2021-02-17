using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class ShopService : IShopService
    {
        private readonly HttpClient _http;
        public ShopService(HttpClient http)
        {
            _http = http;
        }
        public event Action OnChange;
        void ShopsChanged() => OnChange.Invoke();
        public IList<Shops> Shops { get; set; } = new List<Shops>();
        public async Task GetShopList()
        {
            if (Shops.Count == 0)
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<Shops>>>("api/Shops");
                
                Shops = result.Data;
                
            }
        }
        public async Task<ServiceResponse<string>> DeleteShop(int id)
        {
            var result = await _http.DeleteAsync("api/Shops/"+ id);
            var x = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            Shops.Clear();
            await GetShopList();
            ShopsChanged();
            return x;
        }
        public async Task<ServiceResponse<int>> addShop(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = " ";
            var result = await _http.PostAsJsonAsync<string>("api/Shops", name);
            var x = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            Shops.Clear();
            await GetShopList();
            ShopsChanged();

            return x;
        }
        public async Task<ServiceResponse<int>> editShop(int id, string name)
        {
            Shops shop = new Shops { id = id, Owner = null, name = name };
            var result = await _http.PutAsJsonAsync<Shops>("api/Shops", shop);
            var x = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            Shops.Clear();
            await GetShopList();
            ShopsChanged();
            return x;
        }

    }
}
