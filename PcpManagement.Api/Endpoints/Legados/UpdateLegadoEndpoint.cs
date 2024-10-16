using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Legados;

public class UpdateLegadoEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id:long}", HandleAsync)
            .WithName("Legados: Update")
            .WithSummary("Atualiza um legado")
            .WithDescription("Atualiza um legado.")
            .WithOrder(3)
            //.RequireAuthorization() implementar depois
            .Produces<Response<Core.Models.Legados?>>();
    
    private static async Task<IResult> HandleAsync(ILegadoHandler handler, long id, UpdateLegadoRequest request)
    {
        request.Id = id;
        var response = await handler.UpdateAsync(request);
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