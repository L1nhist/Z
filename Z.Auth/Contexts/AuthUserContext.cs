using Microsoft.AspNetCore.Http;
using Z.Auth.Models;

namespace Z.Auth.Contexts;

public sealed class AuthUserContext(IHttpContextAccessor accessor) : IAuthUserContext
{
    private readonly HttpContext _context = accessor.HttpContext ?? throw new NullReferenceException("HttpContextAccessor must not be null");

    public bool IsAuthenticated => _context?.User?.Identity?.IsAuthenticated == true;

    public IAuthUser? User =>  IsAuthenticated ? new AuthUser(_context.User) : null;
}