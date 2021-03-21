
using FinnanceApp.Server.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FinnanceApp.Server.Services.MontlyService
{
    public class UserControllService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public Timer _timer;
        public UserControllService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromHours(24));
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async void Run(object state)
        {
            Log.Information("Started Daily check of inactive user");
            using (var scope = _scopeFactory.CreateScope())
            {
                var _userService = scope.ServiceProvider.GetRequiredService<IAuthRepo>();
                await _userService.DeleteInactiveUser();
            }
            Log.Information("End Daily check of inactive user");
        }
    }
}