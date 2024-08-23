using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Api.Services;
using PcpManagement.Core.Common;
using PcpManagement.Core.Handlers;
using System.Reflection;

namespace PcpManagement.Api.Common.Api;

public static class BuildExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.Sources.Clear();
        builder.Configuration
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json",false)
            .AddUserSecrets(Assembly.GetEntryAssembly()!);
        var connStr = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Dev"))
            {
                Password = builder.Configuration.GetSection("DbPwd").Value
            };
        Configuration.ConnectionString = connStr.ConnectionString;
        Configuration.BackendUrl = builder.Configuration.GetSection("BackendUrl").Value ?? string.Empty;
        Configuration.FrontendUrl = builder.Configuration.GetSection("FrontendUrl").Value ?? string.Empty;
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
                    x.UseSqlServer(Configuration.ConnectionString);
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
            .AddTransient<IRoboHandler, RoboService>()
            .AddTransient<IRpaVmHandler,RpaVmsService>();
}