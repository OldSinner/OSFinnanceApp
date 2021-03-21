using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;

        public PersonService(HttpClient http)
        {
            _http = http;
        }


        public event Action OnChange;
        void PersonChanged() => OnChange.Invoke();
        public IList<Person> People { get; set; } = new List<Person>();
        public async Task<ServiceResponse<int>> addPerson(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                name = " ";
            var result = await _http.PostAsJsonAsync<string>("api/Person", name);
            var x = await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            People.Clear();
            await GetPersonList();
            PersonChanged();
            return x;

        }

        public async Task<ServiceResponse<string>> editPerson(int id, string name)
        {
            Person person = new Person { id = id, Owner = null, name = name };
            var result = await _http.PutAsJsonAsync<Person>("api/Person", person);

            People.Clear();
            await GetPersonList();
            PersonChanged();
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }

        public async Task GetPersonList()
        {
            if (People.Count == 0)
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<Person>>>("api/Person");
                People = result.Data;
            }


        }

        public async Task<ServiceResponse<string>> deletePerson(int id)
        {
            var result = await _http.DeleteAsync("api/Person/" + id);
            People.Clear();
            await GetPersonList();
            PersonChanged();
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();

        }
    }
}
