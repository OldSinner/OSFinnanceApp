using System.Threading.Tasks;
using FinnanceApp.Server.Data;
using FinnanceApp.Server.Services;
using FinnanceApp.Server.Services.MontlyService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MontlyBillController : ControllerBase
    {

        private readonly IMontlyService _montlyBills;
        private readonly DataContext _context;

        public MontlyBillController(IMontlyService montlyBills, DataContext context)
        {
            _context = context;
            _montlyBills = montlyBills;
        }
        [HttpPost]
        public async Task<IActionResult> AddMontlyBill(MontlyBills bill)
        {
            var response = await _montlyBills.AddMontlyBill(bill);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> EditMontlyBill(MontlyBills bill)
        {
            var response = await _montlyBills.EditMontyBill(bill);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMontlyBill(int id)
        {
            var response = await _montlyBills.DeleteMontlyBill(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet]

        public async Task<IActionResult> GetMontlyBill()
        {
            var response = await _montlyBills.GetMontlyBill();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("Category")]
        public async Task<IActionResult> GetCategory()
        {
            var category = await _context.Category.ToListAsync();
            return Ok(category);
        }
    }
}