using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.RpaVmsLegados;

public class CreateRpaVmsLegadoRequest : Request
{
    public int? IdLegadoFk { get; set; }
    
    public int? IdRpaVmFk { get; set; }
    
    [StringLength(16, ErrorMessage = "O campo UserLegado não pode ter mais que 16 caracteres.")]
    public string? UserLegado { get; set; }
    
    [StringLength(64, ErrorMessage = "O campo UserBanco não pode ter mais que 64 caracteres.")]
    public string? UserBanco { get; set; }
    
}