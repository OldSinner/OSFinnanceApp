using FinnanceApp.Server.Services;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
            var response = new ServiceResponse<string>();
            User user = await _context.Users.Include(entity => entity.role).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            if (user == null)
            {
                response.isSuccess = false;
                response.Message = "Nie znaleziono użytkownika";
            }
            else if (!VerifyPassword(passowrd, user.PasswordHash, user.PasswordSalt))
            {
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
                response.Data = CreateToken(user, RememberMe);
                response.Message = "Zalogowano!";
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

        public async Task<ServiceResponse<string>> activatte(string key)
        {
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
            if (!VerifyPassword(profile.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<int>
                {
                    Data = 0,
                    isSuccess = false,
                    Message = "Niepoprawne hasło!"
                };
            }
            if(isWhiteSpace(profile.Username))
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
        private bool isWhiteSpace(string str )
        {
           for (int i = 0;i<str.Length ; i++)
           {
               if(str[i]==' ')
               return true;
           }
           return false;
        }
    }
}
