
using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly HttpClient _http;

        public double monthSum { get; set; }
        public double weekSum { get; set; }
        public double diffSum { get; set; }
        public double targetSum { get; set; }
        public CardService(HttpClient http)
        {
            _http = http;
        }
        public async Task GetCards()
        {
            var result = await _http.GetFromJsonAsync<List<ServiceResponse<double>>>("api/card");
            SaveDataFromApi(result);

        }
        private void SaveDataFromApi(List<ServiceResponse<double>> data)
        {
            foreach (var obj in data)
            {
                switch (obj.Message)
                {
                    case "MonthSum":
                        monthSum = obj.Data;
                        break;
                    case "WeekSum":
                        weekSum = obj.Data;
                        break;
                    case "DiffSum":
                        diffSum = obj.Data;
                        break;
                    case "TargetSum":
                        targetSum = obj.Data;
                        break;
                }
            }



        }
    }
}