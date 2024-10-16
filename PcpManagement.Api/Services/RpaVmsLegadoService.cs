using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class RpaVmsLegadoService(RpaContext context) : IRpaVmsLegadoHandler
{

    public async Task<Response<RpaVmsLegado?>> CreateAsync(CreateRpaVmsLegadoRequest request)
    {
        try
        {
            var rpaVmsLegado = new RpaVmsLegado
            {
                IdLegadoFk = request.IdLegadoFk,
                IdRpaVmFk = request.IdRpaVmFk,
                UserLegado = request.UserLegado,
                UserBanco = request.UserBanco
            };
            await context.RpaVmsLegados.AddAsync(rpaVmsLegado);
            await context.SaveChangesAsync();
            return new Response<RpaVmsLegado?>(rpaVmsLegado, EStatusCode.Created, "RpaVmsLegado criado com sucesso.");
        }catch(Exception e)
        {
            return new Response<RpaVmsLegado?>(null, EStatusCode.InternalServerError, $"Não foi possível criar um RpaVmsLegado. {e.Message}");
        }
    }

    public async Task<Response<RpaVmsLegado?>> UpdateAsync(UpdateRpaVmsLegadoRequest request)
    {
        try
        {
            var rpaVmsLegado = await context.RpaVmsLegados.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (rpaVmsLegado is null)
                return new Response<RpaVmsLegado?>(null, EStatusCode.NotFound, "RpaVmsLegado não encontrado.");
            rpaVmsLegado.IdLegadoFk = request.IdLegadoFk;
            rpaVmsLegado.IdRpaVmFk = request.IdRpaVmFk;
            rpaVmsLegado.UserLegado = request.UserLegado;
            rpaVmsLegado.UserBanco = request.UserBanco;
            
            context.RpaVmsLegados.Update(rpaVmsLegado);
            await context.SaveChangesAsync();
            
            return new Response<RpaVmsLegado?>(rpaVmsLegado, EStatusCode.OK, "RpaVmsLegado atualizado com sucesso.");
        }catch(Exception e)
        {
            return new Response<RpaVmsLegado?>(null, EStatusCode.InternalServerError, $"Não foi possível atualizar um RpaVmsLegado. {e.Message}");
        }
    }
    
    public async Task<Response<RpaVmsLegado?>> GetByIdAsync(GetRpaVmsLegadoByIdRequest request)
    {
        try
        {
            if (request.Id == 0)
                return new Response<RpaVmsLegado?>(null, EStatusCode.BadRequest, "Id não pode ser 0.");
            var rpaVmsLegado = await context.RpaVmsLegados.FirstOrDefaultAsync(x => x.Id == request.Id);
            return rpaVmsLegado is null 
                ? new Response<RpaVmsLegado?>(null, EStatusCode.NotFound, "RpaVmsLegado não encontrado.") 
                : new Response<RpaVmsLegado?>(rpaVmsLegado, EStatusCode.OK, "RpaVmsLegado encontrado com sucesso.");
            
        }catch(Exception e)
        {
            return new Response<RpaVmsLegado?>(null, EStatusCode.InternalServerError, $"Não foi possível encontrar um RpaVmsLegado. {e.Message}");
        }
    }

    public async Task<PagedResponse<List<RpaVmsLegado>>> GetAllByRpaVmAsync(GetAllRpaVmsLegadoByRpaVmRequest request)
    {
        try
        {
            var rpaVmsLegados = await context.RpaVmsLegados.Where(x => x.IdRpaVmFk == request.IdRpaVmFk).ToListAsync();
            return new PagedResponse<List<RpaVmsLegado>>(rpaVmsLegados, EStatusCode.OK, "RpaVmsLegados encontrados com sucesso.");
        }
        catch (Exception e)
        {
            return new PagedResponse<List<RpaVmsLegado>>(null, EStatusCode.InternalServerError, $"Não foi possível encontrar RpaVmsLegados. {e.Message}");
        }
    }

    public async Task<Response<RpaVmsLegado?>> GetByRpaVmAsync(GetRpaVmsLegadoByRpaVmRequest request)
    {
        try
        {
            var rpaVmsLegado = await context.RpaVmsLegados.FirstOrDefaultAsync(x => x.IdRpaVmFk == request.IdRpaVmFk);
            return rpaVmsLegado is null 
                ? new Response<RpaVmsLegado?>(null, EStatusCode.NotFound, "RpaVmsLegado não encontrado.") 
                : new Response<RpaVmsLegado?>(rpaVmsLegado, EStatusCode.OK, "RpaVmsLegado encontrado com sucesso.");
            
        }catch(Exception e)
        {
            return new Response<RpaVmsLegado?>(null, EStatusCode.InternalServerError, $"Não foi possível encontrar um RpaVmsLegado. {e.Message}");
        }
    }


    public async Task<PagedResponse<List<RpaVmsLegado>>> GetAllAsync(GetAllRpaVmsLegadosRequest request)
    {
        try
        {
            var rpaVmsLegados = await context.RpaVmsLegados.ToListAsync();
            return new PagedResponse<List<RpaVmsLegado>>(rpaVmsLegados, EStatusCode.OK, "RpaVmsLegados encontrados com sucesso.");
        }catch(Exception e)
        {
            return new PagedResponse<List<RpaVmsLegado>>(null, EStatusCode.InternalServerError, $"Não foi possível encontrar RpaVmsLegados. {e.Message}");
        }
    }

    public async Task<Response<RpaVmsLegado?>> DeleteAsync(DeleteRpaVmsLegadoRequest request)
    {
        try
        {
            var rpaVmsLegado = await context.RpaVmsLegados.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (rpaVmsLegado is null)
                return new Response<RpaVmsLegado?>(null, EStatusCode.NotFound, "RpaVmsLegado não encontrado.");
            context.RpaVmsLegados.Remove(rpaVmsLegado);
            await context.SaveChangesAsync();
            return new Response<RpaVmsLegado?>(rpaVmsLegado, EStatusCode.OK, "RpaVmsLegado deletado com sucesso.");
        }catch(Exception e)
        {
            return new Response<RpaVmsLegado?>(null, EStatusCode.InternalServerError, $"Não foi possível deletar um RpaVmsLegado. {e.Message}");
        }
    }
}