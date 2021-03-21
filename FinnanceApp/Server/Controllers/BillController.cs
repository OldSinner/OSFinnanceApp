using FinnanceApp.Server.Services.BillService;
using FinnanceApp.Shared.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BillController : ControllerBase
    {

        private readonly IBillService _bill;

        public BillController(IBillService bill)
        {
            _bill = bill;
        }
        [HttpPost]
        public async Task<IActionResult> AddBill([FromBody] Bills bill)
        {
            var response = await _bill.AddBill(bill);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> GetBillPages(int page)
        {
            var response = await _bill.getBillsListWithPages(page);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var response = await _bill.DeleteBill(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
