using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using FinnanceApp.Server.Data;
using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace FinnanceApp.Server.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly DataContext _context;
        public AdminService(DataContext context)
        {
            _context = context;

        }

        public async Task<ServiceResponse<string>> ActivateUser(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
                user.isConfirmed = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Data = "",
                    Message = "Aktywowano!",
                    isSuccess = true
                };

            }
            catch (Exception x)
            {
                return new ServiceResponse<string>
                {
                    Data = x.Message,
                    Message = "Wystąpił błąd",
                    isSuccess = false
                };
            }


        }

        public async Task<ServiceResponse<List<User>>> GetUserList()
        {
            try
            {
                var userlist = await _context.Users.Include(entity => entity.role).ToListAsync();
                return new ServiceResponse<List<User>>
                {
                    Data = userlist,
                    isSuccess = true,
                    Message = ""
                };
            }
            catch (Exception x)
            {
                return new ServiceResponse<List<User>>
                {
                    Data = null,
                    isSuccess = false,
                    Message = x.Message
                };
            }



        }

        public async Task<ServiceResponse<string>> GrantAdmin(int id)
        {
            try
            {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleName == "Admin");
            user.role=role;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ServiceResponse<string>
                {
                    Data = "",
                    Message = "Nadano role Admin dla " + user.Username,
                    isSuccess = true
                };
            }
            catch (Exception x)
            {
              return new ServiceResponse<string>
                {
                    Data = x.Message,
                    Message = "Wystąpił błąd",
                    isSuccess = false
                };
            }
        }

        public async Task<ServiceResponse<string>> SelectAsInactive(int id)
        {
             try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.id == id);
                user.lastLogged = DateTime.Now.AddYears(-2);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return new ServiceResponse<string>
                {
                    Data = "",
                    Message = "Aktywowano!",
                    isSuccess = true
                };

            }
            catch (Exception x)
            {
                return new ServiceResponse<string>
                {
                    Data = x.Message,
                    Message = "Wystąpił błąd",
                    isSuccess = false
                };
            }
        }
    }
}