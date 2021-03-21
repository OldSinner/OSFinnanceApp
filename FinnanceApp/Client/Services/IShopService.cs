using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IShopService
    {
        Task<ServiceResponse<int>> addShop(string name);
        event Action OnChange;
        IList<Shops> Shops { get; set; }
        Task<ServiceResponse<string>> DeleteShop(int id);
        Task GetShopList();
        Task<ServiceResponse<int>> editShop(int id, string name);
    }
}
