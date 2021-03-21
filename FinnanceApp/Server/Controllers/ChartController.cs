using FinnanceApp.Server.Services.ChartService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChartController : ControllerBase
    {

        private readonly IChartService _chartService;
        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }
        [HttpGet("month")]
        public async Task<IActionResult> GetMonthChart()
        {
            var response = await _chartService.GetMonthChart();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("person")]
        public async Task<IActionResult> GetPersonChart()
        {
            var response = await _chartService.GetPersonChart();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("category")]
        public async Task<IActionResult> GetCategoryChart()
        {
            var response = await _chartService.GetCategoryChart();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("shop")]
        public async Task<IActionResult> GetShopChart()
        {
            var response = await _chartService.GetShopChart();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}