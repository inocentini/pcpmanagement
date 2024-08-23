using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class RoboService(RpaContext context) : IRoboHandler
{
    public async Task<Response<Robo?>> CreateAsync(CreateRoboRequest request)
    {
        try
        {
            var robo = new Robo
            {
                IdProjeto = request.IdProjeto,
                Pti = request.Pti,
                Projeto = request.Projeto,
                Descricao = request.Descricao,
                Scrum = request.Scrum,
                Gerencia = request.Gerencia,
                Equipe = request.Equipe,
                Aplicacao = request.Aplicacao,
                Torre = request.Torre,
                SuporteFabrica = request.SuporteFabrica,
                KtDate = request.KtDate,
                ProjetoEm = request.ProjetoEm,
                ClusterAplicacao = request.ClusterAplicacao,
                BancodeDados = request.BancodeDados,
                NumCrq = request.NumCrq,
                StatusCrq = request.StatusCrq,
                BlindagemDate = request.BlindagemDate,
                QtdLicenca = request.QtdLicenca,
            };
            await context.Robos.AddAsync(robo);
            await context.SaveChangesAsync();
            return new Response<Robo?>(robo,EStatusCode.Created,"Robo criado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Robo?>(null, EStatusCode.InternalServerError,
                $"Não foi possível criar um robo. {e.Message}");
        }
    }

    public async Task<Response<Robo?>> UpdateAsync(UpdateRoboRequest request)
    {
        try
        {
            var robo = await context.Robos.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (robo is null)
                return new Response<Robo?>(null, EStatusCode.NotFound, "Robo não encontrado.");
            robo.IdProjeto = request.IdProjeto;
            robo.Pti = request.Pti;
            robo.Projeto = request.Projeto;
            robo.Descricao = request.Descricao;
            robo.Scrum = request.Scrum;
            robo.Gerencia = request.Gerencia;
            robo.Equipe = request.Equipe;
            robo.Aplicacao = request.Aplicacao;
            robo.Torre = request.Torre;
            robo.SuporteFabrica = request.SuporteFabrica;
            robo.KtDate = request.KtDate;
            robo.ProjetoEm = request.ProjetoEm;
            robo.ClusterAplicacao = request.ClusterAplicacao;
            robo.BancodeDados = request.BancodeDados;
            robo.NumCrq = request.NumCrq;
            robo.StatusCrq = request.StatusCrq;
            robo.BlindagemDate = request.BlindagemDate;
            robo.QtdLicenca = request.QtdLicenca;

            context.Robos.Update(robo);
            await context.SaveChangesAsync();

            return new Response<Robo?>(robo, message: "Robo atualizado com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Robo?>(null, EStatusCode.InternalServerError,
                $"Falha na atualização de um robo. {e.Message}");
        }
    }

    public async Task<Response<Robo?>> DeleteAsync(DeleteRoboRequest request)
    {
        try
        {
            var robo = await context.Robos.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (robo is null)
                return new Response<Robo?>(null, EStatusCode.NotFound, "Robo não encontrado.");
            context.Robos.Remove(robo);
            await context.SaveChangesAsync();
            return new Response<Robo?>(robo, message: "Robo excluído com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Robo?>(null, EStatusCode.InternalServerError,
                $"Falha na exclusão do robo. {e.Message}");
        }
    }

    public async Task<Response<Robo?>> GetByIdAsync(GetRoboByIdRequest request)
    {
        try
        {
            var robo = await context.Robos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id); // UsarAsNoTrackingWithIdentityResolution? 
            return robo is null
                ? new Response<Robo?>(null, EStatusCode.NotFound, "Robo não encontrado.")
                : new Response<Robo?>(robo);
        }
        catch (Exception e)
        {
            return new Response<Robo?>(null, EStatusCode.InternalServerError,
                $"Falha em buscar robo. {e.Message}");
        }
    }

    public async Task<PagedResponse<List<Robo>>> GetAllAsync(GetAllRobosRequest request)
    {
        try
        {
            var query = context.Robos.AsNoTracking().OrderBy(x => x.Projeto);
            var robos = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return new PagedResponse<List<Robo>>(robos, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Robo>>(null, EStatusCode.InternalServerError,
                $"Não foi listar todos os robos. {e.Message}");
        }
    }
}