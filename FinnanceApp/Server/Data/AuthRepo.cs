using System.Threading;
using FinnanceApp.Server.Services;
using FinnanceApp.Server.Services.BillService;
using FinnanceApp.Server.Services.PersonService;
using FinnanceApp.Server.Services.ShopService;
using FinnanceApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;


        public AuthRepo(DataContext context, IConfiguration configuration, IEmailSender emailSender)
        {
            _context = context;
            _configuration = configuration;
            _emailSender = emailSender;
            
        }

        public async Task<ServiceResponse<string>> Login(string email, string passowrd, bool RememberMe)
        {
            Log.Information("User:" + email + " is logging...");
            var response = new ServiceResponse<string>();
            User user = await _context.Users.Include(entity => entity.role).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                Log.Error("User:" + email + ": User not Found");
                response.isSuccess = false;
                response.Message = "Nie znaleziono użytkownika";
            }
            else if (!VerifyPassword(passowrd, user.PasswordHash, user.PasswordSalt))
            {
                Log.Error("User:" + email + ": Wrong Password");
                response.isSuccess = false;
                response.Message = "Hasło jest niepoprawne";
            }
            else if (user.isConfirmed == false)
            {
                response.isSuccess = false;
                response.Message = "Konto nie jest aktywne! Sprawdź maila!";
            }
            else
            {
                user.lastLogged = DateTime.Now;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                response.Data = CreateToken(user, RememberMe);
                response.Message = "Zalogowano!";
                Log.Information("User:" + email + " Logged");
            }
            
            return response;

        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExist(user.Email) || user.Username == "root")
            {
                return new ServiceResponse<int>
                {
                    isSuccess = false,
                    Message = "Taki użytkownik już istnieje"
                };
            }
            HashPassword(password, out byte[] passwordHash, out byte[] passwordHashCode);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordHashCode;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            await _emailSender.SendEmailActivate(user.id, user.Email);
            Log.Information("New user registered: " + user.id + "/" + user.Username);
            return new ServiceResponse<int>
            {
                Data = user.id,
                Message = "Rejestracja Udana!"
            };

        }
        public async Task<bool> UserExist(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }
        private void HashPassword(string password, out byte[] passwordHash, out byte[] passwordHashCode)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHashCode = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordHashCode)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordHashCode))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }
        private string CreateToken(User user, bool remember)
        {
            DateTime expires_date = new DateTime();
            if (remember)
                expires_date = DateTime.Now.AddMonths(1);
            else
                expires_date = DateTime.Now.AddHours(1);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.role.RoleName)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires_date,
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public async Task<ServiceResponse<string>> activatte([FromBody] string key)
        {
            Console.WriteLine("klucz: " + key);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.activationkey == key);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    isSuccess = false,
                    Message = "Taki użytkownik nie istnieje"
                };
            }
            else if (user.isConfirmed == true)
                return new ServiceResponse<string>
                {
                    isSuccess = false,
                    Message = "Ten użytkownik jest już aktywny!"
                };
            else
            {
                Log.Information(user.id + " activated!");
                user.isConfirmed = true;
                user.activationkey = null;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    isSuccess = true,
                    Message = "Aktywowano konto!"
                };
            }

        }
        public async Task<ServiceResponse<int>> EditProfile(EditProfile profile)
        {
            var user = await _context.Users.Where(x => x.Email == profile.Email).FirstOrDefaultAsync();
            if(user == null)
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    isSuccess = false,
                    Message = "Wystąpił błąd!"
                };
            }
            if (!VerifyPassword(profile.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    isSuccess = false,
                    Message = "Niepoprawne hasło!"
                };
            }
            if (isWhiteSpace(profile.Username))
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    isSuccess = false,
                    Message = "W nazwie użytkownika nie może być spacji!"
                };
            }
            user.Username = profile.Username;
            user.targetValue = profile.TargetValue;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<int>
            {
                Data = 1,
                isSuccess = true,
                Message = "Dokonano zmian! Zaloguj się ponownie, by zmiany były widoczne!"
            };
        }
        private bool isWhiteSpace(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                    return true;
            }
            return false;
        }

        public async Task<ServiceResponse<string>> DeleteUser(User user)
        {
            var _user = await _context.Users.Where(x => x.id == user.id).FirstOrDefaultAsync();
            if (_user == null)
            {
                return new ServiceResponse<string>
                {
                    Data = String.Empty,
                    isSuccess = false,
                    Message = "Nie znaleziono tego użytkownika."
                };
            }
            var bills = await _context.Bills.Where(x => x.OwnerId == _user.id).ToListAsync();
            foreach(var bill in bills)
            {
                _context.Bills.Remove(bill);
            }
            var shops = await _context.Shops.Where(x => x.Owner.id == _user.id).ToListAsync();
            foreach(var shop in shops)
            {
                _context.Shops.Remove(shop);
            }
            var people = await _context.Person.Where(x => x.Owner.id == _user.id).ToListAsync();
            foreach(var person in people)
            {
                _context.Person.Remove(person);
            }
            _context.Users.Remove(_user);
            await _context.SaveChangesAsync();
            Log.Information("User deleted: " + user.id + "/" + user.Username);
            return new ServiceResponse<string>
                {
                    Data = String.Empty,
                    isSuccess = true,
                    Message = "Użytkownik został usunięty"
                };

        }

        public async Task DeleteInactiveUser()
        {
            var users = await _context.Users.ToListAsync();
            foreach (var user in users)
            {
                if(user.lastLogged < DateTime.Now.AddMonths(-3) && user.roleId != 2)
                {
                    await DeleteUser(user);
                }
            }
        }
    }
}
