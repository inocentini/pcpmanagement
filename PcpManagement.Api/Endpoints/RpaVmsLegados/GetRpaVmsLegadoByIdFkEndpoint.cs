using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class GetRpaVmsLegadoByIdFkEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/getbyfk/{id}", HandleAsync)
            .WithName("RpaVmsLegados: Get By FK Id")
            .WithSummary("Obtém um RpaVmsLegados pelo Id da FK de VM")
            .WithDescription("Obtém um RpaVmsLegados através da FK.")
            .WithOrder(6)
            //.RequireAuthorization() implementar depois
            .Produces<Response<RpaVmsLegado>>();
    
    private static async Task<IResult> HandleAsync(
        IRpaVmsLegadoHandler handler,
        int id)
    {
        var request = new GetRpaVmsLegadoByRpaVmRequest()
        {
            IdRpaVmFk = id
        };
        
        var response = await handler.GetByRpaVmAsync(request);
        return response.Code switch
        {
            EStatusCode.OK => TypedResults.Ok(response),
            EStatusCode.BadRequest => TypedResults.BadRequest(response),
            EStatusCode.NotFound => TypedResults.NotFound(response),
            EStatusCode.InternalServerError => TypedResults.StatusCode(500),
            _ => TypedResults.StatusCode((int)response.Code)
        };
    }
}