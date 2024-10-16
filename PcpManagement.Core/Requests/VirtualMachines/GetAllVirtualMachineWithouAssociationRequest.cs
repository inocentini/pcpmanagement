using System.Text.Json.Serialization;

namespace PcpManagement.Core.Requests.VirtualMachines;

public class GetAllVirtualMachineWithouAssociationRequest : PagedRequest
{
    [JsonIgnore]
    public override long? Id { get; set; }
}