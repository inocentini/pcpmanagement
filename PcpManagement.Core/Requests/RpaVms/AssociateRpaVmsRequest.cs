using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.RpaVms;

public class AssociateRpaVmsRequest : Request
{
    [Required]
    public int? IdProjetoFk { get; set; } = null!;
    [Required]
    public int? IdVmfk { get; set; } = null!;
    [StringLength(64)]
    public string? Funcao { get; set; } = string.Empty;
    [StringLength(32)]
    public string? Status { get; set; } = string.Empty;
    public string? Observacao { get; set; } = string.Empty;
}