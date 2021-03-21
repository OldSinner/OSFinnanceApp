using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.ShopService
{
    public class ShopService : IShopService
    {
        private readonly IUtilityService _utilityService;
        private readonly DataContext _context;

        public ShopService(IUtilityService utilityService, DataContext context)
        {
            _utilityService = utilityService;
            _context = context;
        }

        public async Task<ServiceResponse<List<Shops>>> GetShopList()
        {
            try
            {
                var response = new ServiceResponse<List<Shops>>();
                var user = await _utilityService.GetUser();
                var shops = await _context.Shops.Where(x => x.Owner.id == user.id).ToListAsync();
                if (shops == null)
                {
                    response.isSuccess = false;
                    response.Message = "Nie znaleziono sklepów";
                }
                else
                {
                    response.Data = shops;
                    response.isSuccess = true;
                    response.Message = "Uzyskano sklepy";
                }
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<Shops>>
                {
                    Data = null,
                    Message = x.Message,
                    isSuccess = false
                };
            }
        }

        public async Task<bool> ShopExist(string name)
        {
            var user = await _utilityService.GetUser();
            if (await _context.Shops.AnyAsync(x => x.Owner.id == user.id && x.name.ToLower() == name.ToLower()))
            {
                return true;
            }
            else return false;
        }
        public async Task<ServiceResponse<int>> AddShop(string name)
        {
            try
            {
                var response = new ServiceResponse<int>();
                if (await ShopExist(name))
                {
                    response.Data = 0;
                    response.isSuccess = false;
                    response.Message = "Taki sklep już istnieje";
                }
                else if (string.IsNullOrWhiteSpace(name))
                {
                    response.Data = 0;
                    response.isSuccess = false;
                    response.Message = "Nazwa sklepu nie może być pusta";
                }
                else
                {
                    var user = await _utilityService.GetUser();
                    Shops shop = new Shops()
                    {
                        name = name,
                        Owner = user
                    };
                    await _context.Shops.AddAsync(shop);
                    await _context.SaveChangesAsync();

                    response.Data = shop.id;
                    response.isSuccess = true;
                    response.Message = $"Dodano sklep: {shop.name}";
                }
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }

        }

        public async Task<ServiceResponse<string>> DeleteShop(int id)
        {
            try
            {
                var response = new ServiceResponse<string>();
                var user = await _utilityService.GetUser();
                Shops dbshop = await _context.Shops.FirstOrDefaultAsync(u => u.id == id && u.Owner == user);
                if (dbshop == null)
                {
                    response.isSuccess = false;
                    response.Message = "Nie znaleziono sklepu. Spróbuj odświeżyć stronę";
                }
                else
                {
                    List<Bills> bills = await _context.Bills.Where(b => b.ShopId == id).ToListAsync();
                    foreach (var bill in bills)
                    {
                        _context.Bills.Remove(bill);
                    }
                    _context.Shops.Remove(dbshop);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.Message = $"Sklep {dbshop.name} i {bills.Count} przypisanych rachunków zostały usunięte";
                }
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<string>
                {
                    Data = string.Empty,
                    Message = x.Message,
                    isSuccess = false
                };
            }
        }
        public async Task<ServiceResponse<int>> EditShop(Shops shop)
        {
            try
            {
                var response = new ServiceResponse<int>();
                Shops dbshop = await _context.Shops.FirstOrDefaultAsync(u => u.id == shop.id);
                if (dbshop == null)
                {
                    response.isSuccess = false;
                    response.Message = "Nie znaleziono sklepu, odśwież stronę";
                }
                else
                {
                    response.Message = $"Sklep {dbshop.name} to teraz: ";
                    dbshop.name = shop.name;
                    _context.Shops.Update(dbshop);
                    await _context.SaveChangesAsync();
                    response.isSuccess = true;
                    response.Message += dbshop.name;
                }
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }


        }

    }
}
