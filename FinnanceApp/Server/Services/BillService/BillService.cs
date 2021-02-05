using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.BillService
{
    public class BillService : IBillService
    {
        private readonly DataContext _context;
        private readonly IUtilityService _utilityService;

        public BillService(DataContext context, IUtilityService utilityService)
        {
            _context = context;
            _utilityService = utilityService;
        }

        public async Task<ServiceResponse<int>> AddBill(Bills bill)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            var user = await _utilityService.GetUser();
            bill.Shop = await _context.Shops.FirstOrDefaultAsync(x => x.id == bill.ShopId);
            bill.Person = await _context.Person.FirstOrDefaultAsync(x => x.id == bill.PersonId);

            if (bill.Shop == null)
            {
                response.isSuccess = false;
                response.Message = "Wystąpił błąd, odśwież stronę!";
            }
            else if (bill.Person == null)
            {
                response.isSuccess = false;
                response.Message = "Wystąpił błąd, odśwież stronę!";
            }
            else
            {
                bill.Owner = user;
                await _context.Bills.AddAsync(bill);
                await _context.SaveChangesAsync();
                response.isSuccess = true;
                response.Data = bill.id;
                response.Message = $"Dodano rachunek o wartości: {Math.Round(bill.money, 2)} zł";
            }
            return response;


        }

        public async Task<ServiceResponse<int>> DeleteBill(int id)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            var bill = await _context.Bills.FirstOrDefaultAsync(b => b.id == id);
            if (bill == null)
            {
                response.isSuccess = false;
                response.Message = "Nie udało się usunąć rachunku. Odśwież stronę!";
            }
            else
            {
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
                response.isSuccess = true;
                response.Message = $"Rachunek o wartości {bill.money} zł, został usunięty!";

            }
            return response;
        }

        public async Task<ServiceResponse<List<Bills>>> getBillsList()
        {
            ServiceResponse<List<Bills>> response = new ServiceResponse<List<Bills>>();
            var user = await _utilityService.GetUser();
            var bills = await _context.Bills.Where(x => x.Owner.id == user.id).Include(entity => entity.Shop).Include(entity => entity.Person).ToListAsync();
            response.isSuccess = true;
            Console.WriteLine(bills.Count);
            response.Message = "Załadowano listę sklepów";
            response.Data = bills;
            return response;

        }
        public async Task<ServiceResponse<List<Bills>>> getBillsListWithPages(int page)
        {
            ServiceResponse<List<Bills>> response = new ServiceResponse<List<Bills>>();
            var user = await _utilityService.GetUser();
            var bills = await _context.Bills.Where(x => x.Owner.id == user.id).Include(entity => entity.Shop).Include(entity => entity.Person).OrderByDescending(x => x.BuyDate).Skip(page * 10).Take(10).ToListAsync();
            var pages = _context.Bills.Where(x => x.Owner.id == user.id).Count()/10;
            if(_context.Bills.Where(x => x.Owner.id == user.id).Count()%10 > 0) pages+=1;
            response.isSuccess = true;
            response.Message = pages.ToString();
            response.Data = bills;
            return response;

        }
    }
}
