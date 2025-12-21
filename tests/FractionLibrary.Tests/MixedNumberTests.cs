using FractionLib;
using Xunit;

namespace FractionLib.Tests;

public class MixedNumberTests
{
    [Fact]
    public void Constructor_WithValidValues_CreatesMixedNumber()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        Assert.Equal(2, mixedNumber.Whole);
        Assert.Equal(3, mixedNumber.FractionNumerator);
        Assert.Equal(4, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void Constructor_WithImproperFraction_NormalizesToMixedNumber()
    {
        var mixedNumber = new MixedNumber(1, 5, 3);
        
        Assert.Equal(2, mixedNumber.Whole);
        Assert.Equal(2, mixedNumber.FractionNumerator);
        Assert.Equal(3, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void Constructor_WithZeroDenominator_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new MixedNumber(1, 2, 0));
    }

    [Fact]
    public void Constructor_SimplifiesFractionalPart()
    {
        var mixedNumber = new MixedNumber(1, 2, 4);
        
        Assert.Equal(1, mixedNumber.Whole);
        Assert.Equal(1, mixedNumber.FractionNumerator);
        Assert.Equal(2, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void Constructor_WithNegativeWhole_NormalizesFraction()
    {
        var mixedNumber = new MixedNumber(-2, 3, 4);
        
        Assert.Equal(-2, mixedNumber.Whole);
        Assert.Equal(3, mixedNumber.FractionNumerator);
        Assert.Equal(4, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void Constructor_WithNegativeWholeAndPositiveFraction_NormalizesCorrectly()
    {
        var mixedNumber = new MixedNumber(-1, 1, 2);
        
        // Should be normalized to -1 1/2, which internally is -2 + 1/2
        Assert.Equal(-1, mixedNumber.Whole);
        Assert.Equal(1, mixedNumber.FractionNumerator);
        Assert.Equal(2, mixedNumber.FractionDenominator);
    }

    [Theory]
    [InlineData(0, 0, 1, true)]
    [InlineData(5, 0, 1, false)]
    [InlineData(0, 1, 2, false)]
    public void IsZero_ReturnsCorrectValue(int whole, int num, int den, bool expected)
    {
        var mixedNumber = new MixedNumber(whole, num, den);
        
        Assert.Equal(expected, mixedNumber.IsZero);
    }

    [Theory]
    [InlineData(1, 1, 2, true)]
    [InlineData(0, 1, 2, true)]
    [InlineData(-1, 0, 1, false)]
    [InlineData(0, -1, 2, false)]
    public void IsPositive_ReturnsCorrectValue(int whole, int num, int den, bool expected)
    {
        var mixedNumber = new MixedNumber(whole, num, den);
        
        Assert.Equal(expected, mixedNumber.IsPositive);
    }

    [Theory]
    [InlineData(-1, -1, 2, true)]
    [InlineData(0, -1, 2, true)]
    [InlineData(1, 0, 1, false)]
    [InlineData(0, 1, 2, false)]
    public void IsNegative_ReturnsCorrectValue(int whole, int num, int den, bool expected)
    {
        var mixedNumber = new MixedNumber(whole, num, den);
        
        Assert.Equal(expected, mixedNumber.IsNegative);
    }

    [Fact]
    public void ToImproperFraction_ReturnsCorrectFraction()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        var fraction = mixedNumber.ToImproperFraction();
        
        Assert.Equal(11, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void ToImproperFraction_WithNegativeMixedNumber_ReturnsNegativeFraction()
    {
        var mixedNumber = new MixedNumber(-2, -3, 4);
        
        var fraction = mixedNumber.ToImproperFraction();
        
        Assert.Equal(-11, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void FromFraction_WithProperFraction_CreatesMixedNumber()
    {
        var fraction = new Fraction(3, 4);
        
        var mixedNumber = MixedNumber.FromFraction(fraction);
        
        Assert.Equal(0, mixedNumber.Whole);
        Assert.Equal(3, mixedNumber.FractionNumerator);
        Assert.Equal(4, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void FromFraction_WithImproperFraction_CreatesMixedNumber()
    {
        var fraction = new Fraction(11, 4);
        
        var mixedNumber = MixedNumber.FromFraction(fraction);
        
        Assert.Equal(2, mixedNumber.Whole);
        Assert.Equal(3, mixedNumber.FractionNumerator);
        Assert.Equal(4, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void Addition_ReturnsSumAsMixedNumber()
    {
        var left = new MixedNumber(1, 1, 2);
        var right = new MixedNumber(2, 1, 4);
        
        var result = left + right;
        
        Assert.Equal(3, result.Whole);
        Assert.Equal(3, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void Addition_WithFractionOverflow_NormalizesWholePart()
    {
        var left = new MixedNumber(1, 3, 4);
        var right = new MixedNumber(2, 1, 2);
        
        var result = left + right;
        
        Assert.Equal(4, result.Whole);
        Assert.Equal(1, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void Subtraction_ReturnsDifferenceAsMixedNumber()
    {
        var left = new MixedNumber(3, 1, 2);
        var right = new MixedNumber(1, 1, 4);
        
        var result = left - right;
        
        Assert.Equal(2, result.Whole);
        Assert.Equal(1, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void Subtraction_ResultingInNegative_ReturnsNegativeMixedNumber()
    {
        var left = new MixedNumber(1, 1, 4);
        var right = new MixedNumber(2, 1, 2);
        
        var result = left - right;
        
        Assert.Equal(-1, result.Whole);
        Assert.Equal(-1, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void Multiplication_ReturnsProductAsMixedNumber()
    {
        var left = new MixedNumber(2, 1, 2);
        var right = new MixedNumber(1, 1, 3);
        
        var result = left * right;
        
        Assert.Equal(3, result.Whole);
        Assert.Equal(1, result.FractionNumerator);
        Assert.Equal(3, result.FractionDenominator);
    }

    [Fact]
    public void Division_ReturnsQuotientAsMixedNumber()
    {
        var left = new MixedNumber(5, 0, 1);
        var right = new MixedNumber(2, 0, 1);
        
        var result = left / right;
        
        Assert.Equal(2, result.Whole);
        Assert.Equal(1, result.FractionNumerator);
        Assert.Equal(2, result.FractionDenominator);
    }

    [Fact]
    public void Division_ByZero_ThrowsDivideByZeroException()
    {
        var left = new MixedNumber(5, 0, 1);
        var right = MixedNumber.Zero;
        
        Assert.Throws<DivideByZeroException>(() => left / right);
    }

    [Fact]
    public void UnaryMinus_ReturnsNegatedMixedNumber()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        var result = -mixedNumber;
        
        Assert.Equal(-2, result.Whole);
        Assert.Equal(-3, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Theory]
    [InlineData(1, 1, 2, 1, 1, 2, true)]
    [InlineData(1, 2, 4, 1, 1, 2, true)]
    [InlineData(1, 1, 2, 2, 1, 2, false)]
    public void Equality_ReturnsCorrectResult(int w1, int n1, int d1, int w2, int n2, int d2, bool expected)
    {
        var left = new MixedNumber(w1, n1, d1);
        var right = new MixedNumber(w2, n2, d2);
        
        Assert.Equal(expected, left == right);
        Assert.Equal(!expected, left != right);
        Assert.Equal(expected, left.Equals(right));
    }

    [Theory]
    [InlineData(1, 1, 4, 1, 1, 2, true)]
    [InlineData(1, 1, 2, 1, 1, 4, false)]
    [InlineData(1, 1, 2, 1, 1, 2, false)]
    public void LessThan_ReturnsCorrectResult(int w1, int n1, int d1, int w2, int n2, int d2, bool expected)
    {
        var left = new MixedNumber(w1, n1, d1);
        var right = new MixedNumber(w2, n2, d2);
        
        Assert.Equal(expected, left < right);
    }

    [Theory]
    [InlineData(1, 1, 2, 1, 1, 4, true)]
    [InlineData(1, 1, 4, 1, 1, 2, false)]
    [InlineData(1, 1, 2, 1, 1, 2, false)]
    public void GreaterThan_ReturnsCorrectResult(int w1, int n1, int d1, int w2, int n2, int d2, bool expected)
    {
        var left = new MixedNumber(w1, n1, d1);
        var right = new MixedNumber(w2, n2, d2);
        
        Assert.Equal(expected, left > right);
    }

    [Fact]
    public void CompareTo_ReturnsCorrectOrdering()
    {
        var smaller = new MixedNumber(1, 1, 4);
        var larger = new MixedNumber(1, 1, 2);
        var equal = new MixedNumber(1, 2, 4);
        
        Assert.True(smaller.CompareTo(larger) < 0);
        Assert.True(larger.CompareTo(smaller) > 0);
        Assert.Equal(0, larger.CompareTo(equal));
    }

    [Fact]
    public void ImplicitConversion_FromInt_CreatesMixedNumber()
    {
        MixedNumber mixedNumber = 5;
        
        Assert.Equal(5, mixedNumber.Whole);
        Assert.Equal(0, mixedNumber.FractionNumerator);
        Assert.Equal(1, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void ImplicitConversion_FromFraction_CreatesMixedNumber()
    {
        var fraction = new Fraction(7, 4);
        MixedNumber mixedNumber = fraction;
        
        Assert.Equal(1, mixedNumber.Whole);
        Assert.Equal(3, mixedNumber.FractionNumerator);
        Assert.Equal(4, mixedNumber.FractionDenominator);
    }

    [Fact]
    public void ExplicitConversion_ToFraction_ReturnsImproperFraction()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        var fraction = (Fraction)mixedNumber;
        
        Assert.Equal(11, fraction.Numerator);
        Assert.Equal(4, fraction.Denominator);
    }

    [Fact]
    public void ExplicitConversion_ToDouble_ReturnsCorrectValue()
    {
        var mixedNumber = new MixedNumber(2, 1, 4);
        
        var result = (double)mixedNumber;
        
        Assert.Equal(2.25, result);
    }

    [Fact]
    public void ExplicitConversion_ToDecimal_ReturnsCorrectValue()
    {
        var mixedNumber = new MixedNumber(2, 1, 4);
        
        var result = (decimal)mixedNumber;
        
        Assert.Equal(2.25m, result);
    }

    [Fact]
    public void Abs_OfNegativeMixedNumber_ReturnsPositiveMixedNumber()
    {
        var mixedNumber = new MixedNumber(-2, -3, 4);
        
        var result = mixedNumber.Abs();
        
        Assert.Equal(2, result.Whole);
        Assert.Equal(3, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void Abs_OfPositiveMixedNumber_ReturnsSameMixedNumber()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        var result = mixedNumber.Abs();
        
        Assert.Equal(2, result.Whole);
        Assert.Equal(3, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Theory]
    [InlineData("2 3/4", 2, 3, 4)]
    [InlineData("5", 5, 0, 1)]
    [InlineData("3/4", 0, 3, 4)]
    [InlineData("-1 1/2", -1, 1, 2)]
    [InlineData("7/4", 1, 3, 4)]
    public void Parse_WithValidString_ReturnsMixedNumber(string input, int expectedWhole, int expectedNum, int expectedDen)
    {
        var result = MixedNumber.Parse(input);
        
        Assert.Equal(expectedWhole, result.Whole);
        Assert.Equal(expectedNum, result.FractionNumerator);
        Assert.Equal(expectedDen, result.FractionDenominator);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("1 2 3/4")]
    public void Parse_WithInvalidString_ThrowsFormatException(string input)
    {
        Assert.Throws<FormatException>(() => MixedNumber.Parse(input));
    }

    [Fact]
    public void TryParse_WithValidString_ReturnsTrue()
    {
        var success = MixedNumber.TryParse("2 3/4", out var result);
        
        Assert.True(success);
        Assert.Equal(2, result.Whole);
        Assert.Equal(3, result.FractionNumerator);
        Assert.Equal(4, result.FractionDenominator);
    }

    [Fact]
    public void TryParse_WithInvalidString_ReturnsFalse()
    {
        var success = MixedNumber.TryParse("invalid", out var result);
        
        Assert.False(success);
        Assert.Equal(MixedNumber.Zero, result);
    }

    [Theory]
    [InlineData(5, 0, 1, "5")]
    [InlineData(0, 3, 4, "3/4")]
    [InlineData(2, 3, 4, "2 3/4")]
    [InlineData(-2, -3, 4, "-2 3/4")]
    public void ToString_ReturnsCorrectFormat(int whole, int num, int den, string expected)
    {
        var mixedNumber = new MixedNumber(whole, num, den);
        
        Assert.Equal(expected, mixedNumber.ToString());
    }

    [Fact]
    public void GetHashCode_ForEqualMixedNumbers_ReturnsSameValue()
    {
        var mixed1 = new MixedNumber(1, 1, 2);
        var mixed2 = new MixedNumber(1, 2, 4);
        
        Assert.Equal(mixed1.GetHashCode(), mixed2.GetHashCode());
    }

    [Fact]
    public void FractionalPart_ReturnsCorrectFraction()
    {
        var mixedNumber = new MixedNumber(2, 3, 4);
        
        var fractionalPart = mixedNumber.FractionalPart;
        
        Assert.Equal(3, fractionalPart.Numerator);
        Assert.Equal(4, fractionalPart.Denominator);
    }

    [Fact]
    public void StaticProperty_Zero_ReturnsZeroMixedNumber()
    {
        var zero = MixedNumber.Zero;
        
        Assert.Equal(0, zero.Whole);
        Assert.Equal(0, zero.FractionNumerator);
        Assert.Equal(1, zero.FractionDenominator);
    }

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
}
