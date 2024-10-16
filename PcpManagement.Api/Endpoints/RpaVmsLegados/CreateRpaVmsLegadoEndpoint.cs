using Azure;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class CreateRpaVmsLegadoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    => app.MapPost("/", HandleAsync)
        .WithName("RpaVmsLegados: Create")
        .WithSummary("Cria um novo RpaVmsLegados")
        .WithDescription("Cria um RpaVmsLegados.")
        .WithOrder(1)
        //.RequireAuthorization() implementar depois
        .Produces<Response<RpaVmsLegado?>>();

    private static async Task<IResult> HandleAsync(IRpaVmsLegadoHandler handler, CreateRpaVmsLegadoRequest request)
    {
        var response = await handler.CreateAsync(request);
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