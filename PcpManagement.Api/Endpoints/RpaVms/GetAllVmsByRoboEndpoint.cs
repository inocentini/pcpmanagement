using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class GetAllVmsByRoboEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{idRoboFk}", HandleAsync)
            .WithName("RpaVms: Get All")
            .WithSummary("Get All virtual machines associated with Robo")
            .WithDescription("Recupera todas as máquinas virtuais por robo.")
            .WithOrder(3)
            .Produces<Response<List<RpaVm>?>>();

    public static async Task<IResult> HandleAsync(IRpaVmHandler handler, int idRoboFk)
    {
        var request = new GetAllVmsByRoboRequest
        {
            idRoboFK = idRoboFk
        };
        var result = await handler.GetVmsByRoboAsync(request);
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