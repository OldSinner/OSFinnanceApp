using FinnanceApp.Server.Data;
using FinnanceApp.Server.Services;
using FinnanceApp.Server.Services.ShopService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopsController : ControllerBase
    {

        private readonly IShopService _shopService;

        public ShopsController(IShopService shopService)
        {
            _shopService = shopService;
        }
        [HttpGet]
        public async Task<IActionResult> GetShop()
        {
            var response = await _shopService.GetShopList();
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> addshop([FromBody] string name)
        {
            var response = await _shopService.AddShop(name);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(int id)
        {
            var response = await _shopService.DeleteShop(id);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> EditShop([FromBody] Shops shop)
        {
            var response = await _shopService.EditShop(shop);
            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
