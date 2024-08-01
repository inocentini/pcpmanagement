using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Common;
using PcpManagement.Api.Common.Api;
using PcpManagement.Api.Data;
using PcpManagement.Api.Endpoints;
using PcpManagement.Api.Services;
using PcpManagement.Core.Handlers;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();
if (app.Environment.IsDevelopment())
    app.ConfigureDevEnvironment();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();
app.Run();
