using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Common.Components;

public class ListVmsWithoutAssociationComponent : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    protected List<Vm> VirtualMachines  { get; private set; } = [];

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
            var result = await Handler.GetAllWithoutAssociationAsync(new GetAllVirtualMachineWithouAssociationRequest());
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
    
    #region Methods
    
    #endregion
}