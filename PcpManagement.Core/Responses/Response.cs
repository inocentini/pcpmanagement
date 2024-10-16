using System.Text.Json.Serialization;
using PcpManagement.Core.Common;
using PcpManagement.Core.Common.Enum;

namespace PcpManagement.Core.Responses;

public class Response<T>
{
    [JsonConstructor]
    public Response() => Code = Configuration.DefaultStatusCode;
    
    
    public Response(T? data, EStatusCode code = Configuration.DefaultStatusCode, string? message = null)
    {
        Code = code;
        Data = data;
        Message = message;
    }

    public EStatusCode Code { get; set; }
    public T? Data { get; set; } 
    public string? Message { get; set; }

    [JsonIgnore] public bool IsSuccess => Code is >= EStatusCode.OK and <= EStatusCode.MiscellaneousWarning;
}