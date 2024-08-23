using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class DeleteRpaVmsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/", HandleAsync)
            .WithName("RpaVms: Delete")
            .WithSummary("Disassociate a virtual machine with a robo.")
            .WithDescription("Desassocia uma máquina virtual com um robo.")
            .WithOrder(5)
            .Produces<Response<RpaVm?>>();

    public static async Task<IResult> HandleAsync(IRpaVmHandler handler, long id)
    {
        var request = new DeleteRpaVmsRequest
        {
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        return result.Code switch
        {
            EStatusCode.OK => TypedResults.Ok(result),
            EStatusCode.BadRequest => TypedResults.BadRequest(result),
            EStatusCode.NotFound => TypedResults.NotFound(result),
            EStatusCode.InternalServerError => TypedResults.StatusCode(500),
            _ => TypedResults.StatusCode((int)result.Code)
        };
    }
}