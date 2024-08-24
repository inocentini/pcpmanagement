using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface IVirtualMachineHandler
{
    Task<Response<Vm?>> CreateAsync(CreateVirtualMachineRequest request);
    Task<Response<Vm?>> UpdateAsync(UpdateVirtualMachineRequest request);
    Task<Response<Vm?>> DeleteAsync(DeleteVirtualMachineRequest request);
    Task<Response<Vm?>> GetByIdAsync(GetVirtualMachineByIdRequest request);
    Task<PagedResponse<List<Vm>>> GetAllAsync(GetAllVirtualMachinesRequest request);
    Task<PagedResponse<List<Vm>>> GetAllWithoutAssociationAsync(GetAllVirtualMachineWithouAssociationRequest request);
}