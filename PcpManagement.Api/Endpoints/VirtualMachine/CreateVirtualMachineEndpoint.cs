using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachine;

public class CreateVirtualMachineEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Virtual Machine: Create")
            .WithSummary("Cria uma nova máquina virtual")
            .WithDescription("Cria uma máquina virtual.")
            .WithOrder(1)
            //.RequireAuthorization() implementar depois
            .Produces<Response<Vm?>>();
    
    private static async Task<IResult> HandleAsync(IVirtualMachineHandler handler,CreateVirtualMachineRequest request)
    {
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/virtualmachine/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}