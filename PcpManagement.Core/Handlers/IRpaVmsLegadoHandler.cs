using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface IRpaVmsLegadoHandler
{
    Task<Response<RpaVmsLegado?>> CreateAsync(CreateRpaVmsLegadoRequest request);
    Task<Response<RpaVmsLegado?>> UpdateAsync(UpdateRpaVmsLegadoRequest request);
    Task<Response<RpaVmsLegado?>> GetByIdAsync(GetRpaVmsLegadoByIdRequest request);
    Task<PagedResponse<List<RpaVmsLegado>>> GetAllByRpaVmAsync(GetAllRpaVmsLegadoByRpaVmRequest request);
    
    Task<Response<RpaVmsLegado?>> GetByRpaVmAsync(GetRpaVmsLegadoByRpaVmRequest request);
    Task<PagedResponse<List<RpaVmsLegado>>> GetAllAsync(GetAllRpaVmsLegadosRequest request);
    Task<Response<RpaVmsLegado?>> DeleteAsync(DeleteRpaVmsLegadoRequest request);
}