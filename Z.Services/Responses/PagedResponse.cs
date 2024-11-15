using System.Net;
using Z.Data.Entities;
using Z.Data.Interfaces;

namespace Z.Services.Responses;

public class PagedResponse<T>(Pagination<T>? data = null) : BaseResponse<IEnumerable<T>>(null), IResponse<IEnumerable<T>>, IPagination
{
    private Pagination<T>? _items = data;

    public new IEnumerable<T>? Data => _items;

    public int PageIndex => _items?.PageIndex ?? 0;

    public int PageSize => _items?.PageSize ?? 1;

    public int PageCount => _items?.PageCount ?? 0;

    public int TotalCount => _items?.TotalCount ?? 0;

    IResponse IResponse<IEnumerable<T>>.Ok(IEnumerable<T>? data, string message)
    {
        if (data is not Pagination<T> pagin)
            pagin = new Pagination<T>(0, 1, data?.Count() ?? 0, data);

        _items = pagin;
        Code = HttpStatusCode.OK;
        Message = message;
        return this;
    }
}