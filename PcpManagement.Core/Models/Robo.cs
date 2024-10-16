using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcpManagement.Core.Models;

[Table("robos", Schema = "efetividadeRobotica")]
public class Robo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("idProjeto")]
    [StringLength(32)]
    public string IdProjeto { get; set; } = null!;

    [Column("pti")]
    [StringLength(32)]
    public string Pti { get; set; } = null!;

    [Column("projeto")]
    [StringLength(128)]
    public string? Projeto { get; set; }

    [Column("descricao")]
    public string? Descricao { get; set; }

    [Column("scrum")]
    [StringLength(128)]
    public string? Scrum { get; set; }

    [Column("gerencia")]
    [StringLength(128)]
    public string? Gerencia { get; set; }

    [Column("equipe")]
    [StringLength(64)]
    public string? Equipe { get; set; }

    [Column("aplicacao")]
    [StringLength(64)]
    public string? Aplicacao { get; set; }

    [Column("torre")]
    [StringLength(32)]
    public string? Torre { get; set; }

    [Column("suporteFabrica")]
    [StringLength(64)]
    public string? SuporteFabrica { get; set; }

    [Column("ktDate")]
    public DateTime? KtDate { get; set; }

    [Column("projetoEm")]
    [StringLength(32)]
    public string? ProjetoEm { get; set; }

    [Column("clusterAplicacao")]
    [StringLength(64)]
    public string? ClusterAplicacao { get; set; }

    [Column("bancodeDados")]
    [StringLength(64)]
    public string? BancodeDados { get; set; }

    [Column("numCRQ")]
    [StringLength(32)]
    public string? NumCrq { get; set; }

    [Column("statusCRQ")]
    [StringLength(32)]
    public string? StatusCrq { get; set; }

    [Column("blindagemDate")]
    public DateTime? BlindagemDate { get; set; }

    [Column("qtdLicenca")]
    public int? QtdLicenca { get; set; }

    [InverseProperty("IdProjetoFkNavigation")] 
    public virtual ICollection<RpaVm>? RpaVms { get; set; }
}
