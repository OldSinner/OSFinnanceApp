using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Client.Services.MonthlyService
{
    public interface IMonthlyService
    {
        Task GetMonthlyBills();

        event Action OnChange;
        IList<MontlyBills> montlyBills { get; set; }
        Task<ServiceResponse<string>> AddMontlyBill(MontlyBills montly);
        Task<ServiceResponse<string>> DeleteMontlyBill(int id);
        Task<ServiceResponse<string>> EditMontlyBill(MontlyBills montly);


    }
}