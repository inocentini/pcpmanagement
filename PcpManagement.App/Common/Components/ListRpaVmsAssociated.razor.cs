using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Common.Components;

public class ListRpaVmsAssociatedComponent : ComponentBase
{
    #region Properties
    [Parameter] public Robo Contexto { get; set; } = new();
    
    #endregion
    
    #region Services
    [Inject]
    public IRpaVmHandler RpaVmHandler { get; set; } = null!;
    [Inject]
    public IVirtualMachineHandler VirtualMachineHandler { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var rpaVms =  await RpaVmHandler.GetVmsByRoboAsync(new GetAllVmsByRoboRequest{idRoboFK = Contexto.Id});
        if (rpaVms.IsSuccess)
            foreach (var rpaVm in rpaVms.Data!)
            {
                var response = await VirtualMachineHandler.GetByIdAsync(new GetVirtualMachineByIdRequest { Id = rpaVm.IdVmfk });
                if (response.IsSuccess)
                    rpaVm.IdVmfkNavigation = response.Data;
            }
        Contexto.RpaVms = rpaVms.Data ?? [];
        await InvokeAsync(StateHasChanged);
    }

    #endregion
    
    #region Methods
    
    protected async void OnVmDropped(MudItemDropInfo<RpaVm> dropInfo)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Você tem certeza que quer incluir a vm '{dropInfo.Item!.IdVmfkNavigation!.Hostname}' no projeto: '{Contexto.Projeto}'?.",
            yesText: "Sim",
            cancelText: "Cancelar");
        if (result is true)
            await OnVmDroppedAsync(dropInfo.Item);

        StateHasChanged();
    }

    private async Task OnVmDroppedAsync(RpaVm rpaVm)
    {
        var request = new AssociateRpaVmsRequest
        {
            IdProjetoFk = Contexto.Id,
            IdVmfk = rpaVm.IdVmfk,
            Funcao = rpaVm.Funcao,
            Status = rpaVm.Status,
            Observacao = rpaVm.Observacao
        };
        var result = await RpaVmHandler.AssociateAsync(request);
        if (result.IsSuccess)
        {
            Snackbar.Add(result.Message, Severity.Success);
            Contexto.RpaVms!.Add(result.Data!);
        }
        else
        {
            Snackbar.Add(result.Message, Severity.Error);
        }
    }
    
    #endregion
}