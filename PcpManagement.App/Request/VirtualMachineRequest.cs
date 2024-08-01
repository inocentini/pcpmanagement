using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Enums;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class VirtualMachineRequest(IHttpClientFactory httpClientFactory) : IVirtualMachineHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName); // Refatorar com interface IFactory
    public async Task<Response<VirtualMachine?>> CreateAsync(CreateVirtualMachineRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/virtualmachine", request);
        return await result.Content.ReadFromJsonAsync<Response<VirtualMachine?>>() ?? new Response<VirtualMachine?>(null,EStatusCode.BadRequest,"Falha ao criar a máquina virtual.");
    }

    public async Task<Response<VirtualMachine?>> UpdateAsync(UpdateVirtualMachineRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/virtualmachine/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<VirtualMachine?>>() ?? new Response<VirtualMachine?>(null,EStatusCode.BadRequest,"Falha ao atualizar uma máquina virtual.");
    }

    public async Task<Response<VirtualMachine?>> DeleteAsync(DeleteVirtualMachineRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/virtualmachine/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<VirtualMachine?>>() ?? new Response<VirtualMachine?>(null,EStatusCode.BadRequest,"Falha ao excluir uma máquina virtual.");
    }

    public async Task<Response<VirtualMachine?>> GetByIdAsync(GetVirtualMachineByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<VirtualMachine?>>($"v1/virtualmachine/{request.Id}")
           ?? new Response<VirtualMachine?>(null, EStatusCode.BadRequest, "Não foi possível obter a máquina virtual.");

    public async Task<PagedResponse<List<VirtualMachine>?>> GetAllAsync(GetAllVirtualMachinesRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<VirtualMachine>?>>("v1/virtualmachine")
           ?? new PagedResponse<List<VirtualMachine>?>(null, EStatusCode.BadRequest, "Não foi possível obter as máquinas virtuais.");
}