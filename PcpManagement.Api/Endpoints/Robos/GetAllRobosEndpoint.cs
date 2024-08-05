using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.Robos;

public class GetAllRobosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Robo: Get All")
            .WithSummary("Recupera todos os robos cadastrados")
            .WithDescription("Recupera todos os robos.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Robo>?>>();
    
    private static async Task<IResult> HandleAsync(
        IRoboHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllRobosRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };

        var result = await handler.GetAllAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}