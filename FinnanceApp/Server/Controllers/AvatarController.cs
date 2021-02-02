using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinnanceApp.Server.Services;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AvatarController : ControllerBase
    {
        private readonly IWebHostEnvironment _webhost;
        private readonly IUtilityService _utility;
        public AvatarController(IWebHostEnvironment webhost, IUtilityService utility)
        {
            _utility = utility;
            _webhost = webhost;
        }
    }
}