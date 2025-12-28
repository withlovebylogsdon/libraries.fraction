# WLBLFractionLibrary

Immutable Fraction and MixedNumber types with arithmetic, parsing and conversions.

## Installation

Install via NuGet:

```powershell
dotnet add package WLBLFractionLibrary --version 1.0.0
```

## Usage

```csharp
using FractionLibrary;

var half = new Fraction(1, 2);
var sum = half + new Fraction(1, 3);
Console.WriteLine(sum); // 5/6
```

## Features

- Immutable `Fraction` and `MixedNumber` value types
- Automatic simplification
- Arithmetic and comparison operators
- Parsing and conversions

## License

MIT
