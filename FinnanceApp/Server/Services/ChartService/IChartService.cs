using FinnanceApp.Shared.Models;
using FinnanceApp.Shared.Models.ChartModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.ChartService
{
    public interface IChartService
    {
        Task<ServiceResponse<List<ChartModel>>> GetMonthChart();
        Task<ServiceResponse<List<ChartModel>>> GetPersonChart();

        Task<ServiceResponse<List<ChartModel>>> GetCategoryChart();
        Task<ServiceResponse<List<ChartModel>>> GetShopChart();

    }


}