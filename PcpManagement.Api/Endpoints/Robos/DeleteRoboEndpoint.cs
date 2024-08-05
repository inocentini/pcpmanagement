using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Robos;

public class DeleteRoboEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id:long}", HandleAsync)
            .WithName("Robo: Delete")
            .WithSummary("Exclui um robo dado um id.")
            .WithDescription("Exclui um robo.")
            .WithOrder(3)
            .Produces<Response<Robo?>>();
    
    private static async Task<IResult> HandleAsync(IRoboHandler handler, long id)
    {
        var request = new DeleteRoboRequest()
        {
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}