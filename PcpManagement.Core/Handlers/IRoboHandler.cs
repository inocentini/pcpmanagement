using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface IRoboHandler
{
    Task<Response<Robo?>> CreateAsync(CreateRoboRequest request);
    Task<Response<Robo?>> UpdateAsync(UpdateRoboRequest request);
    Task<Response<Robo?>> DeleteAsync(DeleteRoboRequest request);
    Task<Response<Robo?>> GetByIdAsync(GetRoboByIdRequest request);
    Task<PagedResponse<List<Robo>>> GetAllAsync(GetAllRobosRequest request);
}