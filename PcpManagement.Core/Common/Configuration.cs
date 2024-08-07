using PcpManagement.Core.Common.Enum;

namespace PcpManagement.Core.Common;

public class Configuration
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;
    public const EStatusCode DefaultStatusCode = EStatusCode.OK;

    public static string ConnectionString { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;

}