using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.App.Common.Components;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;

namespace PcpManagement.App.Pages.Robos;

public class GetAllRobosPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    protected List<Robo> Robos { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;
    protected DataGridRpaVmsAssociated RpaVmsComponent { get; set; } = null!;


    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    [Inject]
    public IRoboHandler Handler { get; set; } = null!;
    
    #endregion

    #region Overrides
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllRobosRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Robos = result.Data ?? [];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    

    #endregion


    protected async Task OnCommittedItemChanges(Robo item)
    {
        IsBusy = true;
        try
        {
            var request = new UpdateRoboRequest
            {
                Id = item.Id,
                IdProjeto = item.IdProjeto,
                Pti = item.Pti,
                Projeto = item.Projeto,
                Descricao = item.Descricao,
                Scrum = item.Scrum,
                Gerencia = item.Gerencia,
                Equipe = item.Equipe,
                Aplicacao = item.Aplicacao,
                Torre = item.Torre,
                SuporteFabrica = item.SuporteFabrica,
                KtDate = item.KtDate,
                ProjetoEm = item.ProjetoEm,
                ClusterAplicacao = item.ClusterAplicacao,
                BancodeDados = item.BancodeDados,
                NumCrq = item.NumCrq,
                StatusCrq = item.StatusCrq,
                BlindagemDate = item.BlindagemDate,
                QtdLicenca = item.QtdLicenca
            };
            var result = await Handler.UpdateAsync(request);
            if (result.IsSuccess)
                Snackbar.Add("Robo atualizado com sucesso.", Severity.Success);
            else
                Snackbar.Add($"Erro ao atualizar robo. {result.Message}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }

    }
    public async void OnDeleteButtonClickedAsync(long id, string? projeto)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Você tem certeza de excluir o robo {projeto ?? string.Empty}?.",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, projeto?? string.Empty);

        StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string projeto)
    {
        try
        {
            var request = new DeleteRoboRequest()
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            Robos.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Robo {projeto} removido com sucesso.", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<Robo, bool> Filter => robo =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
        if (robo.Projeto!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Pti.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.IdProjeto.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Descricao is not null && robo.Descricao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Scrum is not null && robo.Scrum.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Gerencia is not null && robo.Gerencia.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Equipe is not null && robo.Equipe.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Aplicacao is not null && robo.Aplicacao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.Torre is not null && robo.Torre.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.SuporteFabrica is not null && robo.SuporteFabrica.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.KtDate is not null && robo.KtDate.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.ProjetoEm is not null && robo.ProjetoEm.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.ClusterAplicacao is not null && robo.ClusterAplicacao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.BancodeDados is not null && robo.BancodeDados.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.NumCrq is not null && robo.NumCrq.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.StatusCrq is not null && robo.StatusCrq.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (robo.BlindagemDate is not null && robo.BlindagemDate.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if(robo.QtdLicenca is not null && robo.QtdLicenca.ToString()!.Contains(SearchTerm,StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };
}