using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcpManagement.Core.Models;

[Table("vms", Schema = "efetividadeRobotica")]
public class Vm
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("hostname")]
    [StringLength(16, ErrorMessage = "")]
    public string Hostname { get; set; } = null!;

    [Column("ip")]
    [StringLength(16)]
    public string Ip { get; set; } = null!;

    [Column("userVM")]
    [StringLength(16)]
    public string? UserVm { get; set; }

    [Column("vCPU")]
    public int? VCpu { get; set; }

    [Column("memoria")]
    public int? Memoria { get; set; }

    [Column("hd")]
    public int? Hd { get; set; }

    [Column("ambiente")]
    [StringLength(32)]
    public string? Ambiente { get; set; }

    [Column("emprestimo")]
    public bool? Emprestimo { get; set; }

    [Column("resolucao")]
    [StringLength(16)]
    public string? Resolucao { get; set; }

    [Column("enviroment")]
    [StringLength(16)]
    public string? Enviroment { get; set; }

    [Column("sistemaOperacional")]
    [StringLength(32)]
    public string? SistemaOperacional { get; set; }

    [Column("status")]
    public bool? Status { get; set; }

    [Column("observacao")]
    
    public string? Observacao { get; set; }

    [Column("funcionalidade")]
    [StringLength(64)]
    public string? Funcionalidade { get; set; }

    [Column("lote")]
    public DateTime? Lote { get; set; }

    [Column("dataCenter")]
    [StringLength(32)]
    public string? DataCenter { get; set; }

    [Column("farm")]
    [StringLength(64)]
    public string? Farm { get; set; }

    [InverseProperty("IdVmfkNavigation")]
    public virtual ICollection<RpaVm> RpaVms { get; set; } = new List<RpaVm>();
}
