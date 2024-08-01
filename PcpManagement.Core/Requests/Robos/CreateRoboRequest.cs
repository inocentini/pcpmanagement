using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.Robos;

public class CreateRoboRequest : Request
{
    [Required(ErrorMessage = "Informe o Id do Projeto")]
    [StringLength(32, ErrorMessage = "O Id do Projeto deve ter até 32 caracteres")]
    [MinLength(5, ErrorMessage = "O Id do Projeto deve conter pelo menos 5 caracteres. Ex.: 12345")]
    public string IdProjeto { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe o PTI do Projeto")]
    [StringLength(32, ErrorMessage = "O PTI do Projeto deve ter até 32 caracteres")]
    [MinLength(5, ErrorMessage = "O PTI deve conter pelo menos 5 caracteres. Ex.: PTI-123")]
    [DisplayName]
    public string PTI { get; set; } = string.Empty;
    
    public string Projeto { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Scrum { get; set; } = string.Empty;
    public string Gerencia { get; set; } = string.Empty;
    public string Equipe { get; set; } = string.Empty;
    public string Aplicacao { get; set; } = string.Empty;
    public string Torre { get; set; } = string.Empty;
    public string SuporteFabrica { get; set; } = string.Empty;
    public DateTime KTDate { get; set; }
    public string ProjetoEm { get; set; } = string.Empty;
    public string ClusterAplicacao { get; set; } = string.Empty;
    public string BancoDeDados { get; set; } = string.Empty;
    public string NumCRQ { get; set; } = string.Empty;
    public string StatusCRQ { get; set; } = string.Empty;
    public DateTime BlindagemDate { get; set; }
    public int QtdLicenca { get; set; }
}