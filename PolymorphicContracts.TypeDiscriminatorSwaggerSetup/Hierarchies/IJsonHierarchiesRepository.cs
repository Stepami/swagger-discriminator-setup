using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models;

namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies;

public interface IJsonHierarchiesRepository
{
    bool TryGetHierarchy(Type baseType, out JsonHierarchy jsonHierarchy);
}

internal class JsonHierarchiesRepository : IJsonHierarchiesRepository
{
    private readonly Dictionary<Type, JsonHierarchy> _hierarchies;

    public JsonHierarchiesRepository(IJsonHierarchiesProvider provider) =>
        _hierarchies = provider.GetAllHierarchies()
            .ToDictionary(h => h.BaseType, h => h);

    public bool TryGetHierarchy(Type baseType, out JsonHierarchy jsonHierarchy) =>
        _hierarchies.TryGetValue(baseType, out jsonHierarchy!);
}