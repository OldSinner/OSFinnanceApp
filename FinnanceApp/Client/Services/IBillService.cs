using FinnanceApp.Shared.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Client.Services
{
    public interface IBillService
    {
        event Action OnChange;
        IList<Bills> bill { get; set; }

        int pages {get;set;}

        IList<Bills> billWithPages { get; set; }
        Task<ServiceResponse<int>> AddBill(Bills bill);
        Task<ServiceResponse<int>> DeleteBill(int id, int page);

        Task GetBillListWithPages(int page);
    }
}
