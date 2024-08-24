using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;

namespace PcpManagement.App.Common.Components;

public class ListRobosComponent : ComponentBase
{
    #region Properties
    public List<Robo> Robos { get; set; } = [];
    public IReadOnlyCollection<TreeItemData<Robo>> TreeRobos { get; set; } = [];
    
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
        try
        {
            var request = new GetAllRobosRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Robos = result.Data ?? [];
            TreeRobos = Robos.Select(x => new TreeItemData<Robo>()).ToList();
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    #endregion
    
}