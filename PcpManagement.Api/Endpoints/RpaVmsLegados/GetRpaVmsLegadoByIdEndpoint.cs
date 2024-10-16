using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class GetRpaVmsLegadoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id:long}", HandleAsync)
            .WithName("RpaVmsLegados: Get")
            .WithSummary("Obtém um RpaVmsLegados")
            .WithDescription("Obtém um RpaVmsLegados.")
            .WithOrder(2)
            //.RequireAuthorization() implementar depois
            .Produces<Response<RpaVmsLegado>>();

    private static async Task<IResult> HandleAsync(
        IRpaVmsLegadoHandler handler,
        long id)
    {
        var request = new GetRpaVmsLegadoByIdRequest
        {
            Id = id
        };
        
        var response = await handler.GetByIdAsync(request);
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