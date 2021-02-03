using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Server.Services.CardService
{
    public interface ICardService
    {
         Task<ServiceResponse<double>> MonthSum();
         Task<ServiceResponse<double>> WeekSum();

         Task<ServiceResponse<double>> DiffSum();

         Task<ServiceResponse<double>> TargetSum();
    }
}