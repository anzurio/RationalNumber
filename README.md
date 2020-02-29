# Rational Number

This repository contains a .NET Core command line program written in C# that will take operations on fractions as an input and produce a fractional result.

## Definition

[Three projects](https://github.com/anzurio/RationalNumber/projects) were created to track the basic functionality of this application:

- [Rational Number Representation](https://github.com/anzurio/RationalNumber/projects/4)
- [Rational Number Arithmetic Operations](https://github.com/anzurio/RationalNumber/projects/3)
- [Rational Number Arithmetic Operations Shell](https://github.com/anzurio/RationalNumber/projects/5)

## Components

```csharp
// TODO
```


## Getting Started

### Prerequistes

In order to being able to run this application, .NET Core 3.0 **MUST** be installed in your computer with the `dotnet` command line tool.

### Running the Application

In order to run the Shell Application, execute the follwing from this reposity's root folder:

```csharp
> dotnet run -p RationalNumberShell/RationalNumberShell.csproj
```

### Running Tests

In order to run the test cases of the solution in the command line, execute the following from this repository's root folder:

```
> dotnet test RationalNumber.sln
```

Alternatively, you may run directly from the [Test Folder](./Anzurio.Rational.Tests):

```
> dotnet test Anzurio.Rational.Tests.csproj
```

