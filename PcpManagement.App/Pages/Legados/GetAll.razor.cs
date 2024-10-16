using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Legados;

namespace PcpManagement.App.Pages.Legados;

public class GetAllLegadosPage : ComponentBase
{
    #region Properties
    
    public bool IsBusy { get; set; }
    protected List<Core.Models.Legados> AllLegados { get; set; } = [];
    
    protected string SearchTerm { get; set; } = string.Empty;
    
    #endregion
    
    #region Services
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    [Inject]
    public ILegadoHandler Handler { get; set; } = null!;
    
    #endregion
    
    #region Overrides
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllLegadosRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                AllLegados = result.Data ?? [];
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
    
    #region Methods

    protected async Task OnCommittedItemChanges(Core.Models.Legados item)
    {
        IsBusy = true;
        try
        {
            var request = new UpdateLegadoRequest
            {
                Id = item.Id,
                Nome = item.Nome,
                Autenticacao = item.Autenticacao,
                Acesso = item.Acesso,
                Url = item.Url,
            };
            var result = await Handler.UpdateAsync(request);
            if (result.IsSuccess)
                Snackbar.Add("Legado atualizado com sucesso", Severity.Success);
            else
                Snackbar.Add($"Erro ao atualizar legado. {result.Message}", Severity.Error);
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
    
    public async void OnDeleteButtonClickedAsync(long id, string nome)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Você tem certeza de excluir o legado '{nome}'?.",
            yesText: "Excluir",
            cancelText: "Cancelar");
        if (result is true)
            await OnDeleteAsync(id, nome);

        StateHasChanged();
    }

    private async Task OnDeleteAsync(long id, string nome)
    {
        try
        {
            var request = new DeleteLegadoRequest
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            AllLegados.RemoveAll(x=>x.Id == id);
            Snackbar.Add($"Legado '{nome}' removido", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    
    public Func<Core.Models.Legados, bool> Filter => legado =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
        if (legado.Nome!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (legado.Autenticacao!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (legado.Acesso!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (legado.Url!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };
    
    #endregion
}