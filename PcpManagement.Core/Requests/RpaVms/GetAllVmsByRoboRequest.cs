namespace PcpManagement.Core.Requests.RpaVms;

public class GetAllVmsByRoboRequest : PagedRequest
{
    public int idRoboFK { get; set; }
}