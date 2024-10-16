namespace PcpManagement.Core.Requests.RpaVms;

public class GetAllRpaVmsByVmIdFkRequest : PagedRequest
{
    public int IdVmFk { get; set; }
}