using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IPersonService
    {
        event Action OnChange;
        IList<Person> People { get; set; }
        Task<ServiceResponse<int>> addPerson(string name);
        Task<ServiceResponse<string>> editPerson(int id, string name);
        Task<ServiceResponse<string>> deletePerson(int id);
        Task GetPersonList();

    }
}
