using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Api.Services;
using PcpManagement.Core.Common;
using PcpManagement.Core.Handlers;

namespace PcpManagement.Api.Common.Api;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        /* Adicionar no appSettings ou user-secrets futuramente
        Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        */
    }
    
    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.CustomSchemaIds(n => n.FullName);
        });
    }
    
    public static void AddDataContexts(this WebApplicationBuilder builder)
        => builder
            .Services
            .AddDbContext<RpaContext>(
                x =>
                {
                    x.UseSqlServer(ApiConfiguration.ConnectionString);
                });
    
    public static void AddCrossOrigin(this WebApplicationBuilder builder)
        => builder.Services.AddCors(
            options => options.AddPolicy(
                ApiConfiguration.CorsPolicyName,
                policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                    ])
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            ));

    public static void AddServices(this WebApplicationBuilder builder)
        => builder
            .Services
            .AddTransient<IVirtualMachineHandler, VirtualMachineService>()
            .AddTransient<IRoboHandler, RoboService>();
}