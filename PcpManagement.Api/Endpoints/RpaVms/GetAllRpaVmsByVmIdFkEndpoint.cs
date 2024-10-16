using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class GetAllRpaVmsByVmIdFkEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/getallbyvmidfk/{id}", HandleAsync)
            .WithName("RpaVms: Get All By VmIdFk")
            .WithSummary("Get All RpaVms by VmIdFk")
            .WithDescription("Recupera todas as associações de robos por máquina virtual.")
            .WithOrder(6)
            .Produces<PagedResponse<List<RpaVm>?>>();

    public static async Task<IResult> HandleAsync(IRpaVmHandler handler, int id)
    {
        var request = new GetAllRpaVmsByVmIdFkRequest
        {
            IdVmFk = id
        };
        var result = await handler.GetAllByVmIdFkAsync(request);
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