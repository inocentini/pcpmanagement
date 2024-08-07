using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.VirtualMachines;

public class UpdateVirtualMachineRequest : Request
{
    
    [Required]
    [StringLength(16, ErrorMessage = "O Hostname não pode ter mais que 16 caracteres.")]
    public string Hostname { get; set; } = null!;

    [Required]
    [StringLength(16, ErrorMessage = "O ip não pode ter mais que 16 caracteres")]
    public string Ip { get; set; } = null!;
    
    [StringLength(16, ErrorMessage = "O UserName não pode ter mais que 16 caracteres")]
    public string? UserVm { get; set; }
    
    public int? VCpu { get; set; }
    
    public int? Memoria { get; set; }
    
    public int? Hd { get; set; }
    
    [StringLength(32, ErrorMessage = "O Ambiente não pode ter mais que 32 caracteres")]
    public string? Ambiente { get; set; }
    
    public bool? Emprestimo { get; set; }
    
    [StringLength(16, ErrorMessage = "A resolução não pode ter mais que 16 caracteres")]
    public string? Resolucao { get; set; }
    
    [StringLength(16, ErrorMessage = "O environment não pode ter mais que 16 caracteres")]
    public string? Enviroment { get; set; }
    
    [StringLength(32, ErrorMessage = "O sistema operacional não pode ter mais que 32 caracteres")]
    public string? SistemaOperacional { get; set; }
    
    public bool? Status { get; set; }
    
    public string? Observacao { get; set; }
    
    [StringLength(64, ErrorMessage = "A funcionalidade não pode ter mais que 64 caracteres")]
    public string? Funcionalidade { get; set; }
    
    public DateTime? Lote { get; set; }

    [StringLength(32, ErrorMessage = "O data center não pode ter mais que 32 caracteres")]
    public string? DataCenter { get; set; }

    [StringLength(64, ErrorMessage = "A farm não pode ter mais que 64 caracteres")]
    public string? Farm { get; set; }
}