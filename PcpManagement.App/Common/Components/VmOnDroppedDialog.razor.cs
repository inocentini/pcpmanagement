using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Models;

namespace PcpManagement.App.Common.Components;

public class VmOnDroppedDialogComponent : ComponentBase
{
    #region Properties
    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public Vm Vm { get; set; } = null!;
    public string Funcao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    #endregion
    
    #region Services
    [Inject] public IDialogService DialogService { get; set; } = null!;
    #endregion

    #region Methods

    protected void Submit()
    {
        MudDialog.Close(DialogResult.Ok(new VmOnDroppedDialogComponent
        {
            Funcao = Funcao,
            Status = Status,
            Observacao = Observacao
        }));
    }

    protected void Cancel()
    {
        MudDialog.Cancel();
    }
    #endregion
}