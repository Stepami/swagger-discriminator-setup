using PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models.Discriminators;

namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models;

public record DerivedTypeInfo(
    Type Type,
    IDiscriminator Discriminator
);

public record JsonHierarchy(
    Type BaseType,
    Dictionary<Type, DerivedTypeInfo> DerivedTypes
);