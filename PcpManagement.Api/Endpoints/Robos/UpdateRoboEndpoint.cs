using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Robos;

public class UpdateRoboEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Robo: Update")
            .WithSummary("Atualiza um robo")
            .WithDescription("Atualiza um robo")
            .WithOrder(2)
            .Produces<Response<Robo?>>();

    private static async Task<IResult> HandleAsync(IRoboHandler handler, UpdateRoboRequest request, long id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}