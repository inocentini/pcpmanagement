using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.VirtualMachines;

public class CreateVirtualMachineRequest : Request
{
    
    [Required(ErrorMessage = "Informe o hostname")]
    [StringLength(80, ErrorMessage = "O hostname deve ter até 80 caracteres")]
    [MinLength(10, ErrorMessage = "O hostname deve conter pelo menos 10 caracteres. Ex.: BRTLVBGS1234RP")]
    public string Hostname { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe o IP")]
    [StringLength(20, ErrorMessage = "O ip não pode ter mais que 20 caracteres")]
    [MinLength(12,
        ErrorMessage = "O ip deve conter pelo menos 12 caracteres. Ex.: 255.255.255.255 ou 255.255.255.255/30")]
    public string Ip { get; set; } = string.Empty;
    
    [StringLength(20, ErrorMessage = "O username deve ter até 20 caracteres")]
    [MinLength(8, ErrorMessage = "O username deve conter pelo menos 8 caracteres. Ex.: RPRDXPTO")]
    public string? UserName { get; set; } = string.Empty;
    
    [RegularExpression(@"^\d{1,}$",ErrorMessage = "Padrão inválido, favor fornecer um ou mais dígitos.")]
    public int? VCpu { get; set; }
    
    [RegularExpression(@"^\d{1,}$",ErrorMessage = "Padrão inválido, favor fornecer um ou mais dígitos.")]
    public int? Memoria { get; set; }
    
    [RegularExpression(@"^\d{1,}$",ErrorMessage = "Padrão inválido, favor fornecer um ou mais dígitos.")]
    public int? HD { get; set; }

    [StringLength(20, ErrorMessage = "O Ambiente deve ter até 20 caracteres.")]
    [MinLength(3, ErrorMessage = "O Ambiente deve conter pelo menos 3 caracteres. Ex.: Produção/Staging/Dev")]
    public string? Ambiente { get; set; } = string.Empty;
    
    public bool? Emprestimo { get; set; } 

    [StringLength(20, ErrorMessage = "A resolução deve ter até 20 caracteres.")]
    [MinLength(5, ErrorMessage = "A resolução deve conter pelo menos 5 caracteres. Ex.: 1920x768")]
    public string? Resolucao { get; set; } = string.Empty;

    [Required]
    [StringLength(20, ErrorMessage = "O environment deve ter até 20 caracteres.")]
    [MinLength(3, ErrorMessage = "O environment deve conter pelo menos 3 caracteres. Ex.: prod/stage/dev")]
    public string Environment { get; set; } = string.Empty;

    [StringLength(20, ErrorMessage = "O Sistema Operacional deve ter até 20 caracteres.")]
    [MinLength(3,
        ErrorMessage = "O Sistema Operacional deve conter pelo menos 3 caracteres. Ex.: W10/Ubuntu/Windows Server")]
    public string? SistemaOperacional { get; set; } = string.Empty;
    
    
    public bool? Status { get; set; }
    
    [StringLength(255, ErrorMessage = "A observação deve ter até 255 caracteres.")]
    [MinLength(3, ErrorMessage = "A observação deve conter pelo menos 3 caracteres. Ex.: N/A ou Sem observação")]
    public string? Observacao { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "A funcionalidade deve ter até 30 caracteres.")]
    [MinLength(3,
        ErrorMessage =
            "A funcionalidade deve conter pelo menos 3 caracteres. Ex.: produtora/executora/orquestradora/IIS")]
    public string? Funcionalidade { get; set; } = string.Empty;

    public string? Lote { get; set; } = string.Empty;

    public string? DataCenter { get; set; } = string.Empty;

    public string? Farm { get; set; } = string.Empty;
}