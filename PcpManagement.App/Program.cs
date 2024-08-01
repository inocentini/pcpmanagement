using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PcpManagement.App;
using PcpManagement.App.Common;
using PcpManagement.App.Request;
using PcpManagement.Core;
using PcpManagement.Core.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddHttpClient(AppConfiguration.HttpClientName, opt => opt.BaseAddress = new Uri(Configuration.BackendUrl));
builder.Services.AddTransient<IVirtualMachineHandler, VirtualMachineRequest>();


await builder.Build().RunAsync();
