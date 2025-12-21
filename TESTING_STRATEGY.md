# Testing Strategy - FractionLib

## Overview
This document outlines the comprehensive testing strategy used in FractionLib, demonstrating clean code principles applied to test design.

## Testing Framework
- **xUnit** - Modern, extensible testing framework
- **Theory-based testing** - Data-driven tests with InlineData
- **AAA Pattern** - Arrange, Act, Assert structure

## Test Organization

### Test Class Structure
```
FractionLib.Tests/
├── FractionTests.cs      (66+ test cases)
│   ├── Constructor tests
│   ├── Arithmetic operation tests
│   ├── Comparison operation tests
│   ├── Conversion tests
│   ├── Parsing tests
│   └── Utility method tests
│
└── MixedNumberTests.cs   (48+ test cases)
    ├── Constructor and normalization tests
    ├── Arithmetic operation tests
    ├── Comparison operation tests
    ├── Conversion tests
    ├── Parsing tests
    └── Complex scenario tests
```

## Test Categories

### 1. Constructor Tests
**Purpose**: Validate object creation and automatic normalization

```csharp
[Fact]
public void Constructor_WithValidValues_CreatesFraction()
{
    // Arrange & Act
    var fraction = new Fraction(3, 4);
    
    // Assert
    Assert.Equal(3, fraction.Numerator);
    Assert.Equal(4, fraction.Denominator);
}

[Fact]
public void Constructor_SimplifiesFraction()
{
    // Validates GCD-based simplification
    var fraction = new Fraction(6, 8);
    Assert.Equal(3, fraction.Numerator);
    Assert.Equal(4, fraction.Denominator);
}
```

**Coverage:**
- Valid value construction
- Automatic simplification
- Sign normalization
- Zero denominator rejection
- Edge cases (zero, negative values)

### 2. Arithmetic Operation Tests
**Purpose**: Ensure mathematical correctness

```csharp
[Fact]
public void Addition_WithDifferentDenominators_ReturnsCorrectSum()
{
    // Arrange
    var left = new Fraction(1, 3);
    var right = new Fraction(1, 4);
    
    // Act
    var result = left + right;
    
    // Assert
    Assert.Equal(7, result.Numerator);
    Assert.Equal(12, result.Denominator);
}
```

**Coverage:**
- Addition (same/different denominators)
- Subtraction (positive/negative results)
- Multiplication
- Division
- Unary negation
- Edge cases (zero, negative operands)
- Division by zero validation

### 3. Comparison Tests
**Purpose**: Validate ordering and equality semantics

```csharp
[Theory]
[InlineData(1, 2, 1, 2, true)]   // Equal fractions
[InlineData(2, 4, 1, 2, true)]   // Equivalent fractions
[InlineData(1, 2, 1, 3, false)]  // Different fractions
public void Equality_ReturnsCorrectResult(int n1, int d1, int n2, int d2, bool expected)
{
    var left = new Fraction(n1, d1);
    var right = new Fraction(n2, d2);
    
    Assert.Equal(expected, left == right);
    Assert.Equal(!expected, left != right);
    Assert.Equal(expected, left.Equals(right));
}
```

**Coverage:**
- Equality (==, !=, Equals)
- Relational (<, >, <=, >=)
- CompareTo implementation
- Transitive property validation
- Simplified vs unsimplified comparison

### 4. Conversion Tests
**Purpose**: Validate type conversions and casting

```csharp
[Fact]
public void ImplicitConversion_FromInt_CreatesFraction()
{
    Fraction fraction = 5;
    
    Assert.Equal(5, fraction.Numerator);
    Assert.Equal(1, fraction.Denominator);
}

[Fact]
public void ExplicitConversion_ToDouble_ReturnsCorrectValue()
{
    var fraction = new Fraction(1, 4);
    var result = (double)fraction;
    Assert.Equal(0.25, result);
}
```

**Coverage:**
- Implicit conversions (int → Fraction/MixedNumber)
- Explicit conversions (to double/decimal)
- Fraction ↔ MixedNumber conversions
- Precision preservation

### 5. Parsing Tests
**Purpose**: Validate string parsing and formatting

