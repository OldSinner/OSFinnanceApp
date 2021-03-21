using System;

namespace FinnanceApp.Shared.Models
{
    public class User
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool isConfirmed { get; set; } = false;

        public double targetValue { get; set; } = 1000;
        public string activationkey { get; set; }
        public int roleId { get; set; } = 1;
        public Roles role { get; set; }

        public DateTime lastLogged { get; set; } = DateTime.Now;


    }
}
