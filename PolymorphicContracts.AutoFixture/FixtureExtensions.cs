using AutoFixture;

namespace PolymorphicContracts.AutoFixture;

public static class FixtureExtensions
{
    public static PolymorphicCustomizationBuilder<TBaseType> CustomizePolymorphismFor<TBaseType>(this Fixture fixture)
        where TBaseType : class => new(fixture);
}