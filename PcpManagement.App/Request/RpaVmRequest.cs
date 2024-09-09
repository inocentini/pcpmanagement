using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class RpaVmRequest(IHttpClientFactory httpClientFactory) : IRpaVmHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName);
    public async Task<Response<RpaVm?>> AssociateAsync(AssociateRpaVmsRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/rpavms", request);
        return await result.Content.ReadFromJsonAsync<Response<RpaVm?>>() 
               ?? new Response<RpaVm?>(null,code: EStatusCode.BadRequest,"Falha ao associar máquina virtual.");
    }

    public async Task<Response<RpaVm?>> UpdateAsync(UpdateRpaVmsRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/rpavms/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<RpaVm?>>()
               ?? new Response<RpaVm?>(null, code: EStatusCode.BadRequest, "Falha ao atualizar uma associação.");
    }

    public async Task<PagedResponse<List<RpaVm>?>> GetVmsByRoboAsync(GetAllVmsByRoboRequest byRoboRequest)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<RpaVm>?>>($"v1/rpavms/{byRoboRequest.idRoboFK}")
        ?? new PagedResponse<List<RpaVm>?>(null, code: EStatusCode.BadRequest, "Não foi possível obter a lista de associações por robô.");

    public async Task<PagedResponse<List<RpaVm>>> GetAllAsync(GetAllRpaVmsRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<RpaVm>>>($"v1/rpavms")
           ?? new PagedResponse<List<RpaVm>>(null, code: EStatusCode.BadRequest,
               $"Não foi possível obter a lista de associações");


    public async Task<Response<RpaVm?>> DeleteAsync(DeleteRpaVmsRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/rpavms/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<RpaVm?>>()
               ?? new Response<RpaVm?>(null, code: EStatusCode.BadRequest, "Falha ao desassociar uma vm à um robô.");
    }
}