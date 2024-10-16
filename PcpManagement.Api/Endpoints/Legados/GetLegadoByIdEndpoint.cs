using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Legados;

public class GetLegadoByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Legados: Get")
            .WithSummary("Obtém um legado")
            .WithDescription("Obtém um legado.")
            .WithOrder(2)
            //.RequireAuthorization() implementar depois
            .Produces<Response<Core.Models.Legados?>>();
    
    private static async Task<IResult> HandleAsync(ILegadoHandler handler, long id)
    {
        var request = new GetLegadoByIdRequest()
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