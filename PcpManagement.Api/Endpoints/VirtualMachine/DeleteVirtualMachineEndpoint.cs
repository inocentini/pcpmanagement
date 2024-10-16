using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachine;

public class DeleteVirtualMachineEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:long}", HandleAsync)
            .WithName("Virtual Machine: Delete")
            .WithSummary("Exclui uma máquina virtual")
            .WithDescription("Exclui uma máquina virtual")
            .WithOrder(3)
            .Produces<Response<Vm?>>();
    
    private static async Task<IResult> HandleAsync(IVirtualMachineHandler handler, long id)
    {
        var request = new DeleteVirtualMachineRequest()
        {
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}