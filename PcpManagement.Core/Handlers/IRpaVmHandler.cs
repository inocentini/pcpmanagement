using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface IRpaVmHandler
{
    Task<Response<RpaVm?>> AssociateAsync(AssociateRpaVmsRequest request);
    Task<Response<RpaVm?>> UpdateAsync(UpdateRpaVmsRequest request);
    Task<PagedResponse<List<RpaVm>?>> GetVmsByRoboAsync(GetAllVmsByRoboRequest byRoboRequest);
    Task<PagedResponse<List<RpaVm>>> GetAllAsync(GetAllRpaVmsRequest request);
    Task<Response<RpaVm?>> DeleteAsync(DeleteRpaVmsRequest request);
    
}