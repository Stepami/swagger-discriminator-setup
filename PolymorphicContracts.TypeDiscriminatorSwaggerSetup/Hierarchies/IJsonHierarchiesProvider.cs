using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models;
using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models.Discriminators;

namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies;

public interface IJsonHierarchiesProvider
{
    IEnumerable<JsonHierarchy> GetAllHierarchies();
}

internal class JsonHierarchiesProvider : IJsonHierarchiesProvider
{
    private readonly IJsonHierarchyRootsProvider _rootsProvider;

    public JsonHierarchiesProvider(IJsonHierarchyRootsProvider rootsProvider) =>
        _rootsProvider = rootsProvider;
    
    public IEnumerable<JsonHierarchy> GetAllHierarchies()
    {
        var typesWithAttributes = _rootsProvider.GetAllRoots();

        var hierarchies = typesWithAttributes
            .Select(x => new JsonHierarchy(
                x.Type,
                x.Attributes.Select(attr => new DerivedTypeInfo(
                        attr.DerivedType, attr.TypeDiscriminator.ToDiscriminator(attr.DerivedType)))
                    .ToDictionary(d => d.Type, d => d)
            )).ToList();

        var wrongHierarchies = hierarchies
            .Where(x => x.DerivedTypes.Values
                    .Select(d => d.Discriminator.Type)
                    .Aggregate<string?>((t1, t2) => t1 == t2 ? t1 : null)
                is null)
            .Select(x => x.BaseType).ToList();
        
        if (wrongHierarchies.Any())
            throw new InvalidDataException(
                $"All discriminators must have the same type within hierarchy.\nHierarchies:\n{string.Join('\n', wrongHierarchies)}"
            );
        
        return hierarchies;
    }
}