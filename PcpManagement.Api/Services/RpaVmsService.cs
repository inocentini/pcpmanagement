using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class RpaVmsService(RpaContext context) : IRpaVmHandler
{
    public async Task<Response<RpaVm?>> AssociateAsync(AssociateRpaVmsRequest request)
    {
        try
        {
            var rpaVm = new RpaVm
            {
                IdProjetoFk = request.IdProjetoFk,
                IdVmfk = request.IdVmfk,
                Funcao = request.Funcao,
                Status = request.Status,
                Observacao = request.Observacao
            };
            await context.RpaVms.AddAsync(rpaVm);
            await context.SaveChangesAsync();
            return new Response<RpaVm?>(rpaVm,EStatusCode.Created,"Associação feita com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<RpaVm?>(null,EStatusCode.InternalServerError,$"Não foi possível associar o robo com a vm.{e.Message}");
        }
    }
    public async Task<Response<RpaVm?>> UpdateAsync(UpdateRpaVmsRequest request)
    {
        try
        {
            var rpavm = await context.RpaVms.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (rpavm is null)
                return new Response<RpaVm?>(null, EStatusCode.NotFound, "Associação não encontrada.");
            rpavm.IdProjetoFk = request.IdProjetoFk;
            rpavm.IdVmfk = request.IdVmfk;
            rpavm.Funcao = request.Funcao;
            rpavm.Status = request.Status;
            rpavm.Observacao = request.Observacao;
            context.RpaVms.Update(rpavm);
            await context.SaveChangesAsync();

            return new Response<RpaVm?>(rpavm, message: "Associação atualizada com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<RpaVm?>(null,EStatusCode.InternalServerError,$"Não foi possível atualizar a associação.{e.Message}");
        }
    }

    public async Task<Response<RpaVm?>> GetByVmIdFkAsync(GetRpaVmByVmIdFkRequest request)
    {
        try
        {
            var rpavm = await context.RpaVms
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IdVmfk == request.idVmFk);
            return rpavm is null
                ? new Response<RpaVm?>(null, EStatusCode.NotFound, "Associação não encontrada.")
                : new Response<RpaVm?>(rpavm, message: "Associação encontrada.");
        }
        catch (Exception e)
        {
            return new Response<RpaVm?>(null,EStatusCode.InternalServerError,$"Não foi possível localizar a associação.{e.Message}");
        }
    }

    public async Task<PagedResponse<List<RpaVm>?>> GetAllByVmIdFkAsync(GetAllRpaVmsByVmIdFkRequest request)
    {
        try
        {
            var rpavms = await context.RpaVms
                .AsNoTracking()
                .Where(x => x.IdVmfk == request.IdVmFk)
                .ToListAsync();
            return new PagedResponse<List<RpaVm>?>(rpavms);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<RpaVm>?>(null, EStatusCode.InternalServerError,
                $"Não foi possível listar todas as associações da máquina virtual. {e.Message}");
        }
    }

    public async Task<PagedResponse<List<RpaVm>?>> GetVmsByRoboAsync(GetAllVmsByRoboRequest request)
    {
        try
        {
            var rpavms = await context.RpaVms
                .AsNoTracking()
                .Where(x => x.IdProjetoFk == request.idRoboFK)
                .ToListAsync();
            return new PagedResponse<List<RpaVm>?>(rpavms);

        }
        catch (Exception e)
        {
            return new PagedResponse<List<RpaVm>?>(null,EStatusCode.InternalServerError,$"Não foi possível listar todas as associações do robo. {e.Message}");
        }
    }

    public async Task<PagedResponse<List<RpaVm>>> GetAllAsync(GetAllRpaVmsRequest request)
    {
        try
        {
            var query = context.RpaVms.AsNoTracking().OrderBy(x => x.Id);
            var rpavms = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return new PagedResponse<List<RpaVm>>(rpavms, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<RpaVm>>(null, code: EStatusCode.InternalServerError,
                $"Não foi listar todas as associações. {e.Message}");
        }
    }

    public async Task<Response<RpaVm?>> DeleteAsync(DeleteRpaVmsRequest request)
    {
        try
        {
            var rpavm = await context.RpaVms.FirstOrDefaultAsync(x=> x.Id == request.Id);
            if(rpavm is null)
                return new Response<RpaVm?>(null,EStatusCode.NotFound,$"Não foi possível localizar associação.");
            context.RpaVms.Remove(rpavm);
            await context.SaveChangesAsync();
            return new Response<RpaVm?>(rpavm,message:"Associação desfeita com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<RpaVm?>(null,EStatusCode.InternalServerError,$"Não foi possível desassociar uma máquina virtual com um robo. {e.Message}");
        }
    }
}