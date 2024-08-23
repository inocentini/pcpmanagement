using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;

namespace PcpManagement.App.Pages.VirtualMachines;

public class GetAllVirtualMachinesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    protected List<Vm> VirtualMachines  { get; private set; } = [];
    protected string SearchTerm { get; set; } = string.Empty;
    private double Progress { get; set; } = 0;

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
        Progress = 0;
        IsBusy = true;
        try
        {
            var request = new GetAllVirtualMachinesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                VirtualMachines = result.Data ?? [];
            Progress = VirtualMachines.Count;
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
    
    
    public async Task OnUploadFileButtonClickedAsync(IBrowserFile file)
    {
        IsBusy = true;
        if(!file.Name.Split(".").Last().Equals("csv",StringComparison.OrdinalIgnoreCase)){
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add($"Extensão de arquivo errada: {file.Name.Split(".").Last()}", Severity.Error);
            return;
        }

        try
        {
            var vms = new List<Vm>();
            await using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream,Encoding.UTF8);
            using var csv = new CsvReader(reader,new CsvConfiguration(new CultureInfo("pt-BR")){HasHeaderRecord = false,Delimiter = ";"});
            await csv.ReadAsync();//cabeçalho
            while (await csv.ReadAsync())
            {
                var vm = new Vm
                {
                    Hostname = csv.GetField(index: 0)?? string.Empty,
                    Ip = csv.GetField(index: 1) ?? string.Empty,
                    UserVm = csv.GetField(index: 2),
                    VCpu = int.Parse(csv.GetField(index: 3)?? string.Empty),
                    Memoria = int.Parse(csv.GetField(index: 4)?? string.Empty),
                    Hd = int.Parse(csv.GetField(index: 5)?? string.Empty),
                    Ambiente = csv.GetField(index: 6),
                    Emprestimo = Convert.ToBoolean(int.Parse(csv.GetField(index: 7)?? string.Empty)),
                    Resolucao = csv.GetField(index: 8),
                    Enviroment = csv.GetField(index: 9),
                    SistemaOperacional = csv.GetField(index: 10),
                    Status = Convert.ToBoolean(int.Parse(csv.GetField(index: 11)?? string.Empty)),
                    Observacao = csv.GetField(index: 12),
                    Funcionalidade = csv.GetField(index: 13),
                    Lote = string.IsNullOrEmpty(csv.GetField(index: 14)) || csv.GetField(index: 14)!.Contains("null", StringComparison.OrdinalIgnoreCase) 
                        ? default
                        : DateTime.Parse(csv.GetField(index: 14)!, new CultureInfo("pt-BR")),
                    DataCenter = csv.GetField(index: 15),
                    Farm = csv.GetField(index: 16),
                };
                vms.Add(vm);
            }
            var result = await Dialog.ShowMessageBox(
                "ATENÇÃO",
                $"As máquinas virtuais já estão associadas à algum robo?.",
                yesText: "Sim",
                cancelText: "Não");
            if (result is true)
                Snackbar.Add($"Adicionar vms e associar com um robo", Severity.Success);
            else
            {
                await OnUploadFileAsync(vms);
            }
            StateHasChanged();
        }
        catch (Exception e)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add(e.Message, Severity.Error);
        }   
        finally
        {
            IsBusy = false;
        }
    }
    
    public async Task OnUploadFileAsync(List<Vm> vms)
    {
        try
        {
            var count = 0;
            foreach (var vm in vms)
            {
                
                var request = new CreateVirtualMachineRequest
                {
                    Hostname = vm.Hostname,
                    Ip = vm.Ip,
                    UserVm = vm.UserVm,
                    VCpu = vm.VCpu,
                    Memoria = vm.Memoria,
                    Hd = vm.Hd,
                    Ambiente = vm.Ambiente,
                    Emprestimo = vm.Emprestimo,
                    Resolucao = vm.Resolucao,
                    Enviroment = vm.Enviroment,
                    SistemaOperacional = vm.SistemaOperacional,
                    Status = vm.Status,
                    Observacao = vm.Observacao,
                    Funcionalidade = vm.Funcionalidade,
                    Lote = vm.Lote,
                    DataCenter = vm.DataCenter,
                    Farm = vm.Farm
                };
                var response = await Handler.CreateAsync(request);
                if (response.IsSuccess)
                    count++;
            }

            if (count == vms.Count)
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                Snackbar.Add("Máquinas virtuais inseridas com sucesso.", Severity.Success);
            }
            else
            {
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                Snackbar.Add("Ocorreu um erro na inserção de alguma máquina virtual, favor validar.", Severity.Error);
            }
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
    
    public async Task OnCommittedItemChanges(Vm item)
    {
        IsBusy = true;
        try
        {
            var request = new UpdateVirtualMachineRequest
            {
                Id = item.Id,
                Hostname = item.Hostname,
                Ip = item.Ip,
                UserVm = item.UserVm,
                VCpu = item.VCpu,
                Memoria = item.Memoria,
                Hd = item.Hd,
                Ambiente = item.Ambiente,
                Emprestimo = item.Emprestimo,
                Resolucao = item.Resolucao,
                Enviroment = item.Enviroment,
                SistemaOperacional = item.SistemaOperacional,
                Status = item.Status,
                Observacao = item.Observacao,
                Funcionalidade = item.Funcionalidade,
                Lote = item.Lote,
                DataCenter = item.DataCenter,
                Farm = item.Farm,
            };
            var result = await Handler.UpdateAsync(request);
            if (result.IsSuccess)
                Snackbar.Add("Máquina virtual atualizada", Severity.Success);
            else
                Snackbar.Add($"Erro ao atualizar máquina virtual. {result.Message}", Severity.Error);
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

    public Func<Vm, bool> Filter => vm =>
    {
        if (string.IsNullOrEmpty(SearchTerm))
            return true;
        if (vm.Hostname.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Ip.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.UserVm is not null && vm.UserVm.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.VCpu is not null && vm.VCpu.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Memoria is not null && vm.Memoria.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Hd is not null && vm.Hd.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Ambiente is not null && vm.Ambiente.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Resolucao is not null && vm.Resolucao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Enviroment is not null && vm.Enviroment.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.SistemaOperacional is not null && vm.SistemaOperacional.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Status is not null && vm.Status.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Observacao is not null && vm.Observacao.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Funcionalidade is not null && vm.Funcionalidade.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Lote is not null && vm.Lote.ToString()!.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.DataCenter is not null && vm.DataCenter.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        if (vm.Farm is not null && vm.Farm.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    };
}