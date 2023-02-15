Status:

[![NuGet](https://img.shields.io/nuget/dt/PolymorphicContracts.TypeDiscriminatorSwaggerSetup.svg)](https://www.nuget.org/packages/PolymorphicContracts.TypeDiscriminatorSwaggerSetup/)

# PolymorphicContracts.TypeDiscriminatorSwaggerSetup

## Installation

### NuGet

Install package : [https://www.nuget.org/packages/PolymorphicContracts.TypeDiscriminatorSwaggerSetup](https://www.nuget.org/packages/PolymorphicContracts.TypeDiscriminatorSwaggerSetup).

### GitHub

- Clone locally this github repository
- Build the `PolymorphicContracts.TypeDiscriminatorSwaggerSetup.sln` solution

## Usage

In your ASP.NET Core 7 project choose assemblies, that contains hierarchies marked with [`JsonDerivedTypeAttribute`](https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/polymorphism?pivots=dotnet-7-0).

List them in `appsettings.json` files as follows:

```json lines
{
  //... your config
  "JsonHierarchies": {
    "Assemblies": [
      "PolymorphicContracts.Models"
    ]
  }
}
```

Then, while configuring services, after adding SwaggerGen call special extension:

```csharp
builder.Services.AddTypeDiscriminatorToSwagger(builder.Configuration);
```

### Rules for hierarchies

1. Use same discriminator type. Whether it's all `string`, whether it's all `int`.
2. Hierarchy root type must be `abstract record` or `abstract class`.
3. Hierarchy must be "flat", i.e. you inherit only hierarchy root.

**Wrong** hierarchy example, breaking **all rules**:

```csharp
[JsonDerivedType(typeof(DerivedFirst), typeDiscriminator: 1)]
[JsonDerivedType(typeof(DerivedSecond), typeDiscriminator: "2")]
interface IBase
{
}

record DerivedFirst : IBase;

record DerivedSecond : DerivedFirst;
```