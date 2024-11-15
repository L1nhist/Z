using Z.Data.Entities;
using Z.Services.Requests;
using Z.Services.Responses;

namespace Z.Services.Managements;

public interface IDataManagementService<T>
    where T : class, IEntity
{
    Task<IResponse> Create<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResponse> Update<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResponse> Delete<TReq>(TReq request)
        where TReq : IRequest;

    Task<IResponse<TRes>> Select<TReq, TRes>(TReq request)
        where TReq : IRequest;

    Task<ListedResponse<TRes>> Search<TReq, TRes>(TReq request)
        where TReq : ListingRequest;

    Task<PagedResponse<TRes>> Paginate<TReq, TRes>(TReq request)
        where TReq : PagingRequest;

    Task<bool> Validate<TReq>(TReq? request)
        where TReq : IRequest;

    void WriteLog(string msg = "");

    void WriteLog(Exception ex);
}