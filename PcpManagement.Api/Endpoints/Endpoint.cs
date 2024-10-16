using PcpManagement.Api.Common.Api;
using PcpManagement.Api.Endpoints.Legados;
using PcpManagement.Api.Endpoints.Robos;
using PcpManagement.Api.Endpoints.RpaVms;
using PcpManagement.Api.Endpoints.RpaVmsLegados;
using PcpManagement.Api.Endpoints.VirtualMachine;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Requests.VirtualMachines;

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
            .MapEndpoint<GetAllVirtualMachinesEndpoint>()
            .MapEndpoint<GetAllVirtualMachineWithoutAssociationEndpoint>();

        endpoints.MapGroup("v1/robo")
            .WithTags("Robos")
            .MapEndpoint<CreateRoboEndpoint>()
            .MapEndpoint<GetRoboByIdEndpoint>()
            .MapEndpoint<UpdateRoboEndpoint>()
            .MapEndpoint<DeleteRoboEndpoint>()
            .MapEndpoint<GetAllRobosEndpoint>();

        endpoints.MapGroup("v1/rpavms")
            .WithTags("RpaVms")
            .MapEndpoint<AssociateRpaVmsEndpoint>()
            .MapEndpoint<UpdateRpaVmsEndpoint>()
            .MapEndpoint<GetAllVmsByRoboEndpoint>()
            .MapEndpoint<GetAllRpaVmsEndpoint>()
            .MapEndpoint<DeleteRpaVmsEndpoint>()
            .MapEndpoint<GetAllRpaVmsByVmIdFkEndpoint>();
        
        endpoints.MapGroup("v1/legados")
            .WithTags("Legados")
            .MapEndpoint<CreateLegadoEndpoint>()
            .MapEndpoint<GetLegadoByIdEndpoint>()
            .MapEndpoint<UpdateLegadoEndpoint>()
            .MapEndpoint<DeleteLegadoEndpoint>()
            .MapEndpoint<GetAllLegadosEndpoint>();
        
        endpoints.MapGroup("v1/rpavmslegados")
            .WithTags("RpaVmsLegados")
            .MapEndpoint<CreateRpaVmsLegadoEndpoint>()
            .MapEndpoint<GetRpaVmsLegadoByIdEndpoint>()
            .MapEndpoint<GetRpaVmsLegadoByIdFkEndpoint>()
            .MapEndpoint<UpdateRpaVmsLegadoEndpoint>()
            .MapEndpoint<DeleteRpaVmsLegadoEndpoint>()
            .MapEndpoint<GetAllRpaVmsLegadosEndpoint>()
            .MapEndpoint<GetAllRpaVmsLegadosByRpaVmEndpoint>();
            
    }

    private static IEndpointRouteBuilder MapEndpoint<T>(this IEndpointRouteBuilder app)
        where T : IEndpoint
    {
        T.Map(app);
        return app;
    }
    
}