using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcpManagement.Core.Models;

[Table("legados", Schema = "efetividadeRobotica")]
public class Legados
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("legado")]
    [StringLength(32)]
    public string? Nome { get; set; }

    [Column("autenticacao")]
    [StringLength(32)]
    public string? Autenticacao { get; set; }

    [Column("acesso")]
    [StringLength(32)]
    public string? Acesso { get; set; }

    [Column("url")]
    public string? Url { get; set; }

    [InverseProperty("IdLegadoFkNavigation")]
    public virtual ICollection<RpaVmsLegado> RpaVmsLegados { get; set; } = new List<RpaVmsLegado>();
}
