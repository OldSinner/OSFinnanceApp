using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.PersonService
{
    public interface IPersonService
    {
        Task<ServiceResponse<List<Person>>> GetPersonList();
        Task<ServiceResponse<int>> AddPerson(string name);
        Task<ServiceResponse<string>> DeletePerson(int id);
        Task<ServiceResponse<string>> EditPerson(Person person);
        Task<bool> PersonExist(string name);
    }
}
