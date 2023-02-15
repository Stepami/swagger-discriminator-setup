namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models.Discriminators;

internal static class ToDiscriminatorConverter
{
    internal static IDiscriminator ToDiscriminator
        (this object? typeDiscriminator, Type derivedType) =>
        typeDiscriminator switch
        {
            int intValue => new IntegerDiscriminator(intValue),
            string strValue => new StringDiscriminator(strValue),
            null => throw new ArgumentNullException(nameof(typeDiscriminator), $"{derivedType}"),
            _ => throw new ArgumentOutOfRangeException(nameof(typeDiscriminator), $"{derivedType}")
        };
}