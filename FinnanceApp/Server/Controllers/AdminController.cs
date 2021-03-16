using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinnanceApp.Server.Services;
using FinnanceApp.Server.Services.AdminService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _admin;
        public AdminController(IAdminService admin)
        {
            _admin = admin;

        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            var response = await _admin.GetUserList();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
        [HttpPost("activateuser")]
        public async Task<IActionResult> ActivateUser([FromBody]int id)
        {
            var response = await _admin.ActivateUser(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
        [HttpPost("GrantAdmin")]
        public async Task<IActionResult> GrantAdmin([FromBody]int id)
        {
            var response = await _admin.GrantAdmin(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
        [HttpPost("DeactiveUser")] 
        public async Task<IActionResult> DeactiveUser([FromBody]int id)
        {
            var response = await _admin.SelectAsInactive(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

    }
}