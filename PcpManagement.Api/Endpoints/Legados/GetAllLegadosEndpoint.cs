using Azure;
using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Legados;

namespace PcpManagement.Api.Endpoints.Legados;

public class GetAllLegadosEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Legados: GetAll")
            .WithSummary("Obtém todos os legados")
            .WithDescription("Obtém todos os legados.")
            .WithOrder(5)
            //.RequireAuthorization() implementar depois
            .Produces<Response<List<Core.Models.Legados>>>();

    private static async Task<IResult> HandleAsync(
        ILegadoHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllLegadosRequest
        {
            PageNumber = pageNumber,
            PageSize = pageSize
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