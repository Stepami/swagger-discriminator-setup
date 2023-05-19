using AutoFixture;
using AutoFixture.Kernel;

namespace PolymorphicContracts.AutoFixture;

public class PolymorphicCustomizationBuilder<TBaseType>
    where TBaseType : class
{
    private readonly ISet<Type> _derivedTypes;
    private readonly Fixture _fixture;

    internal PolymorphicCustomizationBuilder(Fixture fixture)
    {
        _derivedTypes = new HashSet<Type>();
        _fixture = fixture;
    }

    public PolymorphicCustomizationBuilder<TBaseType> WithDerivedType<TDerivedType>()
        where TDerivedType : TBaseType
    {
        _derivedTypes.Add(typeof(TDerivedType));
        return this;
    }

    public void BuildCustomization() =>
        _fixture.Customizations.Add(
            item: new FilteringSpecimenBuilder(
                new RandomRelayCustomization(builders: _derivedTypes
                    .Select(x =>
                        new TypeRelay(typeof(TBaseType), x)
                    )),
                new ExactTypeSpecification(typeof(TBaseType))
            )
        );
}