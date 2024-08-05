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
}