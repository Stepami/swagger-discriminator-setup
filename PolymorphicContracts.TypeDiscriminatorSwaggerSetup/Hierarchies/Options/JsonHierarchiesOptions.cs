namespace PolymorphicContracts.TypeDiscriminatorSwaggerSetup.Hierarchies.Options;

public class JsonHierarchiesOptions
{
    public const string Key = "JsonHierarchies";
    
    public List<string> Assemblies { get; set; } = new();
}