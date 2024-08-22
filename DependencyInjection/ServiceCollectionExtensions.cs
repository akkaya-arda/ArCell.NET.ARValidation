using System.Reflection;
using ArCell.NET.ARValidation.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ArCell.NET.ARValidation.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddValidators(this IServiceCollection services, bool searchAllAssemblies = false)
    {
        if (searchAllAssemblies)
        {
            var assemblyNames = Assembly.GetCallingAssembly().GetReferencedAssemblies();
            foreach (var assembly in assemblyNames.Select(Assembly.Load))
            {
                foreach (var type in assembly.GetTypes().Where(x =>
                             x.IsClass && !x.IsAbstract && typeof(IEntityValidator).IsAssignableFrom(x)))
                {
                    var interfaceType = type.BaseType?.GetInterface("IEntityValidator`1");
                    if (interfaceType != null)
                        services.AddScoped(interfaceType, type);
                }
            }
        }

        foreach (var type in Assembly.GetCallingAssembly().GetTypes().Where(x=>x is { IsClass: true, IsAbstract: false } && typeof(IEntityValidator).IsAssignableFrom(x)))
        {
            var interfaceType = type.BaseType?.GetInterface("IEntityValidator`1");
            if(interfaceType != null)
                services.AddScoped(interfaceType, type);
        }
    }
}