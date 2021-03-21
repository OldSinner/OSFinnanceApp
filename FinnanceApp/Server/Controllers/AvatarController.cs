using FinnanceApp.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
        [HttpPost]
        public async Task<IActionResult> PostImage([FromForm] IFormFile image)
        {
            var user = await _utility.GetUser();
            string[] allowedExtension = { ".jpg" };
            if (image == null || image.Length == 0)
            {
                return BadRequest("Upload a file!");

            }
            string fileName = image.FileName;
            string extension = Path.GetExtension(fileName);
            if (!allowedExtension.Contains(extension))
            {
                return BadRequest("File is not a valid image");
            }
            string newFileName = "avatar_" + user.id.ToString() + extension;
            string filePath = Path.Combine(_webhost.ContentRootPath, "wwwroot", "Avatars", newFileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await image.CopyToAsync(fileStream);
            }
            return Ok(filePath);



        }
    }

}