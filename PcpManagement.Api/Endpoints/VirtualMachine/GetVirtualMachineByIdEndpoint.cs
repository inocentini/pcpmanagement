using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachine;

public class GetVirtualMachineByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Virtual Machine: Get By Id")
            .WithSummary("Recupera uma máquina virtual")
            .WithDescription("Recupera uma máquina virtual")
            .WithOrder(4)
            .Produces<Response<Vm?>>();
    
    private static async Task<IResult> HandleAsync(IVirtualMachineHandler handler, long id)
    {
        var request = new GetVirtualMachineByIdRequest()
        {
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}