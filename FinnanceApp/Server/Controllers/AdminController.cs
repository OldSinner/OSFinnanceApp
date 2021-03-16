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
          if(!response.isSuccess)
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