```csharp
[Theory]
[InlineData("3/4", 3, 4)]
[InlineData("5", 5, 1)]
[InlineData("-2/3", -2, 3)]
[InlineData("6/8", 3, 4)]  // Simplification during parse
public void Parse_WithValidString_ReturnsFraction(string input, int num, int den)
{
    var result = Fraction.Parse(input);
    Assert.Equal(num, result.Numerator);
    Assert.Equal(den, result.Denominator);
}

[Theory]
[InlineData("")]
[InlineData("   ")]
[InlineData("abc")]
[InlineData("1/2/3")]
public void Parse_WithInvalidString_ThrowsFormatException(string input)
{
    Assert.Throws<FormatException>(() => Fraction.Parse(input));
}
```

**Coverage:**
- Parse success cases
- Parse failure cases
- TryParse validation
- Format validation
- Whitespace handling
- Mixed number parsing (whole + fraction)

### 6. Utility Method Tests
**Purpose**: Validate helper operations

```csharp
[Fact]
public void Reciprocal_ReturnsCorrectValue()
{
    var fraction = new Fraction(3, 4);
    var result = fraction.Reciprocal();
    
    Assert.Equal(4, result.Numerator);
    Assert.Equal(3, result.Denominator);
}

[Fact]
public void Reciprocal_OfZero_ThrowsInvalidOperationException()
{
    var fraction = Fraction.Zero;
    Assert.Throws<InvalidOperationException>(() => fraction.Reciprocal());
}
```

**Coverage:**
- Reciprocal calculation
- Absolute value
- Special properties (IsZero, IsPositive, IsNegative)
- Static constants (Zero, One)
- ToString formatting

### 7. Edge Case Tests
**Purpose**: Ensure robustness with boundary conditions

**Covered Edge Cases:**
- Zero numerator
- Negative numerator and/or denominator
- Large numbers near Int32 limits
- Improper fractions
- Self-operations (x + x, x * x)
- Identity operations (x + 0, x * 1)
- Overflow scenarios in multiplication

### 8. Complex Scenario Tests
**Purpose**: Validate real-world usage patterns

```csharp
[Fact]
public void ComplexArithmetic_MultipleOperations_ProducesCorrectResult()
{
    var a = new MixedNumber(2, 1, 2);  // 2 1/2
    var b = new MixedNumber(1, 1, 4);  // 1 1/4
    var c = new MixedNumber(3, 0, 1);  // 3
    
    var result = (a + b) * c - new MixedNumber(1, 1, 2);
    
    // (2 1/2 + 1 1/4) * 3 - 1 1/2 = 3 3/4 * 3 - 1 1/2 = 11 1/4 - 1 1/2 = 9 3/4
    Assert.Equal(9, result.Whole);
    Assert.Equal(3, result.FractionNumerator);
    Assert.Equal(4, result.FractionDenominator);
}
```

## Test Naming Conventions

### Pattern: `MethodName_Condition_ExpectedBehavior`

Examples:
- `Constructor_WithValidValues_CreatesFraction`
- `Addition_WithSameDenominator_ReturnsCorrectSum`
- `Parse_WithInvalidString_ThrowsFormatException`

### Benefits:
- Self-documenting test purpose
- Easy to identify failing tests
- Clear intent for maintenance

## Test Data Strategies

### 1. Inline Data (Theory Tests)
Used for testing multiple inputs with same logic:

```csharp
[Theory]
[InlineData(1, 2, true)]
[InlineData(-1, 2, false)]
[InlineData(0, 1, false)]
public void IsPositive_ReturnsCorrectValue(int num, int den, bool expected)
{
    var fraction = new Fraction(num, den);
    Assert.Equal(expected, fraction.IsPositive);
}
```

### 2. Fact Tests
Used for single, specific test cases:

```csharp
[Fact]
public void Division_ByZeroFraction_ThrowsDivideByZeroException()
{
    var left = new Fraction(2, 3);
    var right = Fraction.Zero;
    Assert.Throws<DivideByZeroException>(() => left / right);
}
```

## Test Independence

### Principles:
1. **No shared state** between tests
2. **Each test creates its own data** (Arrange phase)
3. **Tests can run in any order**
4. **No dependencies between tests**

