using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Requests.Robos;

namespace PcpManagement.App.Pages.Robos;

public class CreateRoboPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public CreateRoboRequest Request { get; set; } = new();

    #endregion

    #region Services
    [Inject] public IRoboHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Overrides

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;
        try
        {
            var result = await Handler.CreateAsync(Request);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/robos");
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
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