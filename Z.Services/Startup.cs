using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Z.Data.Repositories;
using Z.Services.Managements;

namespace Z.Services;

public static class Startup
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));
        services.AddScoped(typeof(IDataManagementService<>), typeof(DataManagementService<>));

        //Register AutoMapper by MapperProfiles
        var type = typeof(Profile);
        var profiles = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a != Assembly.GetAssembly(type))
                        .SelectMany(a => a.GetExportedTypes().Where(t => t.IsSubclassOf(type)));

        services.AddAutoMapper(opts =>
        {
            foreach (var p in profiles)
            {
                opts.AddProfile(p);
            }
        });

    }
}