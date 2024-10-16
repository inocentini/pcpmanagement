using System.ComponentModel.DataAnnotations;

namespace PcpManagement.Core.Requests.Legados;

public class UpdateLegadoRequest : Request
{
    
    [StringLength(32, ErrorMessage = "O nome do Legado deve ter até 32 caracteres.")]
    public string? Nome { get; set; }
    
    [StringLength(32, ErrorMessage = "A autenticação do Legado deve ter até 32 caracteres.")]
    public string? Autenticacao { get; set; }
    
    [StringLength(32, ErrorMessage = "O acesso do Legado deve ter até 32 caracteres.")]
    public string? Acesso { get; set; }
    
    public string? Url { get; set; }
}