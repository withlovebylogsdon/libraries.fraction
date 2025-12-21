namespace FractionLib;

/// <summary>
/// Represents an immutable fraction with a numerator and denominator.
/// Always stored in simplified form with positive denominator.
/// </summary>
public readonly struct Fraction : IEquatable<Fraction>, IComparable<Fraction>
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));

        var gcd = GreatestCommonDivisor(Math.Abs(numerator), Math.Abs(denominator));
        var sign = Math.Sign(numerator) * Math.Sign(denominator);

        Numerator = sign * Math.Abs(numerator) / gcd;
        Denominator = Math.Abs(denominator) / gcd;
    }

    public static Fraction Zero => new(0, 1);
    public static Fraction One => new(1, 1);

    public bool IsZero => Numerator == 0;
    public bool IsPositive => Numerator > 0;
    public bool IsNegative => Numerator < 0;

    private static int GreatestCommonDivisor(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private static int LeastCommonMultiple(int a, int b)
    {
        return Math.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    public static Fraction operator +(Fraction left, Fraction right)
    {
        var lcm = LeastCommonMultiple(left.Denominator, right.Denominator);
        var numerator = left.Numerator * (lcm / left.Denominator) + 
                       right.Numerator * (lcm / right.Denominator);
        return new Fraction(numerator, lcm);
    }

    public static Fraction operator -(Fraction left, Fraction right)
    {
        var lcm = LeastCommonMultiple(left.Denominator, right.Denominator);
        var numerator = left.Numerator * (lcm / left.Denominator) - 
                       right.Numerator * (lcm / right.Denominator);
        return new Fraction(numerator, lcm);
    }

    public static Fraction operator *(Fraction left, Fraction right)
    {
        return new Fraction(
            left.Numerator * right.Numerator,
            left.Denominator * right.Denominator);
    }

    public static Fraction operator /(Fraction left, Fraction right)
    {
        if (right.IsZero)
            throw new DivideByZeroException("Cannot divide by zero fraction.");

        return new Fraction(
            left.Numerator * right.Denominator,
            left.Denominator * right.Numerator);
    }

    public static Fraction operator -(Fraction fraction)
    {
        return new Fraction(-fraction.Numerator, fraction.Denominator);
    }

    public static bool operator ==(Fraction left, Fraction right)
    {
        return left.Numerator == right.Numerator && left.Denominator == right.Denominator;
    }

    public static bool operator !=(Fraction left, Fraction right)
    {
        return !(left == right);
    }

    public static bool operator <(Fraction left, Fraction right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Fraction left, Fraction right)
    {
        return left.CompareTo(right) > 0;
    }

    public static bool operator <=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) <= 0;
    }

    public static bool operator >=(Fraction left, Fraction right)
    {
        return left.CompareTo(right) >= 0;
    }

    public static implicit operator Fraction(int value)
    {
        return new Fraction(value, 1);
    }

    public static explicit operator double(Fraction fraction)
    {
        return (double)fraction.Numerator / fraction.Denominator;
    }

    public static explicit operator decimal(Fraction fraction)
    {
        return (decimal)fraction.Numerator / fraction.Denominator;
    }

    public Fraction Reciprocal()
    {
        if (IsZero)
            throw new InvalidOperationException("Cannot get reciprocal of zero.");

        return new Fraction(Denominator, Numerator);
    }

    public Fraction Abs()
    {
        return new Fraction(Math.Abs(Numerator), Denominator);
    }

    public bool Equals(Fraction other)
    {
        return this == other;
    }

    public override bool Equals(object? obj)
    {
        return obj is Fraction other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    public int CompareTo(Fraction other)
    {
        var leftValue = (long)Numerator * other.Denominator;
        var rightValue = (long)other.Numerator * Denominator;
        return leftValue.CompareTo(rightValue);
    }

    public override string ToString()
    {
        if (Denominator == 1)
            return Numerator.ToString();
        return $"{Numerator}/{Denominator}";
    }

    public static Fraction Parse(string s)
    {
        if (s is null)
            throw new ArgumentNullException(nameof(s));

        // Consider empty or whitespace-only input invalid format for parsing
        if (string.IsNullOrWhiteSpace(s))
            throw new FormatException("Input string cannot be null or whitespace.");

        var parts = s.Split('/');
        
        if (parts.Length == 1)
        {
            if (int.TryParse(parts[0].Trim(), out var whole))
                return new Fraction(whole, 1);
        }
        else if (parts.Length == 2)
        {
            if (int.TryParse(parts[0].Trim(), out var numerator) && 
                int.TryParse(parts[1].Trim(), out var denominator))
            {
                return new Fraction(numerator, denominator);
            }
        }

        throw new FormatException($"String '{s}' is not in a valid fraction format.");
    }

    public static bool TryParse(string? s, out Fraction result)
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
