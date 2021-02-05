using System;
using System.Linq;
using System.Threading.Tasks;
using FinnanceApp.Server.Data;
using FinnanceApp.Server.Services.BillService;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinnanceApp.Server.Services.MontlyService
{
    public class MontlyService : IMontlyService
    {
        private readonly DataContext _context;
        private readonly IBillService _billService;

        public MontlyService(DataContext context, IBillService billService)
        {
            _context = context;
            _billService = billService;
        }

        public async Task AddBillsFromMontlyBill()
        {
            var bills = await _context.MontlyBills.Where(x => x.dayOfMonth == DateTime.Now.Day).ToListAsync();
            if (DateTime.Now.Month == 2)
            {
                var febBill = await _context.MontlyBills.Where(x => x.dayOfMonth > 28).ToListAsync();
                foreach (var bill in febBill)
                {
                    bills.Add(bill);
                }
            }
            if (bills.Count != 0)
            {
                foreach (var bill in bills)
                {
                    var nBill = new Bills
                    {
                        money = bill.value,
                        Owner = bill.user,
                        Shop = bill.shop,
                        Person = bill.person,
                        BuyDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        comment = "Miesięczny rachunek: " + bill.description
                    };
                    _context.Bills.Add(nBill);
                    Log.Information("Added Montly Bill for: " + nBill.OwnerId);
                }
                await _context.SaveChangesAsync();
            }


        }

        public async Task<ServiceResponse<string>> AddMontlyBill(MontlyBills bill)
        {
            await _context.MontlyBills.AddAsync(bill);
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
            {
                Data = String.Empty,
                isSuccess = true,
                Message = "Usunięto rachunek miesięczny!"
            };

        }

        public async Task<ServiceResponse<string>> DeleteMontlyBill(MontlyBills bill)
        {
            var dBill = await _context.MontlyBills.Where(x => x.id == bill.id).FirstOrDefaultAsync();
            if (dBill == null)
            {
                return new ServiceResponse<string>
                {
                    Data = String.Empty,
                    isSuccess = false,
                    Message = "Wystąpił błąd, nie znaleziono rachunku!"
                };
            }
            _context.MontlyBills.Remove(dBill);
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
            {
                Data = String.Empty,
                isSuccess = true,
                Message = "Usunięto rachunek miesięczny!"
            };
        }

        public async Task<ServiceResponse<string>> EditMontyBill(MontlyBills bill)
        {
            var dBill = await _context.MontlyBills.Where(x => x.id == bill.id).FirstOrDefaultAsync();
            if (dBill == null)
            {
                return new ServiceResponse<string>
                {
                    Data = String.Empty,
                    isSuccess = false,
                    Message = "Wystąpił błąd, nie znaleziono rachunku!"
                };
            }
            _context.MontlyBills.Update(bill);
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
            {
                Data = String.Empty,
                isSuccess = true,
                Message = "Zedytowano rachunek miesięczny"
            };
        }
    }
}