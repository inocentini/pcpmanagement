using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Core.Handlers;

public interface ILegadoHandler
{
    Task<Response<Legados?>> CreateAsync(CreateLegadoRequest request);
    Task<Response<Legados?>> UpdateAsync(UpdateLegadoRequest request);
    Task<Response<Legados?>> DeleteAsync(DeleteLegadoRequest request);
    Task<Response<Legados?>> GetByIdAsync(GetLegadoByIdRequest request);
    Task<PagedResponse<List<Legados>>> GetAllAsync(GetAllLegadosRequest request);   
}