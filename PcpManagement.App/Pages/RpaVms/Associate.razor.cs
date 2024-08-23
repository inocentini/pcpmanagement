using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;

namespace PcpManagement.App.Pages.RpaVms;

public  class AssociateVmsRoboPage : ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; }
    public List<RpaVm> RpaVms { get; set; } = [];

    #endregion

    #region Services

    [Inject] 
    public IRpaVmHandler Handler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.GetAllAsync(new GetAllRpaVmsRequest());
            if (result.IsSuccess)
                RpaVms = result.Data ?? [];
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
}