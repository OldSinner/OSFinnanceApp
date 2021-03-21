using FinnanceApp.Shared.Models;
using FinnanceApp.Shared.Models.ChartModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class ChartService : IChartService
    {
        private readonly HttpClient _http;

        public ChartService(HttpClient http)
        {
            _http = http;
        }

        public IList<ChartModel> chartMonths { get; set; } = new List<ChartModel>();
        public IList<ChartModel> chartPerson { get; set; } = new List<ChartModel>();
        public IList<ChartModel> chartCategory { get; set; } = new List<ChartModel>();
        public IList<ChartModel> chartShop { get; set; } = new List<ChartModel>();

        public async Task GetCategoryChart()
        {
            chartCategory.Clear();
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartModel>>>("api/Chart/category");
            if (response.isSuccess)
            {
                chartCategory = response.Data;
            }
        }

        public async Task GetMonthChart()
        {
            chartMonths.Clear();
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartModel>>>("api/Chart/month");
            if (response.isSuccess)
            {
                response.Data.Reverse();
                chartMonths = response.Data;
            }
        }

        public async Task GetPersonChart()
        {
            chartPerson.Clear();
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartModel>>>("api/Chart/person");
            if (response.isSuccess)
            {
                chartPerson = response.Data;
            }
        }

        public async Task GetShopChart()
        {
            chartShop.Clear();
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartModel>>>("api/Chart/shop");
            if (response.isSuccess)
            {
                chartShop = response.Data;
            }
        }
    }
}