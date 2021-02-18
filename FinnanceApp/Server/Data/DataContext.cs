using FinnanceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Shops> Shops { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Bills> Bills { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<MontlyBills> MontlyBills { get; set; }

        public DbSet<Category> Category {get; set;}


    }
}
