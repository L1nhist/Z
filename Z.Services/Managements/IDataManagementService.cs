using Z.Data.Entities;
using Z.Services.Requests;
using Z.Services.Results;

namespace Z.Services.Managements;

public interface IDataManagementService<T>
    where T : class, IEntity
{
    Task<IResult> Create<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResult> Update<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResult> Delete<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResult<TRes>> Select<TReq, TRes>(TReq request)
        where TReq : IRequest;

    Task<IResult<IEnumerable<TRes>>> Search<TReq, TRes>(TReq request)
        where TReq : ListingRequest;

    Task<PagedResult<TRes>> Paginate<TReq, TRes>(TReq request)
        where TReq : PagingRequest;

    Task<IResult> Validate(object? request);

    Task<IResult<TRes>> Validate<TRes>(object? request);

    void WriteLog(string msg = "");

    void WriteLog(Exception ex);
}