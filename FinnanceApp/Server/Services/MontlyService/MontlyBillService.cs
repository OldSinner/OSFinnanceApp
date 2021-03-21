using FinnanceApp.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.MontlyService
{
    public class MontlyBillService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public Timer _timer;
        public MontlyBillService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(test, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void test(object state)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                Log.Information("Started Daily check of Montly Bills");
                var _montlyService = scope.ServiceProvider.GetRequiredService<IMontlyService>();
                await _montlyService.AddBillsFromMontlyBill();
                var _userService = scope.ServiceProvider.GetRequiredService<IAuthRepo>();
                await _userService.DeleteInactiveUser();
                Log.Information("End Daily check of Montly Bills");

            }


        }
    }
}