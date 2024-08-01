using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface IVirtualMachineHandler
{
    Task<Response<VirtualMachine?>> CreateAsync(CreateVirtualMachineRequest request);
    Task<Response<VirtualMachine?>> UpdateAsync(UpdateVirtualMachineRequest request);
    Task<Response<VirtualMachine?>> DeleteAsync(DeleteVirtualMachineRequest request);
    Task<Response<VirtualMachine?>> GetByIdAsync(GetVirtualMachineByIdRequest request);
    Task<PagedResponse<List<VirtualMachine>?>> GetAllAsync(GetAllVirtualMachinesRequest request);
}