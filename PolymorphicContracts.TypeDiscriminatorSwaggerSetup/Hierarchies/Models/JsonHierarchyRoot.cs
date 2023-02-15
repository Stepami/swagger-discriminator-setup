namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models;

public record JsonHierarchyRoot(
    Type Type,
    IEnumerable<JsonDerivedTypeAttribute> Attributes
);