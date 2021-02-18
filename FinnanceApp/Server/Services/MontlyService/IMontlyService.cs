using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Server.Services.MontlyService
{
    public interface IMontlyService
    {
      Task<ServiceResponse<string>> AddMontlyBill(MontlyBills bill);
      Task<ServiceResponse<string>> EditMontyBill(MontlyBills bill);
      Task<ServiceResponse<string>> DeleteMontlyBill(int id);
      
      Task<ServiceResponse<List<MontlyBills>>> GetMontlyBill();
      Task AddBillsFromMontlyBill();
    }
}