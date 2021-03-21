using Finnanceapp.Shared.Models;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IAdditionalService
    {
        Task<CoronaCases> GetCoronaCases();
    }
}