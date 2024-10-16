using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Models;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Robos;

public class CreateRoboEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Robos: Create")
            .WithSummary("Cria um novo robo")
            .WithDescription("Cria um robo.")
            .WithOrder(1)
            //.RequireAuthorization() implementar depois
            .Produces<Response<Robo?>>();
    
    private static async Task<IResult> HandleAsync(IRoboHandler handler,CreateRoboRequest request)
    {
        var response = await handler.CreateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/robo/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}