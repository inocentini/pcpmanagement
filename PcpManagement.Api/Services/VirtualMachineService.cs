using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Common.Enum;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class VirtualMachineService(RpaContext context) : IVirtualMachineHandler
{
    public async Task<Response<Vm?>> CreateAsync(CreateVirtualMachineRequest request)
    {
        try
        {
            var vm = new Vm
            {
                Hostname = request.Hostname,
                Ip = request.Ip,
                UserVm = request.UserVm,
                VCpu = request.VCpu,
                Memoria = request.Memoria,
                Hd = request.Hd,
                Ambiente = request.Ambiente,
                Emprestimo = request.Emprestimo,
                Resolucao = request.Resolucao,
                Enviroment = request.Enviroment,
                SistemaOperacional = request.SistemaOperacional,
                Status = request.Status,
                Observacao = request.Observacao,
                Funcionalidade = request.Funcionalidade,
                Lote = request.Lote,
                DataCenter = request.DataCenter,
                Farm = request.Farm
            };
            await context.Vms.AddAsync(vm);
            await context.SaveChangesAsync();
            return new Response<Vm?>(vm,code: EStatusCode.Created,"Máquina virtual criada com sucesso.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Response<Vm?>(null, code: EStatusCode.InternalServerError,
                $"Não foi possível criar uma máquina virtual. {e.Message}");
        }
    }

    public async Task<Response<Vm?>> UpdateAsync(UpdateVirtualMachineRequest request)
    {
        try
        {
            var vm = await context.Vms.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (vm is null)
                return new Response<Vm?>(null, code: EStatusCode.NotFound, "Máquina virtual não encontrada.");
            
            vm.Hostname = request.Hostname;
            vm.Ip = request.Ip;
            vm.UserVm = request.UserVm;
            vm.VCpu = request.VCpu;
            vm.Memoria = request.Memoria;
            vm.Hd = request.Hd;
            vm.Ambiente = request.Ambiente;
            vm.Emprestimo = request.Emprestimo;
            vm.Resolucao = request.Resolucao;
            vm.Enviroment = request.Enviroment;
            vm.SistemaOperacional = request.SistemaOperacional;
            vm.Status = request.Status;
            vm.Observacao = request.Observacao;
            vm.Funcionalidade = request.Funcionalidade;
            vm.Lote = request.Lote;
            vm.DataCenter = request.DataCenter;
            vm.Farm = request.Farm;

            context.Vms.Update(vm);
            await context.SaveChangesAsync();

            return new Response<Vm?>(vm,message:"Máquina virtual atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return new Response<Vm?>(null,code: EStatusCode.InternalServerError,
                $"Falha na atualização da máquina virtual. {ex.Message}");
        }
    }

    public async Task<Response<Vm?>> DeleteAsync(DeleteVirtualMachineRequest request)
    {
        try
        {
            var vm = await context.Vms.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (vm is null)
                return new Response<Vm?>(null, code: EStatusCode.NotFound, "Máquina virtual não encontrada.");
            context.Vms.Remove(vm);
            await context.SaveChangesAsync();

            return new Response<Vm?>(vm, message: "Máquina virtual excluída com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<Vm?>(null, code: EStatusCode.InternalServerError,
                $"Não foi possível excluir a Máquina virtual. {e.Message}");
        }
    }

    public async Task<Response<Vm?>> GetByIdAsync(GetVirtualMachineByIdRequest request)
    {
        try
        {
            var vm = await context.Vms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id); // UsarAsNoTrackingWithIdentityResolution? 
            return vm is null
                ? new Response<Vm?>(null, code: EStatusCode.NotFound, "Máquina virtual não encontrada.")
                : new Response<Vm?>(vm);
        }
        catch (Exception e)
        {
            return new Response<Vm?>(null, code: EStatusCode.InternalServerError,
                $"Não foi possível localizar a Máquina virtual. {e.Message}");
        }
    }
    public async Task<PagedResponse<List<Vm>>> GetAllAsync(GetAllVirtualMachinesRequest request)
    {
        try
        {
            var query = context.Vms.AsNoTracking().OrderBy(x => x.Hostname);
            var vms = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return new PagedResponse<List<Vm>>(vms, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<Vm>>(null, code: EStatusCode.InternalServerError,
                $"Não foi listar todas as Máquinas virtuais. {e.Message}");
        }
    }
}