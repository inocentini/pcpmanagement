using PcpManagement.Core.Enums;

namespace PcpManagement.Core;

public static class Configuration
{
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;
    public const EStatusCode DefaultStatusCode = EStatusCode.OK;

    public static string BackendUrl { get; set; } = "http://localhost:5296";
    public static string FrontendUrl { get; set; } = "http://localhost:5292";
}