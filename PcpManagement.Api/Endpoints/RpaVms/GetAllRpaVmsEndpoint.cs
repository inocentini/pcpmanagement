using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVms;

public class GetAllRpaVmsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("RpaVms: Get All RpaVms")
            .WithSummary("Get All virtual machines associated with Robo")
            .WithDescription("Recupera todas as máquinas virtuais associadas a um robo.")
            .WithOrder(4)
            .Produces<Response<List<RpaVm>?>>();
    
    public static async Task<IResult> HandleAsync(
        IRpaVmHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllRpaVmsRequest
        {
            PageNumber = 0,
            PageSize = 0
        };
        var result = await handler.GetAllAsync(request);
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