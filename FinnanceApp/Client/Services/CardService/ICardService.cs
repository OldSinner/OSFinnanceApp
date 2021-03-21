using System.Threading.Tasks;

namespace FinnanceApp.Client.Services.CardService
{
    public interface ICardService
    {
        double monthSum { get; set; }
        double weekSum { get; set; }
        double diffSum { get; set; }
        double targetSum { get; set; }
        Task GetCards();
    }
}