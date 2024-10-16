using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class LegadosRequest(IHttpClientFactory httpClientFactory) : ILegadoHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName);
    public async Task<Response<Legados?>> CreateAsync(CreateLegadoRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/legados", request);
        return await result.Content.ReadFromJsonAsync<Response<Legados?>>()
               ?? new Response<Legados?>(null,code: EStatusCode.BadRequest,"Falha ao criar o Legado.");
    }

    public async Task<Response<Legados?>> UpdateAsync(UpdateLegadoRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/legados/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Legados?>>()
               ?? new Response<Legados?>(null,code: EStatusCode.BadRequest,"Falha ao atualizar um Legado.");
    }

    public async Task<Response<Legados?>> DeleteAsync(DeleteLegadoRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/legados/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Legados?>>()
               ?? new Response<Legados?>(null,code: EStatusCode.BadRequest,"Falha ao excluir um Legado.");
    }

    public async Task<Response<Legados?>> GetByIdAsync(GetLegadoByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<Legados?>>($"v1/legados/{request.Id}")
               ?? new Response<Legados?>(null, code: EStatusCode.BadRequest, "Não foi possível obter o Legado.");

    public async Task<PagedResponse<List<Legados>>> GetAllAsync(GetAllLegadosRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<Legados>>>("v1/legados")
           ?? new PagedResponse<List<Legados>>(null, code: EStatusCode.BadRequest, "Não foi possível obter os AllLegados.");
}