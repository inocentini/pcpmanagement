using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class AssociateRpaVmsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("RpaVms: Associate")
            .WithSummary("Associate a virtual machine with a Robo")
            .WithDescription("Associa uma máquina virtual em um robo.")
            .WithOrder(1)
            //.RequireAuthorization() implementar depois
            .Produces<Response<RpaVm?>>();
    private static async Task<IResult> HandleAsync(IRpaVmHandler handler,AssociateRpaVmsRequest request)
    {
        var response = await handler.AssociateAsync(request);
        return response.IsSuccess
            ? TypedResults.Created($"v1/rpavms/{response.Data?.Id}", response)
            : TypedResults.BadRequest(response);
    }
}