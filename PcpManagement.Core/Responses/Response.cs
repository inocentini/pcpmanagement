using System.Text.Json.Serialization;
using PcpManagement.Core.Enums;

namespace PcpManagement.Core.Responses;

public class Response<T>
{
    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;

    public Response(T? data, EStatusCode code = Configuration.DefaultStatusCode, string? message = null)
    {
        _code = code;
        Data = data;
        Message = message;
    }
    private EStatusCode _code = Configuration.DefaultStatusCode;
    public T? Data { get; set; }
    public string? Message { get; set; }
    
    [JsonIgnore]
    public bool IsSuccess => _code is >= EStatusCode.OK and <= EStatusCode.MiscellaneousWarning;
}