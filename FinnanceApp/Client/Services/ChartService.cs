using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;
using FinnanceApp.Shared.Models.ChartModels;

namespace FinnanceApp.Client.Services
{
    public class ChartService : IChartService
    {
        private readonly HttpClient _http;

        public ChartService(HttpClient http)
        {
            _http = http;
        }

        public IList<ChartMonth> chartMonths  {get;set;} = new List<ChartMonth>();
        public IList<ChartMonth> chartPerson  {get;set;} = new List<ChartMonth>();

        public async Task GetMonthChart()
        {
            chartMonths.Clear();
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartMonth>>>("api/Chart/month");
            if (response.isSuccess)
            {
                response.Data.Reverse();
                chartMonths = response.Data;
            }
        }

        public async Task GetPersonChart()
        {
            chartPerson.Clear();
             var response = await _http.GetFromJsonAsync<ServiceResponse<List<ChartMonth>>>("api/Chart/person");
            if (response.isSuccess)
            {
                response.Data.Reverse();
                chartPerson = response.Data;
            }
        }
    }
}