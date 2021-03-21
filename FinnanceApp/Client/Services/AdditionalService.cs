using Finnanceapp.Shared.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class AdditionalService : IAdditionalService
    {
        public async Task<CoronaCases> GetCoronaCases()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-data.p.rapidapi.com/totals"),
                Headers =
                {
                { "x-rapidapi-key", "b66b8c501bmshb9c7b572774557ep14aed0jsne3df088fcd8c" },
                { "x-rapidapi-host", "covid-19-data.p.rapidapi.com" },
                },
            };

            try
            {
                var responsee = await client.SendAsync(request);
                responsee.EnsureSuccessStatusCode();
                var boody = await responsee.Content.ReadAsStringAsync();
                boody = boody.Substring(1, boody.Length - 2);
                return JsonSerializer.Deserialize<CoronaCases>(boody);
            }
            catch (Exception ex)
            {

            }
            return new CoronaCases();


        }
    }

}