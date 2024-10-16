using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PcpManagement.App;
using PcpManagement.App.Common;
using PcpManagement.App.Layout;
using PcpManagement.App.Request;
using PcpManagement.Core.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddHttpClient(AppConfiguration.HttpClientName, opt => opt.BaseAddress = new Uri(AppConfiguration.BackendUrl));
//Transients
builder.Services
    .AddTransient<IRoboHandler, RoboRequest>()
    .AddTransient<IVirtualMachineHandler, VirtualMachineRequest>()
    .AddTransient<IRpaVmHandler,RpaVmRequest>()
    .AddTransient<ILegadoHandler,LegadosRequest>()
    .AddTransient<IRpaVmsLegadoHandler,RpaVmsLegadoRequest>();
//LayoutServices
builder.Services.AddSingleton<MainLayout.IsDarkModeService>();



await builder.Build().RunAsync();
