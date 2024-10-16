using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class GetAllRpaVmsLegadosByRpaVmEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/getbyvmfk/{id}", HandleAsync)
            .WithName("RpaVmsLegados: Get By RpaVm")
            .WithSummary("Obtém todos os RpaVmsLegados pelo Id de RpaVm")
            .WithDescription("Obtém todos os RpaVmsLegados pelo Id de RpaVm.")
            .WithOrder(7)
            //.RequireAuthorization() implementar depois
            .Produces<Response<List<RpaVmsLegado>>>();

    private static async Task<IResult> HandleAsync(IRpaVmsLegadoHandler handler, int id)
    {
        var request = new GetAllRpaVmsLegadoByRpaVmRequest()
        {
            IdRpaVmFk = id
        };
        var result = await handler.GetAllByRpaVmAsync(request);
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