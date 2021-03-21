using FinnanceApp.Shared.Models;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class BillService : IBillService
    {
        private readonly HttpClient _http;


        public BillService(HttpClient http)
        {
            _http = http;
        }
        public event Action OnChange;
        void BillsChanged() => OnChange.Invoke();

        public IList<Bills> bill { get; set; } = new List<Bills>();
        public IList<Bills> billWithPages { get; set; } = new List<Bills>();
        public BillService(int pages)
        {
            this.pages = pages;

        }
        public int pages { get; set; } = new int();


        public async Task<ServiceResponse<int>> AddBill(Bills billtoadd)
        {
            var result = await _http.PostAsJsonAsync<Bills>("api/Bill", billtoadd);

            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();


        }

        public async Task<ServiceResponse<int>> DeleteBill(int id, int page)
        {
            var result = await _http.DeleteAsync("api/Bill/" + id);
            billWithPages.Clear();
            await GetBillListWithPages(page);
            BillsChanged();
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task GetBillListWithPages(int page)
        {
            billWithPages.Clear();
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Bills>>>($"api/Bill?page={page}");
            billWithPages = result.Data;
            pages = Int16.Parse(result.Message);
        }
    }
}
