using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using FinnanceApp.Shared.Models.ChartModels;

namespace FinnanceApp.Server.Services.ChartService
{
    public interface IChartService
    {
        Task<ServiceResponse<List<ChartMonth>>> GetMonthChart();

    }


}