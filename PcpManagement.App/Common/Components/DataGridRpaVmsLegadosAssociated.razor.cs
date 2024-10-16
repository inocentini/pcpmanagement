using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Legados;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Requests.RpaVmsLegados;

namespace PcpManagement.App.Common.Components;

public class RpaVmsLegadosAssociatedComponent : ComponentBase
{
    #region Properties
    [Parameter] public Vm Contexto { get; set; } = new();
    
     
    #endregion
    
    #region Services
    [Inject]
    public IRpaVmHandler RpaVmHandler { get; set; } = null!;
    [Inject]
    public IRpaVmsLegadoHandler RpaVmsLegadoHandler { get; set; } = null!;
    [Inject]
    public ILegadoHandler LegadoHandler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    [Inject]
    public NavigationManager Navigation { get; set; } = null!;
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var rpaVms =  await RpaVmHandler.GetAllByVmIdFkAsync(new GetAllRpaVmsByVmIdFkRequest{ IdVmFk = Contexto.Id});
        if (rpaVms.IsSuccess && rpaVms.Data != null)
        {
            foreach (var rpaVm in rpaVms.Data)
            {
                var response = await RpaVmsLegadoHandler.GetAllByRpaVmAsync(new GetAllRpaVmsLegadoByRpaVmRequest { IdRpaVmFk = rpaVm.Id });
                if (response is { IsSuccess: true, Data: not null }) rpaVm.RpaVmsLegados = response.Data ?? [];
                foreach (var rpaVmLegado in rpaVm.RpaVmsLegados)
                {
                    var response2 = await LegadoHandler.GetByIdAsync(new GetLegadoByIdRequest { Id = rpaVmLegado.IdLegadoFk });
                    if (response2.IsSuccess)
                    {
                        rpaVmLegado.IdLegadoFkNavigation = response2.Data;
                    }
                }
            }
        }
        Contexto.RpaVms = rpaVms.Data ?? [];
        await InvokeAsync(StateHasChanged);
    }

    #endregion
}