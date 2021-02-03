using System.Collections.Generic;
using System.Threading.Tasks;
using FinnanceApp.Shared.Models;

namespace FinnanceApp.Client.Services.CardService
{
    public interface ICardService
    {
        double monthSum { get; set; }
        double weekSum { get; set; }
        double diffSum { get; set; }
        double targetSum {get;set;}
        Task GetCards();
    }
}