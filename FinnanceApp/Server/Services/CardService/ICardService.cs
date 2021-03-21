using FinnanceApp.Shared.Models;
using System.Threading.Tasks;

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