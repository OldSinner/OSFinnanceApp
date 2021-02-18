using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Client.Services.MonthlyService
{
    public class MonthlyService : IMonthlyService
    {
        private readonly HttpClient _http;

        public event Action OnChange;
        void MBillsChanged() => OnChange.Invoke();
        public IList<MontlyBills> montlyBills { get; set; } = new List<MontlyBills>();

        public MonthlyService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<string>> AddMontlyBill(MontlyBills montly)
        {
            var response = await _http.PostAsJsonAsync<MontlyBills>("api/MontlyBill", montly);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> DeleteMontlyBill(int id)
        {
            var response = await _http.DeleteAsync("api/MontlyBill/" + id);
            montlyBills.Clear();
            await GetMonthlyBills();
            MBillsChanged();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<string>> EditMontlyBill(MontlyBills montly)
        {
            var response = await _http.PutAsJsonAsync<MontlyBills>("api/MontlyBill", montly);
            montlyBills.Clear();
            await GetMonthlyBills();
            MBillsChanged();
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task GetMonthlyBills()
        {
            if (montlyBills.Count == 0)
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<MontlyBills>>>("api/MontlyBill");
                if (result.isSuccess)
                {
                    montlyBills.Clear();
                    montlyBills = result.Data;
                }
            }
        }


    }
}