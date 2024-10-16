using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.RpaVmsLegados;

public class GetAllRpaVmsLegadosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("RpaVmsLegados: GetAll")
            .WithSummary("Obtém todos os RpaVmsLegados")
            .WithDescription("Obtém todos os RpaVmsLegados.")
            .WithOrder(5)
            //.RequireAuthorization() implementar depois
            .Produces<PagedResponse<List<RpaVmsLegado>>>();

    private static async Task<IResult> HandleAsync(
        IRpaVmsLegadoHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllRpaVmsLegadosRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
        var response = await handler.GetAllAsync(request);
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