using PcpManagement.Api.Common.Api;
using PcpManagement.Api.Endpoints.Robos;
using PcpManagement.Api.Endpoints.VirtualMachine;

namespace PcpManagement.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app.MapGroup(""); //Start Application

        endpoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/",handler: () => new {message = "OK"});

        endpoints.MapGroup("v1/virtualmachine")
            .WithTags("VirtualMachine")
            //.RequireAuthorization() implementar depois
            .MapEndpoint<CreateVirtualMachineEndpoint>()
            .MapEndpoint<GetVirtualMachineByIdEndpoint>()
            .MapEndpoint<UpdateVirtualMachineEndpoint>()
            .MapEndpoint<DeleteVirtualMachineEndpoint>()
            .MapEndpoint<GetAllVirtualMachinesEndpoint>();

        endpoints.MapGroup("v1/robos")
            .WithTags("Robos")
            .MapEndpoint<CreateRoboEndpoint>()
            .MapEndpoint<GetRoboByIdEndpoint>()
            .MapEndpoint<UpdateRoboEndpoint>()
            .MapEndpoint<DeleteRoboEndpoint>()
            .MapEndpoint<GetAllRobosEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app)
        where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
    
}