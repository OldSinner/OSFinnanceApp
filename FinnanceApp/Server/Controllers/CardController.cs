using FinnanceApp.Server.Services.CardService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cservice;

        public CardController(ICardService cservice)
        {
            _cservice = cservice;
        }
        [HttpGet]
        public async Task<IActionResult> GetCards()
        {
            List<ServiceResponse<double>> responses = new List<ServiceResponse<double>>();
            responses.Add(await _cservice.MonthSum());
            responses.Add(await _cservice.WeekSum());
            responses.Add(await _cservice.DiffSum());
            responses.Add(await _cservice.TargetSum());
            if (!isok(responses))
            {
                return BadRequest("Error");
            }
            else
            {
                return Ok(responses);
            }

        }

        bool isok(List<ServiceResponse<double>> check)
        {
            foreach (var obj in check)
            {
                if (!obj.isSuccess)
                    return false;
            }
            return true;
        }
    }
}
