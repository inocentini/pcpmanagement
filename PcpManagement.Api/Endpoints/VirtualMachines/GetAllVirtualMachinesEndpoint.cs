using Microsoft.AspNetCore.Mvc;
using PcpManagement.Api.Common;
using PcpManagement.Api.Common.Api;
using PcpManagement.Core;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Endpoints.VirtualMachines;

public class GetAllVirtualMachinesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
        .WithName("Virtual Machine: Get All")
        .WithSummary("Recupera todas as máquinas virtuais")
        .WithDescription("Recupera todas as máquinas virtuais")
        .WithOrder(5)
        .Produces<PagedResponse<List<VirtualMachine>?>>();
    
    private static async Task<IResult> HandleAsync(
        IVirtualMachineHandler handler,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllVirtualMachinesRequest()
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