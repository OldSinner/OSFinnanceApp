using FinnanceApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.BillService
{
    public interface IBillService
    {
        Task<ServiceResponse<List<Bills>>> getBillsListWithPages(int page);
        Task<ServiceResponse<int>> AddBill(Bills bill);
        Task<ServiceResponse<int>> DeleteBill(int id);
    }
}
