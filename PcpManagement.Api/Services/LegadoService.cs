using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class LegadoService(RpaContext context) : ILegadoHandler
{
    public async Task<Response<Legados?>> CreateAsync(CreateLegadoRequest request)
    {
        try
        {
            var legado = new Legados
            {
                Nome = request.Nome,
                Autenticacao = request.Autenticacao,
                Acesso = request.Acesso,
                Url = request.Url
            };
            await context.Legados.AddAsync(legado);
            await context.SaveChangesAsync();
            return new Response<Legados?>(legado, EStatusCode.Created, "Legado criado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Legados?>(null, EStatusCode.InternalServerError,
                $"Não foi possível criar um legado. {e.Message}");
        }
    }

    public async Task<Response<Legados?>> UpdateAsync(UpdateLegadoRequest request)
    {
        try
        {
            var legado = await context.Legados.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (legado is null)
                return new Response<Legados?>(null, EStatusCode.NotFound, "Legado não encontrado.");
            legado.Nome = request.Nome;
            legado.Autenticacao = request.Autenticacao;
            legado.Acesso = request.Acesso;
            legado.Url = request.Url;
            context.Update(legado);
            await context.SaveChangesAsync();
            return new Response<Legados?>(legado, EStatusCode.OK, "Legado atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Legados?>(null, EStatusCode.InternalServerError,
                $"Não foi possível atualizar um legado. {e.Message}");
        }
    }

    public async Task<Response<Legados?>> DeleteAsync(DeleteLegadoRequest request)
    {
        try
        {
            var legado = await context.Legados.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (legado is null)
                return new Response<Legados?>(null, EStatusCode.NotFound, "Legado não encontrado.");
            context.Legados.Remove(legado);
            await context.SaveChangesAsync();
            return new Response<Legados?>(legado, EStatusCode.OK, "Legado deletado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Legados?>(null, EStatusCode.InternalServerError,
                $"Não foi possível deletar um legado. {e.Message}");
        }
    }

    public async Task<Response<Legados?>> GetByIdAsync(GetLegadoByIdRequest request)
    {
        try
        {
            var legado = await context.Legados.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (legado is null)
                return new Response<Legados?>(null, EStatusCode.NotFound, "Legado não encontrado.");
            return new Response<Legados?>(legado, EStatusCode.OK, "Legado encontrado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Legados?>(null, EStatusCode.InternalServerError,
                $"Não foi possível encontrar um legado. {e.Message}");
        }
    }

    public async Task<PagedResponse<List<Legados>>> GetAllAsync(GetAllLegadosRequest request)
    {
        try
        {
            var legados = await context.Legados.ToListAsync();
            return new PagedResponse<List<Legados>>(legados, EStatusCode.OK, "Legados encontrados com sucesso.");
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Legados>>(null, EStatusCode.InternalServerError,
                $"Não foi possível encontrar legados. {e.Message}");
        }
    }
}