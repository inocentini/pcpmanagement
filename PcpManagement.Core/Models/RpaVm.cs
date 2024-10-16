using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcpManagement.Core.Models;

[Table("rpaVms", Schema = "efetividadeRobotica")]
public class RpaVm
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idProjetoFK")]
    public int? IdProjetoFk { get; set; }
    
    [Column("idVMFK")]
    public int? IdVmfk { get; set; }

    [Column("funcao")]
    [StringLength(64)]
    public string? Funcao { get; set; }

    [Column("status")]
    [StringLength(32)]
    public string? Status { get; set; }

    [Column("observacao")]
    public string? Observacao { get; set; }

    [ForeignKey("IdProjetoFk")]
    [InverseProperty("RpaVms")]
    public virtual Robo? IdProjetoFkNavigation { get; set; }

    [ForeignKey("IdVmfk")]
    [InverseProperty("RpaVms")]
    public virtual Vm? IdVmfkNavigation { get; set; }

    [InverseProperty("IdRpaVmFkNavigation")]
    public virtual ICollection<RpaVmsLegado> RpaVmsLegados { get; set; } = new List<RpaVmsLegado>();
}
