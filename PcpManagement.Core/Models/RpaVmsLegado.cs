using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcpManagement.Core.Models;

[Table("rpaVmsLegados", Schema = "efetividadeRobotica")]
public partial class RpaVmsLegado
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idLegadoFK")]
    public int? IdLegadoFk { get; set; }

    [Column("idRpaVmFK")]
    public int? IdRpaVmFk { get; set; }

    [Column("userLegado")]
    [StringLength(16)]
    public string? UserLegado { get; set; }

    [Column("userBanco")]
    [StringLength(64)]
    
    public string? UserBanco { get; set; }

    [ForeignKey("IdLegadoFk")]
    [InverseProperty("RpaVmsLegados")]
    public virtual Legados? IdLegadoFkNavigation { get; set; }

    [ForeignKey("IdRpaVmFk")]
    [InverseProperty("RpaVmsLegados")]
    public virtual RpaVm? IdRpaVmFkNavigation { get; set; }
}
