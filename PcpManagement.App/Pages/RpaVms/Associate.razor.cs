using Microsoft.AspNetCore.Components;
using MudBlazor;
using PcpManagement.App.Common.Components;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.Robos;
using PcpManagement.Core.Requests.RpaVms;
using PcpManagement.Core.Requests.RpaVmsLegados;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Pages.RpaVms;

public  class AssociateVmsRoboPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    protected List<VmEx> Vms { get; private set; } = [];
    protected Robo Contexto { get; private set; } = new();
    [Parameter] public int Id { get; set; }

    #endregion

    #region Services

    [Inject] 
    public IRpaVmHandler Handler { get; set; } = null!;
    [Inject]
    public IVirtualMachineHandler VmHandler { get; set; } = null!;
    [Inject]
    public IRoboHandler RoboHandler { get; set; } = null!;
    
    [Inject]
    public IRpaVmsLegadoHandler RpaVmsLegadoHandler { get; set; } = null!;
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
            var roboResult = await RoboHandler.GetByIdAsync(new GetRoboByIdRequest { Id = Id });
            if (roboResult.IsSuccess)
                Contexto = roboResult.Data!;
            await AtualizaVms();
            await InvokeAsync(StateHasChanged);
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

    protected async void OnVmDropped(MudItemDropInfo<VmEx> dropItem)
    {
        IsBusy = true;
        try
        {
            if(dropItem.Item!.Identifier == "Associated")
                await OnVmDroppedToVmListAsync(dropItem.Item);
            else
                await OnVmDroppedToRoboAsync(dropItem.Item);
            await AtualizaVms();
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception e)
        {
           Snackbar.Add(e.Message, Severity.Error);
        }finally
        {
            IsBusy = false;
        }
    }

    private async Task OnVmDroppedToRoboAsync(Vm vm)
    {
        try
        {
            var parameters = new DialogParameters { { "Vm", vm } };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = await Dialog.ShowAsync<VmOnDroppedDialog>("Associar VM", parameters, options);
            var dialogResult = await dialog.Result;

            if (dialogResult!.Canceled)
                return;
            var dialogData = (VmOnDroppedDialogComponent)dialogResult.Data!;
            var request = new AssociateRpaVmsRequest
            {
                IdProjetoFk = Contexto.Id,
                IdVmfk = vm.Id,
                Funcao = dialogData.Funcao,
                Status = dialogData.Status,
                Observacao = dialogData.Observacao
            };
            var associateResult = await Handler.AssociateAsync(request);
            if (associateResult.IsSuccess)
            {
                var vmToUpdate = Vms.FirstOrDefault(x => x.Id == vm.Id);
                if (vmToUpdate != null)
                    vmToUpdate.Identifier = "Associated";
            }
            Snackbar.Add(associateResult.Message, associateResult.IsSuccess ? Severity.Success : Severity.Error);
            await Task.CompletedTask;
            
        }
        catch (Exception e)
        {
            Snackbar.Add($"Não foi possível associar vm {vm.Hostname} com robo {Contexto.Projeto}. {e.Message}", Severity.Error);
        }
    }
    
    private async Task OnVmDroppedToVmListAsync(VmEx vm)
    {
        try
        {
            var result = await Dialog.ShowMessageBox(
                "ATENÇÃO",
                $"Você tem certeza que quer desassociar a vm {vm.Hostname} no robô {Contexto.Projeto}?.",
                yesText: "Sim",
                cancelText: "Não");

            if (!result is true)
                return;
            var checkAssociated = await CheckRpaVmsLegadoAssociated(vm.Id);
            if (checkAssociated) 
                return;
            var request = new DeleteRpaVmsRequest()
            {
                Id = Contexto.RpaVms!.FirstOrDefault(x => x.IdVmfk == vm.Id)!.Id
                
            };
            var disassociateResult = await Handler.DeleteAsync(request);
            if (disassociateResult.IsSuccess)
            {
                var vmToUpdate = Vms.FirstOrDefault(x => x.Id == vm.Id);
                if (vmToUpdate != null)
                    vmToUpdate.Identifier = "Not Associated";
            }
            Snackbar.Add(disassociateResult.Message, disassociateResult.IsSuccess ? Severity.Success : Severity.Error);
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            Snackbar.Add($"Não foi possível desassociar vm {vm.Hostname} com robo {Contexto.Projeto}. {e.Message}", Severity.Error);
        }
    }
    
    private async Task<bool> CheckRpaVmsLegadoAssociated(int id)
    {
        try
        {
            var rpaVmLegado = await RpaVmsLegadoHandler.GetByRpaVmAsync(new GetRpaVmsLegadoByRpaVmRequest { IdRpaVmFk = id });
            if (!rpaVmLegado.IsSuccess) return false;
            Snackbar.Add("Não é possível desassociar uma VM que está associada a um RpaVmLegado. Para desassociar, primeiro atualize a associação de rpaVmLegado.", Severity.Error);
            return true;
        }catch (Exception e)
        {
            Snackbar.Add($"Erro ao verificar se a vm {id} está associada a um rpaVmLegado. {e.Message}", Severity.Error);
            return false;
        }
    }

    private async Task AtualizaVms()
    {
        try
        {
            Vms.Clear();
            var vmsResult = await VmHandler.GetAllWithoutAssociationAsync(new GetAllVirtualMachineWithouAssociationRequest(){PageNumber = 1, PageSize = 1000});
            if (vmsResult.IsSuccess)
                Vms = vmsResult.Data!.Select(vm => new VmEx(vm, "Not Associated")).ToList();
            var rpaVms =  await Handler.GetVmsByRoboAsync(new GetAllVmsByRoboRequest{idRoboFK = Contexto.Id});
            if (rpaVms.IsSuccess)
                foreach (var rpaVm in rpaVms.Data!)
                {
                    var response = await VmHandler.GetByIdAsync(new GetVirtualMachineByIdRequest { Id = rpaVm.IdVmfk });
                    if (!response.IsSuccess) continue;
                    rpaVm.IdVmfkNavigation = response.Data;
                    Vms.Add(new VmEx(rpaVm.IdVmfkNavigation!, "Associated"));
                }
            Contexto.RpaVms = rpaVms.Data ?? [];
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            Snackbar.Add($"Erro ao atualizar vms. {e.Message}", Severity.Error);
        }
    }

    #endregion

    #region Nested Classes
    protected class VmEx : Vm
    {
        public string? Identifier { get; set; }
        
        public VmEx(Vm vm, string identifier)
        {
            Id = vm.Id;
            Hostname = vm.Hostname;
            Ip = vm.Ip;
            UserVm = vm.UserVm;
            VCpu = vm.VCpu;
            Memoria = vm.Memoria;
            Hd = vm.Hd;
            Ambiente = vm.Ambiente;
            Emprestimo = vm.Emprestimo;
            Resolucao = vm.Resolucao;
            Enviroment = vm.Enviroment;
            SistemaOperacional = vm.SistemaOperacional;
            Status = vm.Status;
            Observacao = vm.Observacao;
            Funcionalidade = vm.Funcionalidade;
            Lote = vm.Lote;
            DataCenter = vm.DataCenter;
            Farm = vm.Farm;
            //RpaVms = vm.RpaVms;
            Identifier = identifier;
        }
    }
    #endregion

}