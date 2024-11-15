using System.Net;

namespace Z.Services.Responses;

public interface IResponse
{
    bool Success { get; }

    HttpStatusCode Code { get; }

    string Message { get; }

    IResponse Ok(string message = "");

    IResponse Error(string message = "", HttpStatusCode code = HttpStatusCode.InternalServerError);
}

public interface IResponse<T> : IResponse
{
    T? Data { get; }

    IResponse Ok(T? data, string message = "");
}