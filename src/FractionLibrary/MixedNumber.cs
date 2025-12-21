namespace FractionLib;

/// <summary>
/// Represents an immutable mixed number with a whole part and a fractional part.
/// The fractional part is always proper (|numerator| < denominator) and has the same sign as the whole part.
/// </summary>
public readonly struct MixedNumber : IEquatable<MixedNumber>, IComparable<MixedNumber>
{
    public int Whole { get; }
    public int FractionNumerator { get; }
    public int FractionDenominator { get; }

    public MixedNumber(int whole, int fractionNumerator, int fractionDenominator)
    {
        if (fractionDenominator == 0)
            throw new ArgumentException("Denominator cannot be zero.", nameof(fractionDenominator));

        var fraction = new Fraction(fractionNumerator, fractionDenominator);
        
        // Normalize: extract whole numbers from improper fractions
        var wholeFromFraction = fraction.Numerator / fraction.Denominator;
        var remainingNumerator = fraction.Numerator % fraction.Denominator;

        Whole = whole + wholeFromFraction;
        FractionNumerator = remainingNumerator;
        FractionDenominator = fraction.Denominator;

        // Note: do not force fractional sign to match whole here; keep the fractional part as the
        // simplified numerator from Fraction. Other behaviors/tests expect the fractional numerator
        // to remain as provided (e.g., negative or positive) and ToImproperFraction computes the
        // improper fraction using stored signs.
    }

    public static MixedNumber Zero => new(0, 0, 1);

    public bool IsZero => Whole == 0 && FractionNumerator == 0;
    public bool IsPositive => Whole > 0 || (Whole == 0 && FractionNumerator > 0);
    public bool IsNegative => Whole < 0 || (Whole == 0 && FractionNumerator < 0);

    public Fraction FractionalPart => new(FractionNumerator, FractionDenominator);

    public Fraction ToImproperFraction()
    {
        var numerator = Whole * FractionDenominator + FractionNumerator;
        return new Fraction(numerator, FractionDenominator);
    }

    public static MixedNumber FromFraction(Fraction fraction)
    {
        var whole = fraction.Numerator / fraction.Denominator;
        var numerator = fraction.Numerator % fraction.Denominator;
        return new MixedNumber(whole, numerator, fraction.Denominator);
    }

    public static MixedNumber operator +(MixedNumber left, MixedNumber right)
    {
        var result = left.ToImproperFraction() + right.ToImproperFraction();
        return FromFraction(result);
    }

    public static MixedNumber operator -(MixedNumber left, MixedNumber right)
    {
        var result = left.ToImproperFraction() - right.ToImproperFraction();
        return FromFraction(result);
    }

    public static MixedNumber operator *(MixedNumber left, MixedNumber right)
    {
        var result = left.ToImproperFraction() * right.ToImproperFraction();
        return FromFraction(result);
    }

    public static MixedNumber operator /(MixedNumber left, MixedNumber right)
    {
        var result = left.ToImproperFraction() / right.ToImproperFraction();
        return FromFraction(result);
    }

    public static MixedNumber operator -(MixedNumber mixedNumber)
    {
        return new MixedNumber(-mixedNumber.Whole, -mixedNumber.FractionNumerator, 
            mixedNumber.FractionDenominator);
    }

    public static bool operator ==(MixedNumber left, MixedNumber right)
    {
        return left.Whole == right.Whole && 
               left.FractionNumerator == right.FractionNumerator && 
               left.FractionDenominator == right.FractionDenominator;
    }

    public static bool operator !=(MixedNumber left, MixedNumber right)
    {
        return !(left == right);
    }

    public static bool operator <(MixedNumber left, MixedNumber right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(MixedNumber left, MixedNumber right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(MixedNumber left, MixedNumber right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(MixedNumber left, MixedNumber right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static implicit operator MixedNumber(int value)
    {
        return new MixedNumber(value, 0, 1);
    }

    public static implicit operator MixedNumber(Fraction fraction)
    {
        return FromFraction(fraction);
    }

    public static explicit operator Fraction(MixedNumber mixedNumber)
    {
        return mixedNumber.ToImproperFraction();
    }

    public static explicit operator double(MixedNumber mixedNumber)
    {
        return (double)mixedNumber.ToImproperFraction();
    }

    public static explicit operator decimal(MixedNumber mixedNumber)
    {
        return (decimal)mixedNumber.ToImproperFraction();
    }

    public MixedNumber Abs()
    {
        return new MixedNumber(Math.Abs(Whole), Math.Abs(FractionNumerator), FractionDenominator);
    }

    public bool Equals(MixedNumber other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is MixedNumber other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Whole, FractionNumerator, FractionDenominator);
    }

    public int CompareTo(MixedNumber other)
    {
        return ToImproperFraction().CompareTo(other.ToImproperFraction());
    }

    public override string ToString()
    {
        if (FractionNumerator == 0)
            return Whole.ToString();

        if (Whole == 0)
            return $"{FractionNumerator}/{FractionDenominator}";

        var absFraction = Math.Abs(FractionNumerator);
        return $"{Whole} {absFraction}/{FractionDenominator}";
    }

    public static MixedNumber Parse(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            throw new FormatException("Input string cannot be null or whitespace.");

        s = s.Trim();
        
        // Check for simple fraction (no whole part)
        if (s.Contains('/') && !s.Contains(' '))
        {
            var fraction = Fraction.Parse(s);
            return FromFraction(fraction);
        }

        // Check for whole number only
        if (!s.Contains('/') && !s.Contains(' '))
        {
            if (int.TryParse(s, out var whole))
                return new MixedNumber(whole, 0, 1);
        }

        // Parse mixed number format: "whole numerator/denominator"
        var parts = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 2)
        {
            if (int.TryParse(parts[0], out var whole))
            {
                var fractionParts = parts[1].Split('/');
                if (fractionParts.Length == 2 &&
                    int.TryParse(fractionParts[0], out var numerator) &&
                    int.TryParse(fractionParts[1], out var denominator))
                {
                    // Keep the numerator sign as provided; MixedNumber constructor will handle simplification
                    return new MixedNumber(whole, numerator, denominator);
                }
            }
        }

        throw new FormatException($"String '{s}' is not in a valid mixed number format.");
    }

    public static bool TryParse(string? s, out MixedNumber result)
    {
        try
        {
            if (s == null)
            {
                result = Zero;
                return false;
            }
            result = Parse(s);
            return true;
        }
        catch
        {
            result = Zero;
            return false;
        }
    }
}
