using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class DeleteRpaVmsLegadoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:long}", HandleAsync)
            .WithName("RpaVmsLegados: Delete")
            .WithSummary("Deleta um RpaVmsLegados")
            .WithDescription("Deleta um RpaVmsLegados.")
            .WithOrder(4)
            //.RequireAuthorization() implementar depois
            .Produces<Response<RpaVmsLegado?>>();
    
    private static async Task<IResult> HandleAsync(IRpaVmsLegadoHandler handler, long id)
    {
        var request = new DeleteRpaVmsLegadoRequest
        {
            Id = id
        };
        var response = await handler.DeleteAsync(request);
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