using FractionLib;
using Xunit;

namespace FractionLib.Tests;

public class FractionTests
{
    [Fact]
    public void Constructor_WithValidValues_CreatesFraction()
    {
        var fraction = new Fraction(3, 4);
        
        Assert.Equal(3, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void Constructor_SimplifiesFraction()
    {
        var fraction = new Fraction(6, 8);
        
        Assert.Equal(3, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void Constructor_NormalizesDenominatorToPositive()
    {
        var fraction = new Fraction(3, -4);
        
        Assert.Equal(-3, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void Constructor_WithZeroDenominator_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Fraction(1, 0));
    }

    [Fact]
    public void Constructor_WithNegativeNumeratorAndDenominator_CreatesPositiveFraction()
    {
        var fraction = new Fraction(-3, -4);
        
        Assert.Equal(3, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Theory]
    [InlineData(0, 1, true)]
    [InlineData(5, 3, false)]
    [InlineData(-2, 5, false)]
    public void IsZero_ReturnsCorrectValue(int numerator, int denominator, bool expected)
    {
        var fraction = new Fraction(numerator, denominator);
        
        Assert.Equal(expected, fraction.IsZero);
    }

    [Theory]
    [InlineData(3, 4, true)]
    [InlineData(-3, 4, false)]
    [InlineData(0, 1, false)]
    public void IsPositive_ReturnsCorrectValue(int numerator, int denominator, bool expected)
    {
        var fraction = new Fraction(numerator, denominator);
        
        Assert.Equal(expected, fraction.IsPositive);
    }

    [Theory]
    [InlineData(-3, 4, true)]
    [InlineData(3, 4, false)]
    [InlineData(0, 1, false)]
    public void IsNegative_ReturnsCorrectValue(int numerator, int denominator, bool expected)
    {
        var fraction = new Fraction(numerator, denominator);
        
        Assert.Equal(expected, fraction.IsNegative);
    }

    [Fact]
    public void Addition_WithSameDenominator_ReturnsCorrectSum()
    {
        var left = new Fraction(1, 4);
        var right = new Fraction(2, 4);
        
        var result = left + right;
        
        Assert.Equal(3, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Fact]
    public void Addition_WithDifferentDenominators_ReturnsCorrectSum()
    {
        var left = new Fraction(1, 3);
        var right = new Fraction(1, 4);
        
        var result = left + right;
        
        Assert.Equal(7, result.Numerator);
        Assert.Equal(12, result.Denominator);
    }

    [Fact]
    public void Addition_WithNegativeFractions_ReturnsCorrectSum()
    {
        var left = new Fraction(-1, 2);
        var right = new Fraction(-1, 3);
        
        var result = left + right;
        
        Assert.Equal(-5, result.Numerator);
        Assert.Equal(6, result.Denominator);
    }

    [Fact]
    public void Subtraction_WithSameDenominator_ReturnsCorrectDifference()
    {
        var left = new Fraction(3, 4);
        var right = new Fraction(1, 4);
        
        var result = left - right;
        
        Assert.Equal(1, result.Numerator);
        Assert.Equal(2, result.Denominator);
    }

    [Fact]
    public void Subtraction_WithDifferentDenominators_ReturnsCorrectDifference()
    {
        var left = new Fraction(2, 3);
        var right = new Fraction(1, 4);
        
        var result = left - right;
        
        Assert.Equal(5, result.Numerator);
        Assert.Equal(12, result.Denominator);
    }

    [Fact]
    public void Multiplication_ReturnsCorrectProduct()
    {
        var left = new Fraction(2, 3);
        var right = new Fraction(3, 4);
        
        var result = left * right;
        
        Assert.Equal(1, result.Numerator);
        Assert.Equal(2, result.Denominator);
    }

    [Fact]
    public void Multiplication_WithNegativeFraction_ReturnsNegativeProduct()
    {
        var left = new Fraction(-2, 3);
        var right = new Fraction(3, 4);
        
        var result = left * right;
        
        Assert.Equal(-1, result.Numerator);
        Assert.Equal(2, result.Denominator);
    }

    [Fact]
    public void Division_ReturnsCorrectQuotient()
    {
        var left = new Fraction(2, 3);
        var right = new Fraction(3, 4);
        
        var result = left / right;
        
        Assert.Equal(8, result.Numerator);
        Assert.Equal(9, result.Denominator);
    }

    [Fact]
    public void Division_ByZeroFraction_ThrowsDivideByZeroException()
    {
        var left = new Fraction(2, 3);
        var right = Fraction.Zero;
        
        Assert.Throws<DivideByZeroException>(() => left / right);
    }

    [Fact]
    public void UnaryMinus_ReturnsNegatedFraction()
    {
        var fraction = new Fraction(3, 4);
        
        var result = -fraction;
        
        Assert.Equal(-3, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Theory]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(2, 4, 1, 2, true)]
    [InlineData(1, 2, 1, 3, false)]
    public void Equality_ReturnsCorrectResult(int num1, int den1, int num2, int den2, bool expected)
    {
        var left = new Fraction(num1, den1);
        var right = new Fraction(num2, den2);
        
        Assert.Equal(expected, left == right);
        Assert.Equal(!expected, left != right);
        Assert.Equal(expected, left.Equals(right));
    }

    [Theory]
    [InlineData(1, 3, 1, 2, true)]
    [InlineData(1, 2, 1, 3, false)]
    [InlineData(1, 2, 1, 2, false)]
    public void LessThan_ReturnsCorrectResult(int num1, int den1, int num2, int den2, bool expected)
    {
        var left = new Fraction(num1, den1);
        var right = new Fraction(num2, den2);
        
        Assert.Equal(expected, left < right);
    }

    [Theory]
    [InlineData(1, 2, 1, 3, true)]
    [InlineData(1, 3, 1, 2, false)]
    [InlineData(1, 2, 1, 2, false)]
    public void GreaterThan_ReturnsCorrectResult(int num1, int den1, int num2, int den2, bool expected)
    {
        var left = new Fraction(num1, den1);
        var right = new Fraction(num2, den2);
        
        Assert.Equal(expected, left > right);
    }

    [Theory]
    [InlineData(1, 3, 1, 2, true)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(1, 2, 1, 3, false)]
    public void LessThanOrEqual_ReturnsCorrectResult(int num1, int den1, int num2, int den2, bool expected)
    {
        var left = new Fraction(num1, den1);
        var right = new Fraction(num2, den2);
        
        Assert.Equal(expected, left <= right);
    }

    [Theory]
    [InlineData(1, 2, 1, 3, true)]
    [InlineData(1, 2, 1, 2, true)]
    [InlineData(1, 3, 1, 2, false)]
    public void GreaterThanOrEqual_ReturnsCorrectResult(int num1, int den1, int num2, int den2, bool expected)
    {
        var left = new Fraction(num1, den1);
        var right = new Fraction(num2, den2);
        
        Assert.Equal(expected, left >= right);
    }

    [Fact]
    public void CompareTo_ReturnsCorrectOrdering()
    {
        var smaller = new Fraction(1, 3);
        var larger = new Fraction(1, 2);
        var equal = new Fraction(2, 4);
        
        Assert.True(smaller.CompareTo(larger) < 0);
        Assert.True(larger.CompareTo(smaller) > 0);
        Assert.Equal(0, larger.CompareTo(equal));
    }

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

    [Fact]
    public void ExplicitConversion_ToDecimal_ReturnsCorrectValue()
    {
        var fraction = new Fraction(1, 4);
        
        var result = (decimal)fraction;
        
        Assert.Equal(0.25m, result);
    }

    [Fact]
    public void Reciprocal_ReturnsCorrectValue()
    {
        var fraction = new Fraction(3, 4);
        
        var result = fraction.Reciprocal();
        
        Assert.Equal(4, result.Numerator);
        Assert.Equal(3, result.Denominator);
    }

    [Fact]
    public void Reciprocal_OfNegativeFraction_ReturnsNegativeReciprocal()
    {
        var fraction = new Fraction(-3, 4);
        
        var result = fraction.Reciprocal();
        
        Assert.Equal(-4, result.Numerator);
        Assert.Equal(3, result.Denominator);
    }

    [Fact]
    public void Reciprocal_OfZero_ThrowsInvalidOperationException()
    {
        var fraction = Fraction.Zero;
        
        Assert.Throws<InvalidOperationException>(() => fraction.Reciprocal());
    }

    [Fact]
    public void Abs_OfNegativeFraction_ReturnsPositiveFraction()
    {
        var fraction = new Fraction(-3, 4);
        
        var result = fraction.Abs();
        
        Assert.Equal(3, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Fact]
    public void Abs_OfPositiveFraction_ReturnsSameFraction()
    {
        var fraction = new Fraction(3, 4);
        
        var result = fraction.Abs();
        
        Assert.Equal(3, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Theory]
    [InlineData("3/4", 3, 4)]
    [InlineData("5", 5, 1)]
    [InlineData("-2/3", -2, 3)]
    [InlineData("6/8", 3, 4)]
    public void Parse_WithValidString_ReturnsFraction(string input, int expectedNum, int expectedDen)
    {
        var result = Fraction.Parse(input);
        
        Assert.Equal(expectedNum, result.Numerator);
        Assert.Equal(expectedDen, result.Denominator);
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

    [Fact]
    public void TryParse_WithValidString_ReturnsTrue()
    {
        var success = Fraction.TryParse("3/4", out var result);
        
        Assert.True(success);
        Assert.Equal(3, result.Numerator);
        Assert.Equal(4, result.Denominator);
    }

    [Fact]
    public void TryParse_WithInvalidString_ReturnsFalse()
    {
        var success = Fraction.TryParse("invalid", out var result);
        
        Assert.False(success);
        Assert.Equal(Fraction.Zero, result);
    }

    [Theory]
    [InlineData(1, 1, "1")]
    [InlineData(3, 4, "3/4")]
    [InlineData(0, 1, "0")]
    [InlineData(-5, 3, "-5/3")]
    public void ToString_ReturnsCorrectFormat(int numerator, int denominator, string expected)
    {
        var fraction = new Fraction(numerator, denominator);
        
        Assert.Equal(expected, fraction.ToString());
    }

    [Fact]
    public void GetHashCode_ForEqualFractions_ReturnsSameValue()
    {
        var fraction1 = new Fraction(1, 2);
        var fraction2 = new Fraction(2, 4);
        
        Assert.Equal(fraction1.GetHashCode(), fraction2.GetHashCode());
    }

    [Fact]
    public void StaticProperties_ReturnCorrectValues()
    {
        Assert.Equal(new Fraction(0, 1), Fraction.Zero);
        Assert.Equal(new Fraction(1, 1), Fraction.One);
    }
}
