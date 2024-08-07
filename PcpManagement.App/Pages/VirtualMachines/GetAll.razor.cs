using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Pages.VirtualMachines;

public partial class GetAllVirtualMachinesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Vm> VirtualMachines  { get; set; } = [];
    public string SearchTerm { get; set; } = string.Empty;

    #endregion

    #region Services
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    [Inject]
    public IVirtualMachineHandler Handler { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllVirtualMachinesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                VirtualMachines = result.Data ?? [];
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
    
    
    
    public async void OnDeleteButtonClickedAsync(long id, string hostname)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Você tem certeza de excluir a máquina virtual '{hostname}'?.",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, hostname);

        StateHasChanged();
    }
    
    public async Task OnCommittedItemChanges(Vm item)
    {
        IsBusy = true;
        try
        {
            var request = new UpdateVirtualMachineRequest
            {
                Id = item.Id,
                Hostname = item.Hostname,
                Ip = item.Ip,
                UserVm = item.UserVm,
                VCpu = item.VCpu,
                Memoria = item.Memoria,
                Hd = item.Hd,
                Ambiente = item.Ambiente,
                Emprestimo = item.Emprestimo,
                Resolucao = item.Resolucao,
                Enviroment = item.Enviroment,
                SistemaOperacional = item.SistemaOperacional,
                Status = item.Status,
                Observacao = item.Observacao,
                Funcionalidade = item.Funcionalidade,
                Lote = item.Lote,
                DataCenter = item.DataCenter,
                Farm = item.Farm,
            };
            var result = await Handler.UpdateAsync(request);
            if (result.IsSuccess)
                Snackbar.Add("Máquina virtual atualizada", Severity.Success);
            else
                Snackbar.Add($"Erro ao atualizar máquina virtual. {result.Message}", Severity.Error);
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
    public async Task OnDeleteAsync(long id, string hostname)
    {
        try
        {
            var request = new DeleteVirtualMachineRequest()
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            VirtualMachines.RemoveAll(x=>x.Id == id);
            Snackbar.Add($"Máquina virtual '{hostname}' removida", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public Func<Vm, bool> Filter => vm =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
        if (vm.Hostname.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Ip.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.UserVm is not null && vm.UserVm.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.VCpu is not null && vm.VCpu.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Memoria is not null && vm.Memoria.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Hd is not null && vm.Hd.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Ambiente is not null && vm.Ambiente.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Resolucao is not null && vm.Resolucao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Enviroment is not null && vm.Enviroment.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.SistemaOperacional is not null && vm.SistemaOperacional.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Status is not null && vm.Status.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Observacao is not null && vm.Observacao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Funcionalidade is not null && vm.Funcionalidade.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Lote is not null && vm.Lote.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.DataCenter is not null && vm.DataCenter.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Farm is not null && vm.Farm.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };
}