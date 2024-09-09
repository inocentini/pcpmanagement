using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class VirtualMachineRequest(IHttpClientFactory httpClientFactory) : IVirtualMachineHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName); // Refatorar com interface IFactory
    public async Task<Response<Vm?>> CreateAsync(CreateVirtualMachineRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/virtualmachine", request);
        return await result.Content.ReadFromJsonAsync<Response<Vm?>>() 
               ?? new Response<Vm?>(null,code: EStatusCode.BadRequest,"Falha ao criar a máquina virtual.");
    }

    public async Task<Response<Vm?>> UpdateAsync(UpdateVirtualMachineRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/virtualmachine/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Vm?>>() 
               ?? new Response<Vm?>(null,code: EStatusCode.BadRequest,"Falha ao atualizar uma máquina virtual.");
    }

    public async Task<Response<Vm?>> DeleteAsync(DeleteVirtualMachineRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/virtualmachine/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Vm?>>() 
               ?? new Response<Vm?>(null,code: EStatusCode.BadRequest,"Falha ao excluir uma máquina virtual.");
    }

    public async Task<Response<Vm?>> GetByIdAsync(GetVirtualMachineByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<Vm?>>($"v1/virtualmachine/{request.Id}")
           ?? new Response<Vm?>(null, code: EStatusCode.BadRequest, "Não foi possível obter a máquina virtual.");

    public async Task<PagedResponse<List<Vm>>> GetAllAsync(GetAllVirtualMachinesRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<Vm>>>("v1/virtualmachine")
           ?? new PagedResponse<List<Vm>>(null, code: EStatusCode.BadRequest, "Não foi possível obter as máquinas virtuais.");

    public async Task<PagedResponse<List<Vm>>> GetAllWithoutAssociationAsync(
        GetAllVirtualMachineWithouAssociationRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/virtualmachine/vmwithoutassociation",request);
        return await result.Content.ReadFromJsonAsync<PagedResponse<List<Vm>>>() 
               ?? new PagedResponse<List<Vm>>(null, code: EStatusCode.BadRequest, "Não foi possível obter as máquinas virtuais sem associação.");
    }
        
            
    
}