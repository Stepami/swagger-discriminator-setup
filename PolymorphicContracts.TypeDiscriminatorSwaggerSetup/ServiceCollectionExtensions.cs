using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies;
using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Options;

namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup;

public static class ServiceCollectionExtensions
{
    public static void AddTypeDiscriminatorToSwagger(this IServiceCollection services,
        IConfiguration configuration) => services
        .Configure<JsonHierarchiesOptions>(configuration.GetSection(JsonHierarchiesOptions.Key))
        .AddSingleton<IJsonHierarchyRootsProvider, JsonHierarchyRootsProvider>()
        .AddSingleton<IJsonHierarchiesProvider, JsonHierarchiesProvider>()
        .AddSingleton<IJsonHierarchiesRepository, JsonHierarchiesRepository>()
        .ConfigureSwaggerGen(options =>
        {
            options.UseOneOfForPolymorphism();
            options.SchemaFilter<TypeDiscriminatorSchemaFilter>();
        });
}