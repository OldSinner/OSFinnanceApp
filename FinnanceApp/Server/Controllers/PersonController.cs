using FinnanceApp.Server.Services.PersonService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {

            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> addperson([FromBody] string name)
        {
            var response = await _personService.AddPerson(name);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> EditPerson([FromBody] Person person)
        {
            var response = await _personService.EditPerson(person);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var response = await _personService.DeletePerson(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetPerson()
        {
            var response = await _personService.GetPersonList();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
