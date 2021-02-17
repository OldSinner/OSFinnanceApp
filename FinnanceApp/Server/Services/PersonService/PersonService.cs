using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.PersonService
{
    public class PersonService :IPersonService
    {
        private readonly IUtilityService _utilityService;
        private readonly DataContext _context;

        public PersonService(IUtilityService utilityService, DataContext context)
        {
            _utilityService = utilityService;
            _context = context;
        }
        public async Task<bool> PersonExist(string name)
        {
            var user = await _utilityService.GetUser();
            if (await _context.Person.AnyAsync(x => x.Owner.id == user.id && x.name.ToLower() == name.ToLower()))
            {
                return true;
            }
            else return false;
        }
        public async Task<ServiceResponse<int>> AddPerson(string name)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            Person person = new Person();
            var user = await _utilityService.GetUser();
            person.Owner = user;
            person.name = name;
            if (await PersonExist(person.name))
            {
                response.isSuccess = false;
                response.Message = "Taki Płatnik Już istnieje!";
            }
            else if (string.IsNullOrWhiteSpace(name))
            {
                response.Data = 0;
                response.isSuccess = false;
                response.Message = "Nazwa płatnika nie może być pusta";
            }
            else
            {
                await _context.Person.AddAsync(person);
                await _context.SaveChangesAsync();
                response.Data = person.id;
                response.isSuccess = true;
                response.Message = $"Dodano Płatnika: {person.name}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<Person>>> GetPersonList()
        {
            ServiceResponse<List<Person>> response = new ServiceResponse<List<Person>>();
            var user = await _utilityService.GetUser();
            var persons = await _context.Person.Where(x => x.Owner.id == user.id).ToListAsync();
            response.Data = persons;
            response.isSuccess = true;
            return response;
        }

        public async Task<ServiceResponse<string>> DeletePerson(int id)
        {
            var user = await _utilityService.GetUser();
            ServiceResponse<string> response = new ServiceResponse<string>();
            Person dbperson = await _context.Person.FirstOrDefaultAsync(u => u.id == id && u.Owner == user);
            if (dbperson == null)
            {
                response.isSuccess = false;
                response.Message = "Nie znaleziono sklepu. Przeładuj Stronę";
            }
            else
            {
                List<Bills> bills = await _context.Bills.Where(b => b.PersonId == id).ToListAsync();
                foreach (var bill in bills)
                {
                    _context.Bills.Remove(bill);
                }
                _context.Person.Remove(dbperson);
                await _context.SaveChangesAsync();
                response.isSuccess = true;
                response.Message= $"Płatnik {dbperson.name} i {bills.Count} przypisanych rachunków zostały usunięte";
            }
            return response;
           
        }

        public async Task<ServiceResponse<string>> EditPerson(Person person)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            Person dbperson = await _context.Person.FirstOrDefaultAsync(u => u.id == person.id);
            var user = await _utilityService.GetUser();
            if (dbperson == null)
            {
                response.isSuccess = false;
                response.Message = "Nie znaleziono płatnika, odśwież stronę";
            }
            else
            {
                response.Message = $"Płatnik {dbperson.name} nazywa się teraz: ";
                dbperson.Owner = user;
                dbperson.name = person.name;
                _context.Person.Update(dbperson);
                await _context.SaveChangesAsync();
                response.isSuccess = true;
                response.Message += dbperson.name; 
            }
            return response;
            
        }
    }
}
