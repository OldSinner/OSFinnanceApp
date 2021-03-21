using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using FinnanceApp.Shared.Models.ChartModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.ChartService
{
    public class ChartService : IChartService
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utility;

        public ChartService(DataContext context, IUtilityService utility)
        {
            _utility = utility;
            _context = context;

        }

        public async Task<ServiceResponse<List<ChartModel>>> GetCategoryChart()
        {
            try
            {
                var user = await _utility.GetUser();
                var categoryList = await _context.Category.ToListAsync();
                var list = new List<ChartModel>();
                foreach (var category in categoryList)
                {
                    double sum = 0;
                    var billsList = await _context.Bills.Where(x => x.Owner == user && x.Category == category
                                                                && x.BuyDate.Month == DateTime.Now.Month
                                                                && x.BuyDate.Year == DateTime.Now.Year).ToListAsync();
                    foreach (var bill in billsList)
                    {
                        sum += bill.money;
                    }
                    if (sum > 0)
                    {
                        list.Add(new ChartModel
                        {
                            money = sum,
                            month = category.name
                        });
                    }
                }
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = list,
                    isSuccess = true,
                    Message = "Załadowano chart!"
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = null,
                    Message = x.Message,
                    isSuccess = false
                };
            }

        }

        public async Task<ServiceResponse<List<ChartModel>>> GetMonthChart()
        {
            try
            {
                var user = await _utility.GetUser();
                var list = new List<ChartModel>();
                for (int i = 0; i < 6; i++)
                {
                    double sum = 0;
                    var bills = await _context.Bills.Where(x => x.BuyDate.Month == DateTime.Now.AddMonths(-i).Month && x.OwnerId == user.id).ToListAsync();
                    foreach (var bill in bills)
                    {
                        sum += bill.money;
                    }
                    list.Add(new ChartModel
                    {
                        money = Math.Round(sum, 2),
                        month = DateTime.Now.AddMonths(-i).ToString("MMMM")
                    });
                }
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = list,
                    isSuccess = true,
                    Message = "Załadowano chart!"
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = null,
                    Message = x.Message,
                    isSuccess = false
                };
            }

        }

        public async Task<ServiceResponse<List<ChartModel>>> GetPersonChart()
        {
            try
            {
                var user = await _utility.GetUser();
                var list = new List<ChartModel>();
                var personlist = await _context.Person.Where(x => x.Owner.id == user.id).ToListAsync();

                foreach (var person in personlist)
                {
                    double sum = 0;
                    var billList = await _context.Bills.Where(x => x.PersonId == person.id && x.BuyDate.Month == DateTime.Now.Month && x.BuyDate.Year == DateTime.Now.Year).ToListAsync();
                    foreach (var bill in billList)
                    {
                        sum += bill.money;
                    }
                    if (sum > 0)
                    {
                        list.Add(new ChartModel()
                        {
                            money = Math.Round(sum, 2),
                            month = person.name
                        });
                    }


                }
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = list,
                    isSuccess = true,
                    Message = "Załadowano chart!"
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = null,
                    Message = x.Message,
                    isSuccess = false
                };
            }


        }
        public async Task<ServiceResponse<List<ChartModel>>> GetShopChart()
        {
            try
            {
                var user = await _utility.GetUser();
                var list = new List<ChartModel>();
                var shoplist = await _context.Shops.Where(x => x.Owner == user).ToListAsync();

                foreach (var shop in shoplist)
                {
                    double sum = 0;
                    var billList = await _context.Bills.Where(x => x.ShopId == shop.id && x.BuyDate.Month == DateTime.Now.Month && x.BuyDate.Year == DateTime.Now.Year).ToListAsync();
                    foreach (var bill in billList)
                    {
                        sum += bill.money;
                    }
                    if (sum > 0)
                    {
                        list.Add(new ChartModel()
                        {
                            money = Math.Round(sum, 2),
                            month = shop.name
                        });
                    }


                }
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = list,
                    isSuccess = true,
                    Message = "Załadowano chart!"
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<ChartModel>>
                {
                    Data = null,
                    Message = x.Message,
                    isSuccess = false
                };
            }


        }
    }
}
