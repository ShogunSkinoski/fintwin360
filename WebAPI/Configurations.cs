using Domain;
using Infrastructure;
using System.Reflection;
using WebAPI.V1.Members.Endpoints;

namespace WebAPI;

internal static class Configurations
{
    public static IServiceCollection AppConfiguration(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapperProfilesFromApplicationLayer(typeof(Program).Assembly);
        services.AddMemberDependencies();

        services.DomainLayerDependencies();
        services.ApplicationLayerDependencies();
        services.InfraLayerDependencies();
        return services;
    }

    
}
