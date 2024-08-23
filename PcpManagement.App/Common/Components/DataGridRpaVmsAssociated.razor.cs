using Microsoft.AspNetCore.Components;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Common.Components;

public  class RpaVmsAssociatedComponent : ComponentBase
{
    #region Properties
    [Parameter] public Robo Contexto { get; set; } = new();
    
    #endregion
    
    #region Services
    [Inject]
    public IRpaVmHandler RpaVmHandler { get; set; } = null!;
    [Inject]
    public IVirtualMachineHandler VirtualMachineHandler { get; set; } = null!;
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

}