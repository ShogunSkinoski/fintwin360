using System.Reflection;
using Application.Accounts.Commands;
using Application.Accounts.Commands.CreateAccount;
using Application.Common;
using Application.Members.Commands;
using Application.Members.Commands.CreateMember;
using Application.Members.Commands.ValidateMember;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions;

public static class ApplicationLayerConfig
{
    public static void ApplicationLayerDependencies(this IServiceCollection services)
    {
        typeof(CreateMemberCommand).Assembly.GetTypes()
                .Where(type => type.IsClass &&
                                !type.IsAbstract &&
                                type.GetInterfaces()
                                    .Any(i => i.IsGenericType &&
                                                i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                .SelectMany(type => type.GetInterfaces()
                                            .Where(i => i.IsGenericType &&
                                                        i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)),
                            (handlerType, @interface) => new { HandlerType = handlerType, InterfaceType = @interface })
                .ToList()
                .ForEach(handler =>
                    services.AddScoped(handler.InterfaceType, handler.HandlerType)
                );

        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MemberMappingProfile());
            mc.AddProfile(new AccountMappingProfile());
        });


        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddScoped<ICommandPublisher, CommandPublisher>();

        services.AddTransient<IValidator<CreateAccountCommand>, CreateAccountCommandValidator>();
        services.AddTransient<IValidator<CreateMemberCommand>, CreateMemberCommandValidator>();
        services.AddTransient<IValidator<ValidateMemberCommand>, ValidateMemberCommandValidator>();
    }
    public static void AddAutoMapperProfilesFromApplicationLayer(this IServiceCollection services, Assembly applicationAssembly)
    {
        // Get all types from the application assembly that inherit from Profile
        var profileTypes = applicationAssembly.GetExportedTypes()
            .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToArray();

        // Create and register AutoMapper configuration
        var config = new MapperConfiguration(cfg =>
        {
            foreach (var profile in profileTypes)
            {
                cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        });

        // Register IMapper as a singleton
        services.AddSingleton<IMapper>(config.CreateMapper());

        // Optionally, log the discovered profiles
        foreach (var profile in profileTypes)
        {
            Console.WriteLine($"Discovered AutoMapper profile in application layer: {profile.FullName}");
        }
    }

   
}