using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachine;

public class GetAllVirtualMachineWithoutAssociationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/vmwithoutassociation", HandleAsync)
            .WithName("Virtual Machine: Get All Without Association")
            .WithSummary("Recupera todas as máquinas virtuais sem associação de um robo")
            .WithDescription("Recupera todas as máquinas virtuais sem associação de um robo")
            .WithOrder(6)
            .Produces<PagedResponse<List<Vm>?>>();

    private static async Task<IResult> HandleAsync(
        IVirtualMachineHandler handler,
        GetAllVirtualMachineWithouAssociationRequest request)
    {

        var result = await handler.GetAllWithoutAssociationAsync(request);
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