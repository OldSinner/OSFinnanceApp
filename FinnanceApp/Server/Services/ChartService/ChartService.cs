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
    public class ChartService : IChartService
    {
        private readonly DataContext _context;

        public ChartService(DataContext context)
        {
            _context = context;

        }
        public async Task<ServiceResponse<List<ChartMonth>>> GetMonthChart()
        {
            var list = new List<ChartMonth>();
            for (int i = 0; i < 6; i++)
            {
                double sum = 0;
                var bills = await _context.Bills.Where(x => x.BuyDate.Month == DateTime.Now.AddMonths(-i).Month).ToListAsync();
                foreach (var bill in bills)
                {
                    sum += bill.money;
                }
                list.Add(new ChartMonth
                {
                    money = sum,
                    month = DateTime.Now.AddMonths(-i).ToString("MMMM")
                });
            }
            return new ServiceResponse<List<ChartMonth>>
            {
                Data = list,
                isSuccess = true,
                Message = "Załadowano chart!"
            };

        }
    }
}