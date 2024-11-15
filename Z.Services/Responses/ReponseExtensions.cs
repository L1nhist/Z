using System.Net;
using Z.Data.Entities;

namespace Z.Services.Responses;

public static class ReponseExtensions
{
    public static T Ok<T>(this T response, string message = "")
        where T : IResponse
        => (T)response.Ok(message);

    public static TResp Ok<TResp, TVal>(this TResp response, TVal? data, string message = "")
        where TResp : IResponse<TVal>
        => (TResp)response.Ok(data, message);

    public static TResp Ok<TResp, TVal>(this TResp response, Pagination<TVal>? data, string message = "")
        where TResp : PagedResponse<TVal>
        => response.Ok(data, message);

    public static T Error<T>(this T response, string message = "", HttpStatusCode code = HttpStatusCode.InternalServerError)
        where T : IResponse
        => (T)response.Error(message, code);

    public static T BadRequest<T>(this T response, string message = "")
        where T : IResponse
        => Error(response, message, HttpStatusCode.BadRequest);

    public static T Conflict<T>(this T response, string message = "")
        where T : IResponse
        => Error(response, message, HttpStatusCode.Conflict);

    public static T Forbidden<T>(this T response, string message = "")
        where T : IResponse
        => Error(response, message, HttpStatusCode.Forbidden);

    public static T NotFound<T>(this T response, string message = "")
        where T : IResponse
        => Error(response, message, HttpStatusCode.NotFound);

    public static T Unauthorized<T>(this T response, string message = "")
        where T : IResponse
        => Error(response, message, HttpStatusCode.Unauthorized);
}
