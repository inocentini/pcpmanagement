using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.Robos;

public class UpdateRoboRequest : Request
{
    public long IdRobo { get; set; }
    
    [Required(ErrorMessage = "Informe o Id do Projeto")]
    [StringLength(32, ErrorMessage = "O Id do Projeto deve ter até 32 caracteres")]
    public string IdProjeto { get; set; } = null!;
    
    [Required(ErrorMessage = "Informe o PTI do Projeto")]
    [StringLength(32, ErrorMessage = "O PTI do Projeto deve ter até 32 caracteres")]
    public string Pti { get; set; } = null!;

    [StringLength(128, ErrorMessage = "O nome do Projeto deve ter até 128 caracteres.")]
    public string? Projeto { get; set; }

    public string? Descricao { get; set; }

    [StringLength(128,ErrorMessage = "O Scrum do Projeto deve ter até 8 caracteres")]
    public string? Scrum { get; set; }

    [StringLength(128,ErrorMessage = "A Gerencia do Projeto deve ter até 8 caracteres")]
    public string? Gerencia { get; set; }

    [StringLength(64,ErrorMessage = "A Equipe do Projeto deve ter até 4 caracteres")]
    public string? Equipe { get; set; }

    [StringLength(64,ErrorMessage = "A Aplicacao do Projeto deve ter até 4 caracteres")]
    public string? Aplicacao { get; set; }

    [StringLength(32,ErrorMessage = "A Torre do Projeto deve ter até 2 caracteres")]
    public string? Torre { get; set; }

    [StringLength(64,ErrorMessage = "O SuporteFabrica do Projeto deve ter até 4 caracteres")]
    public string? SuporteFabrica { get; set; }

    public DateTime? KtDate { get; set; }

    [StringLength(32,ErrorMessage = "O ProjetoEm do Projeto deve ter até 2 caracteres")]
    public string? ProjetoEm { get; set; }

    [StringLength(64,ErrorMessage = "O ClusterAplicacao do Projeto deve ter até 4 caracteres")]
    public string? ClusterAplicacao { get; set; }

    [StringLength(64,ErrorMessage = "O BancodeDados do Projeto deve ter até 4 caracteres")]
    public string? BancodeDados { get; set; }

    [StringLength(32,ErrorMessage = "O NumCrq do Projeto deve ter até 2 caracteres")]
    public string? NumCrq { get; set; }

    [StringLength(32,ErrorMessage = "O StatusCrq do Projeto deve ter até 2 caracteres")]
    public string? StatusCrq { get; set; }

    public DateTime? BlindagemDate { get; set; }

    public int? QtdLicenca { get; set; }
}