﻿namespace PcpManagement.Api.Common.Api;

public static class ApiConfiguration
{
    public static string ConnectionString { get; set; } = string.Empty;
    public const string CorsPolicyName = "wasm";
}