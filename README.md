# Rational Number

This repository contains a .NET Core command line program written in C# that will take operations on fractions as an input and produce a fractional result.

## Definition

[Three projects](https://github.com/anzurio/RationalNumber/projects) were created to track the basic functionality of this application:

- [Rational Number Representation](https://github.com/anzurio/RationalNumber/projects/4)
- [Rational Number Arithmetic Operations](https://github.com/anzurio/RationalNumber/projects/3)
- [Rational Number Arithmetic Operations Shell](https://github.com/anzurio/RationalNumber/projects/5)

## Components

- [Rational Number Library](./Anzurio.Rational) contains a RationalNumber class that represents a Rational Number and is able to parse fractions in the following syntax `[whole_]numerator/denominatory`.
- [Rational Number Tests Library](./Anzurio.Rational.Tests) contains basic unit tests using NUnit and FluentAssertions.
- [Rational Number Shell](./RationalNumberShell) contains a basic command line shell to solve arithmetic operations between two fractions.

## Getting Started

### Prerequistes

In order to to run these projects, [.NET Core 2.2 or newer](https://dotnet.microsoft.com/download/dotnet-core) **MUST** be installed in your computer along with the `dotnet` command line tool.

### Running the Application

In order to run the Shell Application, execute the follwing from this reposity's root folder:

```csharp
> dotnet run -p RationalNumberShell/RationalNumberShell.csproj
```

While running the application, enter `help` to get a quick guide of how to enter valid expressions. This will also be displayed after a few consecutive unsuccessful attempts at solving an equation.

### Running Tests

In order to run the test cases of the solution in the command line, execute the following from this repository's root folder:

```
> dotnet test RationalNumber.sln
```

Alternatively, you may run directly from the [Test Folder](./Anzurio.Rational.Tests):

```
> dotnet test Anzurio.Rational.Tests.csproj
```

