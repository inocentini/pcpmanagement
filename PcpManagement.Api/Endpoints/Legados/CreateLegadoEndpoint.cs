using Azure;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Legados;

namespace PcpManagement.Api.Endpoints.Legados;

public class CreateLegadoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Legados: Create")
            .WithSummary("Cria um novo legado")
            .WithDescription("Cria um legado.")
            .WithOrder(1)
            //.RequireAuthorization() implementar depois
            .Produces<Response<Core.Models.Legados?>>();

    private static async Task<IResult> HandleAsync(ILegadoHandler handler, CreateLegadoRequest request)
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