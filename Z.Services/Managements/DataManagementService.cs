﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using Z.Core.Utilities;
using Z.Data.Entities;
using Z.Data.Extentions;
using Z.Data.Repositories;
using Z.Services.Requests;
using Z.Services.Results;

namespace Z.Services.Managements;

public class DataManagementService<T>(ILoggerFactory logFactory, IMapper mapper, IRepository<T> repository)
    : IDataManagementService<T>
    where T : class, IEntity
{
    protected ILogger<DataManagementService<T>> Logger { get; } = logFactory.CreateLogger<DataManagementService<T>>();

    protected IMapper Mapper { get; } = mapper;

    public IRepository<T> Repository { get; } = repository;

    protected virtual bool BuildQuery<TReq>([NotNullWhen(true)] TReq request)
    {
        if (request == null) return false;
        if (request is ListingRequest listing && !Util.IsEmpty(listing.Orders))
            Repository.SortBy(listing.Orders.ToArray());

        return true;
    }

    public async Task<IResult> Create<TReq>(TReq request)
        where TReq : IRequest
    {
        var resp = Result.New();
        try
        {
            var valid = await Validate(request);
            if (!valid.Success) return valid;

            var ent = Mapper.Map<T>(request);
            if (ent == null) return resp.BadRequest("Requested data can not be mapped as usual");
            if (await Repository.Insert(ent) > 0) return Result.Ok();
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }

        return resp.BadRequest("Request can not be execute");
    }

    public async Task<IResult> Update<TReq>(TReq request)
        where TReq : IRequest
    {
        var resp = new BaseResult();
        try
        {
            var valid = await Validate(request);
            if (!valid.Success) return valid;

            var ent = Mapper.Map<T>(request);
            if (ent == null) return resp.BadRequest("Requested data can not be mapped as usual");

            var upd = await Repository.GetByKeys(ent.Keys);
            if (upd == null) return resp.NotFound("Can not find specific data");

            Mapper.Map(ent, upd);
            if (await Repository.Update(upd) > 0) return resp.Ok();
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }

        return resp.BadRequest("Request can not be execute");
    }

    public async Task<IResult> Delete<TReq>(TReq request)
        where TReq : IRequest
    {
        var resp = new BaseResult();
        try
        {
            var valid = await Validate(request);
            if (!valid.Success) return valid;

            var ent = Mapper.Map<T>(request);
            if (ent == null) return resp.BadRequest("Requested data can not be mapped as usual");

            var del = await Repository.GetByKeys(ent.Keys);
            if (del == null) return resp.NotFound("Can not find specific data");
            if (await Repository.Delete(del) > 0) return resp.Ok();
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex);
        }

        return resp.BadRequest("Request can not be execute");
    }

    public async Task<IResult<TRes>> Select<TReq, TRes>(TReq request)
        where TReq : IRequest
    {
        var resp = Result.New<TRes>();
        try
        {
            var ent = Mapper.Map<T>(request);
            if (ent == null) return resp.BadRequest("Requested data can not be mapped as usual");

            ent = await Repository.GetByKeys(ent.Keys);
            if (ent == null) return resp.NotFound("Can not find specific data");

            var data = Mapper.Map<TRes>(ent);
            if (data != null) return resp.Ok(data);
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }
        
        return resp.BadRequest("Result data can not be mapped as usual");
    }

    public async Task<IResult<IEnumerable<TRes>>> Search<TReq, TRes>(TReq request)
        where TReq : ListingRequest
    {
        var resp = Result.New<IEnumerable<TRes>>();
        try
        {
            if (!BuildQuery(request)) return resp.BadRequest("Requested data can not be mapped as usual");

            var ents = await Repository.GetList();
            if (Util.IsEmpty(ents)) return resp.NotFound("Can not find specific data");

            var data = Mapper.Map<IEnumerable<TRes>>(ents);
            if (data != null) return resp.Ok(data);
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }
        
        return resp.BadRequest("Result data can not be mapped as usual");
    }

    public async Task<PagedResult<TRes>> Paginate<TReq, TRes>(TReq request)
        where TReq : PagingRequest
    {
        var resp = Result.NewPage<TRes>();
        try
        {
            if (!BuildQuery(request)) return resp.BadRequest("Requested data can not be mapped as usual");

            var ents = await Repository.GetPaging(request.PageIndex ?? 0, request.PageSize ?? 0);
            if (Util.IsEmpty(ents)) return resp.NotFound("Can not find specific data");

            var data = Mapper.Map<Pagination<TRes>>(ents);
            if (data != null) return resp.Ok(data);
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }
        
        return resp.BadRequest("Result data can not be mapped as usual");
    }

    public virtual async Task<IResult> Validate(object? request)
        => Result.BadRequest("Validation failed!");

    public virtual async Task<IResult<TRes>> Validate<TRes>(object? request)
        => Result.BadRequest<TRes>("Validation failed!");

    public void WriteLog(string? msg = "")
    {
        if (!Util.IsEmpty(msg, true))
            Logger.LogWarning(msg);
    }

    public void WriteLog(Exception ex)
    {
        if (ex != null)
            Logger.LogError(ex, "");
    }
}

public class DataManagementService<TEnt, TId>(ILoggerFactory logFactory, IMapper mapper, IRepository<TEnt> repository)
    : DataManagementService<TEnt>(logFactory, mapper, repository)
    where TEnt : class, IEntity<TId>
{
    public async Task<IResult> Update<TReq>(TId id, TReq request)
        where TReq : IRequest
    {
        var resp = new BaseResult();
        try
        {
            var valid = await Validate(request);
            if (!valid.Success) return valid;

            var ent = await Repository.GetById(id);
            if (ent == null) return resp.NotFound("Can not find specific data");

            ent = Mapper.Map(request, ent);
            if (await Repository.Update(ent) > 0) return resp.Ok();
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }

        return resp.BadRequest("Request can not be execute");
    }

    public async Task<IResult> Delete<TReq>(TId id)
        where TReq : IRequest
    {
        var resp = new BaseResult();
        try
        {
            if (await Repository.DeleteByIds(id) > 0) return resp.Ok();
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }

        return resp.BadRequest("Request can not be execute");
    }

    public async Task<IResult<TRes>> Select<TReq, TRes>(TId id)
        where TReq : IRequest
    {
        var resp = Result.New<TRes>();
        try
        {
            var ent = await Repository.GetById(id);
            if (ent == null) return resp.NotFound("Can not find specific data");

            var data = Mapper.Map<TRes>(ent);
            if (data != null) return resp.Ok(data);
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }
        
        return resp.BadRequest("Result data can not be mapped as usual");
    }

    public async Task<IResult<IEnumerable<TRes>>> Select<TReq, TRes>(params TId[] ids)
        where TReq : IRequest
    {
        var resp = Result.NewList<TRes>();
        try
        {
            var ents = await Repository.GetListByIds(ids);
            if (Util.IsEmpty(ents)) return resp.NotFound("Can not find specific data");

            var data = Mapper.Map<IEnumerable<TRes>>(ents);
            if (data != null) return resp.Ok(data);
        }
        catch (Exception ex)
        {
            WriteLog(ex);
            return resp.Error(ex.Message);
        }

        return resp.BadRequest("Result data can not be mapped as usual");
    }
}
