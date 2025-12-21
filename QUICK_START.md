# Quick Start Guide - FractionLib

## Installation

### From Source
1. Clone or download the repository
2. Open `FractionLib.sln` in Visual Studio 2022 or later
3. Build the solution (Ctrl+Shift+B)

### Command Line
```bash
cd FractionLib
dotnet restore
dotnet build
```

## Running Tests

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test class
dotnet test --filter "FullyQualifiedName~FractionTests"
```

## Running the Demo

```bash
dotnet run --project examples/FractionDemo/FractionDemo.csproj
```

## Basic Usage

### 1. Add Reference to Your Project

```xml
<ItemGroup>
  <ProjectReference Include="path\to\FractionLib.csproj" />
</ItemGroup>
```

### 2. Import Namespace

```csharp
using FractionLib;
```

### 3. Create and Use Fractions

```csharp
// Create fractions
var oneHalf = new Fraction(1, 2);
var oneThird = new Fraction(1, 3);

// Arithmetic
var sum = oneHalf + oneThird;      // 5/6
var product = oneHalf * oneThird;  // 1/6

// Automatic simplification
var simplified = new Fraction(4, 8);  // Becomes 1/2

// Comparisons
if (oneHalf > oneThird)
{
    Console.WriteLine("1/2 is greater than 1/3");
}

// Conversions
double asDouble = (double)oneHalf;  // 0.5
Fraction fromInt = 3;               // 3/1
```

### 4. Work with Mixed Numbers

```csharp
// Create mixed numbers
var twoAndHalf = new MixedNumber(2, 1, 2);      // 2 1/2
var threeQuarters = new MixedNumber(0, 3, 4);   // 3/4

// Arithmetic
var result = twoAndHalf + threeQuarters;  // 3 1/4

// Convert to improper fraction
var improper = twoAndHalf.ToImproperFraction();  // 5/2

// Convert from fraction
var mixed = MixedNumber.FromFraction(new Fraction(7, 3));  // 2 1/3
```

### 5. Parsing from Strings

```csharp
// Parse fractions
var f1 = Fraction.Parse("3/4");
var f2 = Fraction.Parse("5");  // Becomes 5/1

// Parse mixed numbers
var m1 = MixedNumber.Parse("2 3/4");
var m2 = MixedNumber.Parse("5/3");  // Becomes 1 2/3
var m3 = MixedNumber.Parse("4");    // Becomes 4

// Safe parsing
if (Fraction.TryParse("3/4", out var fraction))
{
    Console.WriteLine($"Parsed: {fraction}");
}
```

## Common Patterns

### Recipe Scaling
```csharp
var originalFlour = new MixedNumber(2, 1, 2);  // 2 1/2 cups
var scaleFactor = new MixedNumber(1, 1, 2);    // 1.5x recipe
var neededFlour = originalFlour * scaleFactor;  // 3 3/4 cups
```

### Fraction Arithmetic Chain
```csharp
var result = new Fraction(1, 2) + new Fraction(1, 3) - new Fraction(1, 6);
Console.WriteLine(result);  // 2/3
```

### Sorting Fractions
```csharp
var fractions = new[] {
    new Fraction(3, 4),
    new Fraction(1, 2),
    new Fraction(2, 3)
};
Array.Sort(fractions);  // Now sorted: 1/2, 2/3, 3/4
```

### Finding Reciprocal
```csharp
var fraction = new Fraction(3, 4);
var reciprocal = fraction.Reciprocal();  // 4/3
var product = fraction * reciprocal;      // 1/1
```

### Absolute Value
```csharp
var negative = new Fraction(-3, 4);
var positive = negative.Abs();  // 3/4
```

## Error Handling

The library throws specific exceptions for invalid operations:

```csharp
try
{
    // Division by zero
    var result = new Fraction(1, 2) / Fraction.Zero;
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Cannot divide by zero!");
}

try
{
    // Invalid denominator
    var invalid = new Fraction(1, 0);
}
catch (ArgumentException ex)
{
    Console.WriteLine("Denominator cannot be zero!");
}

try
{
    // Reciprocal of zero
    var reciprocal = Fraction.Zero.Reciprocal();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Cannot get reciprocal of zero!");
}

try
{
    // Invalid parsing
    var parsed = Fraction.Parse("invalid");
}
catch (FormatException ex)
{
    Console.WriteLine("Invalid fraction format!");
}
```

## Tips and Best Practices

1. **Use readonly variables** when possible to maintain immutability
2. **Prefer TryParse** over Parse when input validation is uncertain
3. **Use implicit conversions** from int when creating whole number fractions
4. **Check IsZero** before division or reciprocal operations
5. **Use ToString()** for display, not for computation
6. **Remember fractions are auto-simplified** - 2/4 becomes 1/2

## Performance Notes

- Both types are structs (value types) - allocated on stack
- No heap allocations for arithmetic operations
- GCD algorithm is O(log min(a,b))
- All operations are thread-safe due to immutability

## Next Steps

- Read the full [README.md](README.md) for comprehensive documentation
- Review [PROJECT_SUMMARY.md](PROJECT_SUMMARY.md) for architecture details
- Explore the test files for more usage examples
- Run the demo application to see all features in action

## Getting Help

If you encounter issues:
1. Check the unit tests for usage examples
2. Review the XML documentation comments
3. Run the demo application
4. Examine the PROJECT_SUMMARY.md for design decisions

## Contributing

This is a demonstration project. Key areas for enhancement:
- Additional mathematical operations
- Performance optimizations
- Extended parsing formats
- Serialization support
