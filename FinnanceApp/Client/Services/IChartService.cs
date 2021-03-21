using FinnanceApp.Shared.Models.ChartModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IChartService
    {

        IList<ChartModel> chartMonths { get; set; }
        IList<ChartModel> chartPerson { get; set; }

        IList<ChartModel> chartCategory { get; set; }
        IList<ChartModel> chartShop { get; set; }

        Task GetMonthChart();
        Task GetPersonChart();

        Task GetCategoryChart();

        Task GetShopChart();
    }
}