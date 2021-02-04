using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FinnanceApp.Server.Additional;
using FinnanceApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinnanceApp.Server.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly DataContext _context;


        public EmailSender(IConfiguration config, DataContext context)
        {
            _context = context;
            _config = config;
        }
        public async Task SendEmailActivate(int id, string email)
        {
            KeyGenerator gen = new KeyGenerator();
            var key = gen.GetRandomString();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            user.activationkey=key;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = _config.GetValue<string>("Passwords:gmail-client-username"),
                    Password = _config.GetValue<string>("Passwords:gmail-client-password")
                }

            };
            MailAddress basic = new MailAddress("oldsinnernorreply@gmail.com", "Do Not Reply");
            MailAddress reciver = new MailAddress(email, "New Account!");
            


            MailMessage message = new MailMessage()
            {
                From = basic,
                Subject = "Aktywuj konto na platformie OS!",
                Body = $"Witaj w Serwisie OSFinnaneApp!\n\n" +
                $"Został już Ci tylko ostatni krok przed dołączeniem!\n" +
                $"Aktywuj swoje konto używając linku: https://localhost:5001/activation/{key}" +
                "\n\n\n Z poważaniem \n OSFinnance App"
            };
            message.To.Add(reciver);
            try
            {
                await client.SendMailAsync(message);

            }
            catch { }


        }

    }
}
