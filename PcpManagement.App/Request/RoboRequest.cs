using System.Net.Http.Json;
using PcpManagement.App.Common;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.App.Request;

public class RoboRequest(IHttpClientFactory httpClientFactory) : IRoboHandler
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(AppConfiguration.HttpClientName); // Refatorar com interface IFactory
    public async Task<Response<Robo?>> CreateAsync(CreateRoboRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("v1/robo",request);
        return await result.Content.ReadFromJsonAsync<Response<Robo?>>()
            ?? new Response<Robo?>(null, code: EStatusCode.BadRequest, "Falha ao criar um robo.");
    }

    public async Task<Response<Robo?>> DeleteAsync(DeleteRoboRequest request)
    {
        var result = await _httpClient.DeleteAsync($"v1/robo/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Robo?>>()
            ?? new Response<Robo?>(null, code: EStatusCode.BadRequest, "Falha ao excluir um robo.");
    }

    public async Task<PagedResponse<List<Robo>>> GetAllAsync(GetAllRobosRequest request)
        => await _httpClient.GetFromJsonAsync<PagedResponse<List<Robo>>>("v1/robo")
           ?? new PagedResponse<List<Robo>>(null, code: EStatusCode.BadRequest, "Não foi possível obter a lista de robos.");

    public async Task<Response<Robo?>> GetByIdAsync(GetRoboByIdRequest request)
        => await _httpClient.GetFromJsonAsync<Response<Robo?>>($"v1/robo/{request.Id}")
           ?? new Response<Robo?>(null, code: EStatusCode.BadRequest, "Não foi possível obter o robo.");

    public async Task<Response<Robo?>> UpdateAsync(UpdateRoboRequest request)
    {
        var result = await _httpClient.PutAsJsonAsync($"v1/robo/{request.Id}", request);
        return await result.Content.ReadFromJsonAsync<Response<Robo?>>()
            ?? new Response<Robo?>(null, code: EStatusCode.BadRequest, message: $"Não foi possível atualziar o robo {request.Projeto}");
    }
}