using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Robos;

public class GetRoboByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Robo: Get By Id")
            .WithSummary("Recupera um robo pelo id")
            .WithDescription("Recupera um robo")
            .WithOrder(4)
            .Produces<Response<Robo?>>();
    
    private static async Task<IResult> HandleAsync(IRoboHandler handler, long id)
    {
        var request = new GetRoboByIdRequest()
        {
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}