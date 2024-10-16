using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class RpaVmsLegadoRequest(IHttpClientFactory httpClientFactory) : IRpaVmsLegadoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName);
    public async Task<Response<RpaVmsLegado?>> CreateAsync(CreateRpaVmsLegadoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/rpavmslegados", request);
        return await result.Content.ReadFromJsonAsync<Response<RpaVmsLegado?>>()
               ?? new Response<RpaVmsLegado?>(null,code: EStatusCode.BadRequest,"Falha ao criar o RpaVmsLegado.");
    }

    public async Task<Response<RpaVmsLegado?>> UpdateAsync(UpdateRpaVmsLegadoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/rpavmslegados/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<RpaVmsLegado?>>()
               ?? new Response<RpaVmsLegado?>(null,code: EStatusCode.BadRequest,"Falha ao atualizar um RpaVmsLegado.");
    }

    public async Task<Response<RpaVmsLegado?>> GetByIdAsync(GetRpaVmsLegadoByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<RpaVmsLegado?>>($"v1/rpavmslegados/{request.Id}")
               ?? new Response<RpaVmsLegado?>(null, code: EStatusCode.BadRequest, "Não foi possível obter o RpaVmsLegado.");

    public async Task<PagedResponse<List<RpaVmsLegado>>> GetAllByRpaVmAsync(GetAllRpaVmsLegadoByRpaVmRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<RpaVmsLegado>>>($"v1/rpavmslegados/getbyvmfk/{request.IdRpaVmFk}")
           ?? new PagedResponse<List<RpaVmsLegado>>(null, code: EStatusCode.BadRequest, "Não foi possível obter os RpaVmsLegados.");

    public async Task<Response<RpaVmsLegado?>> GetByRpaVmAsync(GetRpaVmsLegadoByRpaVmRequest request)
        => await _httpClient.GetFromJsonAsync<Response<RpaVmsLegado?>>($"v1/rpavmslegados/getbyfk/{request.IdRpaVmFk}")
               ?? new Response<RpaVmsLegado?>(null, code: EStatusCode.BadRequest, "Não foi possível obter o RpaVmsLegado.");

    public async Task<PagedResponse<List<RpaVmsLegado>>> GetAllAsync(GetAllRpaVmsLegadosRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<RpaVmsLegado>>>("v1/rpavmslegados")
           ?? new PagedResponse<List<RpaVmsLegado>>(null, code: EStatusCode.BadRequest, "Não foi possível obter os RpaVmsLegados.");

    public async Task<Response<RpaVmsLegado?>> DeleteAsync(DeleteRpaVmsLegadoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/rpavmslegados/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<RpaVmsLegado?>>()
               ?? new Response<RpaVmsLegado?>(null,code: EStatusCode.BadRequest,"Falha ao excluir um RpaVmsLegado.");
    }
}