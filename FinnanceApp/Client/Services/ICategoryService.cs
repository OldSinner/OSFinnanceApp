using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Client.Services
{
    public interface ICategoryService
    {
         Task GetCategory();

         IList<Category> category {get;set;}
    }
}