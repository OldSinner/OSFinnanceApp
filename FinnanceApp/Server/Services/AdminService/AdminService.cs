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
        public async Task<ServiceResponse<List<User>>> GetUserList()
        {
           var userlist = await _context.Users.Include(entity => entity.role).ToListAsync();
           return new ServiceResponse<List<User>>
           {
               Data = userlist,
               isSuccess = true,
               Message =""
           };
        }
    }
}