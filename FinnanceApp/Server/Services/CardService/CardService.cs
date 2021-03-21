using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.CardService
{
    public class CardService : ICardService
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utility;

        public CardService(DataContext context, IUtilityService utility)
        {
            _context = context;
            _utility = utility;
        }
        public async Task<ServiceResponse<double>> MonthSum()
        {
            try
            {
                var response = new ServiceResponse<double>();
                double sum = 0;
                var user = await _utility.GetUser();
                var bills = await _context.Bills.Where(x => x.BuyDate.Month == DateTime.Now.Month && x.BuyDate.Year == DateTime.Now.Year && x.OwnerId == user.id).ToListAsync();

                foreach (var bill in bills)
                {
                    sum += bill.money;
                }
                response.Data = sum;
                response.Message = "MonthSum";
                response.isSuccess = true;
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }
        }

        public async Task<ServiceResponse<double>> WeekSum()
        {
            try
            {
                var response = new ServiceResponse<double>();
                double sum = 0;
                var user = await _utility.GetUser();
                var bills = await _context.Bills.Where(x => x.BuyDate > DateTime.Now.AddDays(-8) && x.OwnerId == user.id).ToListAsync();

                foreach (var bill in bills)
                {
                    if (GetWeekOfMonth(bill.BuyDate) == GetWeekOfMonth(DateTime.Now))
                        sum += bill.money;
                }
                response.Data = sum;
                response.Message = "WeekSum";
                response.isSuccess = true;
                return response;
            }
            catch (Exception x)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }
        }
        public static int GetWeekOfMonth(DateTime date)
        {
            DateTime beginningOfMonth = new DateTime(date.Year, date.Month, 1);

            while (date.Date.AddDays(1).DayOfWeek != CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                date = date.AddDays(1);

            return (int)Math.Truncate((double)date.Subtract(beginningOfMonth).TotalDays / 7f) + 1;
        }

        public async Task<ServiceResponse<double>> DiffSum()
        {
            try
            {
                var actualMonth = await MonthSum();
                DateTime previousMontDate = DateTime.Now.AddMonths(-1);
                double previousMonth = 0;
                var user = await _utility.GetUser();
                var bills = await _context.Bills.Where(x => x.BuyDate.Month == previousMontDate.Month && x.BuyDate.Year == previousMontDate.Year && x.OwnerId == user.id).ToListAsync();

                foreach (var bill in bills)
                {
                    previousMonth += bill.money;
                }

                return new ServiceResponse<double>
                {
                    Data = previousMonth - actualMonth.Data,
                    Message = "DiffSum",
                    isSuccess = true
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }

        }

        public async Task<ServiceResponse<double>> TargetSum()
        {
            try
            {
                var actualMonth = await MonthSum();
                var user = await _utility.GetUser();
                if (user.targetValue == 0)
                    user.targetValue = 1;
                var prc = (actualMonth.Data / user.targetValue) * 100;
                return new ServiceResponse<double>
                {
                    Data = prc,
                    Message = "TargetSum",
                    isSuccess = true
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<double>
                {
                    Data = 0,
                    Message = x.Message,
                    isSuccess = false
                };
            }
        }
    }
}