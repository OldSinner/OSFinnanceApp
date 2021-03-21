using FinnanceApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface ICategoryService
    {
        Task GetCategory();

        IList<Category> category { get; set; }
    }
}