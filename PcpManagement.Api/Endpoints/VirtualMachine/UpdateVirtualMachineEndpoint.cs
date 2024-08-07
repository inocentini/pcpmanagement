using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachine;

public class UpdateVirtualMachineEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Virtual Machine: Update")
            .WithSummary("Atualiza uma máquina virtual")
            .WithDescription("Atualiza uma máquina virtual")
            .WithOrder(2)
            .Produces<Response<Vm?>>();

    private static async Task<IResult> HandleAsync(IVirtualMachineHandler handler, UpdateVirtualMachineRequest request, long id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.Code switch
        {
            EStatusCode.OK => TypedResults.Ok(result),
            EStatusCode.BadRequest => TypedResults.BadRequest(result),
            EStatusCode.NotFound => TypedResults.NotFound(result),
            EStatusCode.InternalServerError => TypedResults.StatusCode(500),
            _ => TypedResults.StatusCode((int)result.Code)
        };
    }
}