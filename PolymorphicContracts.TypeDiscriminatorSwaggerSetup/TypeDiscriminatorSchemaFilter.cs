using Microsoft.OpenApi.Models;
using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies;
using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup;

// ReSharper disable once ClassNeverInstantiated.Global
public class TypeDiscriminatorSchemaFilter : ISchemaFilter
{
    private readonly IJsonHierarchiesRepository _jsonHierarchiesRepository;

    public TypeDiscriminatorSchemaFilter(IJsonHierarchiesRepository jsonHierarchiesRepository) =>
        _jsonHierarchiesRepository = jsonHierarchiesRepository;

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        const string discriminator = "$type";
        
        var type = context.Type;

        if (!_jsonHierarchiesRepository.TryGetHierarchy(type.IsAbstract ? type : type.BaseType!, out var hierarchy))
            return;

        DerivedTypeInfo info;
        try
        {
            info = type.IsAbstract
                ? hierarchy.DerivedTypes.First().Value
                : hierarchy.DerivedTypes[type];
        }
        catch (KeyNotFoundException)
        {
            return;
        }

        schema.Required.Add(discriminator);
        schema.Properties.Add(discriminator, new OpenApiSchema
        {
            Type = info.Discriminator.Type
        });
        if (type.IsAbstract)
        {
            schema.Properties[discriminator].Enum = hierarchy.DerivedTypes.Values
                .Select(x => x.Discriminator.Value).ToList();
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = discriminator,
                Mapping = hierarchy.DerivedTypes.Values
                    .ToDictionary(
                        x => x.Discriminator.ToString()!,
                        x => $"#components/schemas/{x.Type.Name}"
                    )
            };
        }
        else
        {
            schema.Properties[discriminator].Default = info.Discriminator.Value;
        }
    }
}