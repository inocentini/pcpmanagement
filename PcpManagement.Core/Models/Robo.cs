using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Models;

public class Robo
{
    [Key]
    public long Id { get; set; }
    public string IdProjeto { get; set; } = string.Empty;
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