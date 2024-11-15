using Z.Data.Interfaces;

namespace Z.Services.Responses;

public class ListedResponse<T>(IEnumerable<T>? data = null) : BaseResponse<IEnumerable<T>>(data)
{ }