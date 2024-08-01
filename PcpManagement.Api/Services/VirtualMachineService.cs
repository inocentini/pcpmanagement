using Microsoft.EntityFrameworkCore;
using PcpManagement.Api.Data;
using PcpManagement.Core.Enums;
using PcpManagement.Core.Handlers;
using PcpManagement.Core.Models;
using PcpManagement.Core.Requests.VirtualMachines;
using PcpManagement.Core.Responses;

namespace PcpManagement.Api.Services;

public class VirtualMachineService(AppDbContext context) : IVirtualMachineHandler
{
    public async Task<Response<VirtualMachine?>> CreateAsync(CreateVirtualMachineRequest request)
    {
        try
        {
            var vm = new VirtualMachine
            {
                Hostname = request.Hostname,
                Ip = request.Ip,
                UserName = request.UserName,
                VCpu = request.VCpu,
                Memoria = request.Memoria,
                HD = request.HD,
                Ambiente = request.Ambiente,
                Emprestimo = request.Emprestimo,
                Resolucao = request.Resolucao,
                Environment = request.Environment,
                SistemaOperacional = request.SistemaOperacional,
                Status = request.Status,
                Observacao = request.Observacao,
                Funcionalidade = request.Funcionalidade,
                Lote = request.Lote,
                DataCenter = request.DataCenter,
                Farm = request.Farm
            };
            await context.VirtualMachines.AddAsync(vm);
            await context.SaveChangesAsync();
            return new Response<VirtualMachine?>(vm,EStatusCode.Created,"Máquina virtual criada com sucesso.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Response<VirtualMachine?>(null, EStatusCode.InternalServerError,
                $"Não foi possível criar uma máquina virtual. {e.Message}");
        }
    }

    public async Task<Response<VirtualMachine?>> UpdateAsync(UpdateVirtualMachineRequest request)
    {
        try
        {
            var vm = await context.VirtualMachines.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (vm is null)
                return new Response<VirtualMachine?>(null, EStatusCode.NotFound, "Máquina virtual não encontrada.");
            
            vm.Hostname = request.Hostname;
            vm.Ip = request.Ip;
            vm.UserName = request.UserName;
            vm.VCpu = request.VCpu;
            vm.Memoria = request.Memoria;
            vm.HD = request.HD;
            vm.Ambiente = request.Ambiente;
            vm.Emprestimo = request.Emprestimo;
            vm.Resolucao = request.Resolucao;
            vm.Environment = request.Environment;
            vm.SistemaOperacional = request.SistemaOperacional;
            vm.Status = request.Status;
            vm.Observacao = request.Observacao;
            vm.Funcionalidade = request.Funcionalidade;
            vm.Lote = request.Lote;
            vm.DataCenter = request.DataCenter;
            vm.Farm = request.Farm;

            context.VirtualMachines.Update(vm);
            await context.SaveChangesAsync();

            return new Response<VirtualMachine?>(vm, message:"Máquina virtual atualizada com sucesso.");
        }
        catch (Exception ex)
        {
            return new Response<VirtualMachine?>(null, EStatusCode.InternalServerError,
                $"Falha na atualização da máquina virtual. {ex.Message}");
        }
    }

    public async Task<Response<VirtualMachine?>> DeleteAsync(DeleteVirtualMachineRequest request)
    {
        try
        {
            var vm = await context.VirtualMachines.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (vm is null)
                return new Response<VirtualMachine?>(null, EStatusCode.NotFound, "Máquina virtual não encontrada.");
            context.VirtualMachines.Remove(vm);
            await context.SaveChangesAsync();

            return new Response<VirtualMachine?>(vm, message: "Máquina virtual excluída com sucesso.");
        }
        catch (Exception e)
        {
            return new Response<VirtualMachine?>(null, EStatusCode.InternalServerError,
                $"Não foi possível excluir a Máquina virtual. {e.Message}");
        }
    }

    public async Task<Response<VirtualMachine?>> GetByIdAsync(GetVirtualMachineByIdRequest request)
    {
        try
        {
            var vm = await context.VirtualMachines.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id); // UsarAsNoTrackingWithIdentityResolution? 
            return vm is null
                ? new Response<VirtualMachine?>(null, EStatusCode.NotFound, "Máquina virtual não encontrada.")
                : new Response<VirtualMachine?>(vm);
        }
        catch (Exception e)
        {
            return new Response<VirtualMachine?>(null, EStatusCode.InternalServerError,
                $"Não foi possível localizar a Máquina virtual. {e.Message}");
        }
    }
    public async Task<PagedResponse<List<VirtualMachine>?>> GetAllAsync(GetAllVirtualMachinesRequest request)
    {
        try
        {
            var query = context.VirtualMachines.AsNoTracking().OrderBy(x => x.Hostname);
            var vms = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return new PagedResponse<List<VirtualMachine>?>(vms, count, request.PageNumber, request.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<VirtualMachine>?>(null, EStatusCode.InternalServerError,
                $"Não foi listar todas as Máquinas virtuais. {e.Message}");
        }
    }
}