### Implementation:
```csharp
[Fact]
public void Example_Test()
{
    // Arrange - Create fresh objects
    var fraction = new Fraction(1, 2);
    
    // Act - Perform operation
    var result = fraction + new Fraction(1, 3);
    
    // Assert - Verify outcome
    Assert.Equal(new Fraction(5, 6), result);
    
    // No cleanup needed - value types, no resources
}
```

## Test Coverage Metrics

### Current Coverage:
- **Fraction**: 66 test cases
- **MixedNumber**: 48 test cases
- **Total**: 114+ test cases

### Coverage Areas:
- ✅ All public constructors
- ✅ All arithmetic operators
- ✅ All comparison operators
- ✅ All conversions
- ✅ All public methods
- ✅ All public properties
- ✅ Error conditions
- ✅ Edge cases
- ✅ Complex scenarios

### Line Coverage (Estimated):
- ~95% of production code
- 100% of public API surface
- All error paths tested

## Testing Best Practices Applied

### 1. AAA Pattern
Every test follows Arrange-Act-Assert:
```csharp
[Fact]
public void Method_Condition_Behavior()
{
    // Arrange - Set up test data
    var input = new Fraction(1, 2);
    
    // Act - Execute operation
    var result = input.Reciprocal();
    
    // Assert - Verify result
    Assert.Equal(new Fraction(2, 1), result);
}
```

### 2. Single Assertion Focus
Each test validates one specific behavior:
```csharp
// Good - Tests one thing
[Fact]
public void Addition_ReturnsCorrectNumerator()
{
    var result = new Fraction(1, 2) + new Fraction(1, 3);
    Assert.Equal(5, result.Numerator);
}

// Also good - Related assertions OK
[Fact]
public void Addition_ReturnsCorrectFraction()
{
    var result = new Fraction(1, 2) + new Fraction(1, 3);
    Assert.Equal(5, result.Numerator);
    Assert.Equal(6, result.Denominator);
}
```

### 3. Clear Test Names
Names describe what is tested and expected:
- ✅ `Constructor_WithZeroDenominator_ThrowsArgumentException`
- ❌ `Test1`, `TestConstructor`, `TestAdd`

### 4. No Test Logic
Tests are straightforward, no complex conditionals:
```csharp
// Good - Simple assertion
Assert.Equal(expected, actual);

// Avoid - Complex test logic
if (condition) {
    Assert.Equal(a, b);
} else {
    Assert.Equal(c, d);
}
```

### 5. Test Isolation
No dependencies on external state or other tests:
```csharp
[Fact]
public void IsolatedTest()
{
    // Create everything needed
    var data = CreateTestData();
    
    // Perform test
    var result = PerformOperation(data);
    
    // Assert
    Assert.NotNull(result);
    
    // No cleanup needed - value types
}
```

## Running Tests

### Command Line
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity normal

# Run specific test class
dotnet test --filter "FullyQualifiedName~FractionTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~Addition_WithSameDenominator_ReturnsCorrectSum"

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### Visual Studio
- Test Explorer (Ctrl+E, T)
- Run All Tests
- Debug Tests
- Run Failed Tests
- Code Coverage

## Continuous Integration Ready

Tests are designed for CI/CD pipelines:
- Fast execution (< 1 second total)
- No external dependencies
- Deterministic results
- Clear pass/fail indicators
- No flaky tests

## Test Maintenance

### Guidelines:
1. **Update tests with code changes**
2. **Keep test names synchronized** with functionality
3. **Remove obsolete tests** when refactoring
4. **Add tests for bug fixes**
5. **Review test coverage regularly**

### Red-Green-Refactor Cycle:
1. Write failing test (Red)
2. Implement minimum code to pass (Green)
3. Refactor while keeping tests green (Refactor)

## Conclusion

The testing strategy for FractionLib demonstrates:
- Comprehensive coverage of all functionality
- Clean, maintainable test code
- Industry-standard practices
- Clear documentation through tests
- Robustness through edge case testing
- Professional software development approach

Tests serve as both validation and documentation, making the library reliable and easy to understand.
