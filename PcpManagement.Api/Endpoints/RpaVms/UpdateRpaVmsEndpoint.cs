using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class UpdateRpaVmsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/", HandleAsync)
            .WithName("RpaVms: Update")
            .WithSummary("Update the virtual machine's association on a Robo")
            .WithDescription("Atualiza a associação de uma máquina virtual em um robo.")
            .WithOrder(2)
            //.RequireAuthorization() implementar depois
            .Produces<Response<RpaVm?>>();
    private static async Task<IResult> HandleAsync(IRpaVmHandler handler, UpdateRpaVmsRequest request, long id)
    {
        request.Id = id;
        var result = await handler.UpdateAsync(request);
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