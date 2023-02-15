namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Models.Discriminators;

internal record IntegerDiscriminator(int IntValue) : IDiscriminator
{
    public string Type => "integer";

    public IOpenApiAny Value =>
        new OpenApiInteger(IntValue);

    public override string ToString() =>
        IntValue.ToString();
}