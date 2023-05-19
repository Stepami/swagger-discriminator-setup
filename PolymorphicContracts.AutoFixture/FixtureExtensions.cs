using AutoFixture;

namespace PolymorphicContracts.AutoFixture;

public static class FixtureExtensions
{
    public static PolymorphicCustomizationBuilder<TBaseType> CustomizePolymorphism<TBaseType>(this Fixture fixture)
        where TBaseType : class => new(fixture);
}