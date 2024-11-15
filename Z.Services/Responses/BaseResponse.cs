using System.Net;

namespace Z.Services.Responses;

public class BaseResponse : IResponse
{
    public bool Success => Code == HttpStatusCode.OK;

    public HttpStatusCode Code { get; protected internal set; } = HttpStatusCode.OK;

    public string Message { get; protected internal set; } = "";

    IResponse IResponse.Ok(string message)
    {
        Code = HttpStatusCode.OK;
        Message = message;
        return this;
    }

    IResponse IResponse.Error(string message, HttpStatusCode code)
    {
        Code = code;
        Message = message;
        return this;
    }
}

public class BaseResponse<T>(T? data = default) : BaseResponse, IResponse<T>
{
    public T? Data { get; private set; } = data;

    IResponse IResponse<T>.Ok(T? data, string message)
    {
        Data = data;
        Code = HttpStatusCode.OK;
        Message = message;
        return this;
    }

    IResponse IResponse.Error(string message, HttpStatusCode code)
    {
        Data = default;
        Code = code;
        Message = message;
        return this;
    }
}