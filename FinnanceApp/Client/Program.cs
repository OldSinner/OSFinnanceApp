using Blazored.LocalStorage;
using Blazored.Toast;
using FinnanceApp.Client.Services;
using FinnanceApp.Client.Services.AdminService;
using FinnanceApp.Client.Services.CardService;
using FinnanceApp.Client.Services.MonthlyService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace FinnanceApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddFileReaderService(options => {
                options.UseWasmSharedBuffer = true;
            });
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IBillService, BillService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IShopService, ShopService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<ICardService,CardService>();
            builder.Services.AddScoped<IChartService,ChartService>();
            builder.Services.AddScoped<IMonthlyService,MonthlyService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IAdditionalService,AdditionalService>();
            builder.Services.AddScoped<IAdminService,AdminService>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            await builder.Build().RunAsync();
        }
    }
}
