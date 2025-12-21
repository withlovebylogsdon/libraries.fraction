# FractionLib

A comprehensive C# library providing immutable value types for working with fractions and mixed numbers, with full support for arithmetic operations and comparisons.

## Features

- **Fraction Value Type**: Immutable struct representing fractions with numerator and denominator
- **MixedNumber Value Type**: Immutable struct representing mixed numbers (whole + fraction)
- **Automatic Simplification**: Fractions are automatically reduced to lowest terms
- **Full Arithmetic Support**: Addition, subtraction, multiplication, division
- **Comparison Operators**: All standard comparison operators implemented
- **Type Conversions**: Implicit and explicit conversions between types
- **Parsing Support**: Parse fractions and mixed numbers from strings
- **Clean Code Principles**: Well-structured, testable, and maintainable code
- **Comprehensive Unit Tests**: Over 100 unit tests covering all functionality

## Project Structure

```
FractionLib/
├── src/
│   └── FractionLib/
│       ├── Fraction.cs          # Fraction value type
│       ├── MixedNumber.cs       # Mixed number value type
│       └── FractionLib.csproj
├── tests/
│   └── FractionLib.Tests/
│       ├── FractionTests.cs     # Comprehensive Fraction tests
│       ├── MixedNumberTests.cs  # Comprehensive MixedNumber tests
│       └── FractionLib.Tests.csproj
└── FractionLib.sln
```

## Usage Examples

### Working with Fractions

```csharp
using FractionLib;

// Create fractions
var half = new Fraction(1, 2);
var third = new Fraction(1, 3);

// Arithmetic operations
var sum = half + third;           // 5/6
var difference = half - third;    // 1/6
var product = half * third;       // 1/6
var quotient = half / third;      // 3/2

// Automatic simplification
var simplified = new Fraction(6, 8);  // Becomes 3/4

// Comparisons
bool isLess = half < third;       // false
bool isEqual = half == new Fraction(2, 4);  // true

// Conversions
Fraction fromInt = 5;             // Implicit: 5/1
double asDouble = (double)half;   // Explicit: 0.5

// Parsing
var parsed = Fraction.Parse("3/4");
bool success = Fraction.TryParse("2/5", out var result);
```

### Working with Mixed Numbers

```csharp
using FractionLib;

// Create mixed numbers
var twoAndHalf = new MixedNumber(2, 1, 2);      // 2 1/2
var threeQuarters = new MixedNumber(0, 3, 4);   // 3/4

// Arithmetic operations
var sum = twoAndHalf + threeQuarters;           // 3 1/4
var product = twoAndHalf * new MixedNumber(2, 0, 1);  // 5

// Conversions
var improper = twoAndHalf.ToImproperFraction(); // 5/2
var fromFraction = MixedNumber.FromFraction(new Fraction(7, 4));  // 1 3/4

// Implicit conversions
MixedNumber fromInt = 3;                        // 3
MixedNumber fromFraction = new Fraction(5, 2); // 2 1/2

// Parsing
var parsed = MixedNumber.Parse("2 3/4");        // 2 3/4
var fromString = MixedNumber.Parse("7/4");      // 1 3/4
```

## Building the Project

### Prerequisites
- .NET 8.0 SDK or later

### Build Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Build in Release mode
dotnet build -c Release
```

## Running Tests

The project includes comprehensive unit tests using xUnit:

```bash
# Run all tests
dotnet test

# Run tests with code coverage
dotnet test /p:CollectCoverage=true

# Run specific test class
dotnet test --filter "FullyQualifiedName~FractionTests"
```

## Clean Code Principles Applied

1. **Single Responsibility**: Each type has a clear, focused purpose
2. **Immutability**: Both value types are immutable for thread safety
3. **Meaningful Names**: Clear, descriptive names for all methods and properties
4. **Small Functions**: Methods are concise and focused
5. **DRY (Don't Repeat Yourself)**: Common logic is extracted and reused
6. **Comprehensive Testing**: High test coverage with clear test names
7. **Error Handling**: Proper exceptions with meaningful messages
8. **Documentation**: XML documentation comments on public APIs

## Key Design Decisions

- **Value Types (struct)**: Both types are structs for performance and value semantics
- **Readonly**: All fields are readonly for immutability
- **Automatic Normalization**: Fractions always stored in simplest form
- **Sign Convention**: Denominators always positive; sign carried by numerator
- **GCD Algorithm**: Euclidean algorithm for efficient simplification
- **Operator Overloading**: Natural arithmetic syntax
- **IEquatable & IComparable**: Standard .NET comparison patterns

## License

This is a demonstration project for educational purposes.
