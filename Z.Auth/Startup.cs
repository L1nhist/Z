using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Z.Auth.Contexts;
using Z.Auth.Interceptors;

namespace Z.Auth;

public static class Startup
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<IAuthUserContext, AuthUserContext>();
        services.AddScoped<ISaveChangesInterceptor, AuditingInterceptor>();
    }
}