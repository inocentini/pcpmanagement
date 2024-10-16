namespace PcpManagement.Core.Requests.RpaVmsLegados;

public class GetAllRpaVmsLegadoByRpaVmRequest : PagedRequest
{
    public int IdRpaVmFk { get; set; }
}