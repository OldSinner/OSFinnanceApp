using System.Threading.Tasks;

namespace FinnanceApp.Server.Services
{
    public interface IEmailSender
    {
        Task SendEmailActivate(int id, string email);

    }
}
