using FractionLib;

Console.WriteLine("=== FractionLib Demo ===\n");

// Fraction Examples
Console.WriteLine("--- Fraction Examples ---");

var oneHalf = new Fraction(1, 2);
var oneThird = new Fraction(1, 3);
var twoThirds = new Fraction(2, 3);

Console.WriteLine($"1/2 = {oneHalf}");
Console.WriteLine($"1/3 = {oneThird}");
Console.WriteLine($"2/3 = {twoThirds}");
Console.WriteLine();

// Arithmetic
Console.WriteLine("Arithmetic Operations:");
Console.WriteLine($"1/2 + 1/3 = {oneHalf + oneThird}");
Console.WriteLine($"2/3 - 1/2 = {twoThirds - oneHalf}");
Console.WriteLine($"1/2 * 2/3 = {oneHalf * twoThirds}");
Console.WriteLine($"2/3 / 1/2 = {twoThirds / oneHalf}");
Console.WriteLine();

// Automatic simplification
Console.WriteLine("Automatic Simplification:");
var unsimplified = new Fraction(6, 8);
Console.WriteLine($"6/8 simplifies to {unsimplified}");
Console.WriteLine();

// Comparisons
Console.WriteLine("Comparisons:");
Console.WriteLine($"1/2 < 2/3? {oneHalf < twoThirds}");
Console.WriteLine($"1/2 == 2/4? {oneHalf == new Fraction(2, 4)}");
Console.WriteLine($"2/3 > 1/3? {twoThirds > oneThird}");
Console.WriteLine();

// Conversions
Console.WriteLine("Conversions:");
Console.WriteLine($"1/2 as double: {(double)oneHalf}");
Console.WriteLine($"2/3 as decimal: {(decimal)twoThirds:F4}");
Fraction fromInt = 5;
Console.WriteLine($"Integer 5 as fraction: {fromInt}");
Console.WriteLine();

// Parsing
Console.WriteLine("Parsing:");
var parsed1 = Fraction.Parse("3/4");
var parsed2 = Fraction.Parse("7");
Console.WriteLine($"Parsed '3/4': {parsed1}");
Console.WriteLine($"Parsed '7': {parsed2}");
Console.WriteLine();

// Mixed Number Examples
Console.WriteLine("\n--- Mixed Number Examples ---");

var twoAndHalf = new MixedNumber(2, 1, 2);
var oneAndQuarter = new MixedNumber(1, 1, 4);
var threeQuarters = new MixedNumber(0, 3, 4);

Console.WriteLine($"2 1/2 = {twoAndHalf}");
Console.WriteLine($"1 1/4 = {oneAndQuarter}");
Console.WriteLine($"3/4 = {threeQuarters}");
Console.WriteLine();

// Mixed number arithmetic
Console.WriteLine("Mixed Number Arithmetic:");
Console.WriteLine($"2 1/2 + 1 1/4 = {twoAndHalf + oneAndQuarter}");
Console.WriteLine($"2 1/2 - 1 1/4 = {twoAndHalf - oneAndQuarter}");
Console.WriteLine($"2 1/2 * 1 1/4 = {twoAndHalf * oneAndQuarter}");
Console.WriteLine($"2 1/2 / 1 1/4 = {twoAndHalf / oneAndQuarter}");
Console.WriteLine();

// Conversion between types
Console.WriteLine("Conversions:");
var improper = twoAndHalf.ToImproperFraction();
Console.WriteLine($"2 1/2 as improper fraction: {improper}");
var backToMixed = MixedNumber.FromFraction(improper);
Console.WriteLine($"5/2 as mixed number: {backToMixed}");
Console.WriteLine();

// Parsing mixed numbers
Console.WriteLine("Parsing Mixed Numbers:");
var parsedMixed1 = MixedNumber.Parse("3 1/4");
var parsedMixed2 = MixedNumber.Parse("7/4");
var parsedMixed3 = MixedNumber.Parse("5");
Console.WriteLine($"Parsed '3 1/4': {parsedMixed1}");
Console.WriteLine($"Parsed '7/4': {parsedMixed2}");
Console.WriteLine($"Parsed '5': {parsedMixed3}");
Console.WriteLine();

// Complex calculation example
Console.WriteLine("Complex Calculation:");
Console.WriteLine("Recipe scaling example:");
Console.WriteLine("Original recipe calls for 2 1/2 cups flour");
Console.WriteLine("We want to make 1 1/2 times the recipe");

var originalAmount = new MixedNumber(2, 1, 2);
var scaleFactor = new MixedNumber(1, 1, 2);
var scaledAmount = originalAmount * scaleFactor;

Console.WriteLine($"{originalAmount} × {scaleFactor} = {scaledAmount} cups flour needed");
Console.WriteLine();

// Comparison example
Console.WriteLine("Comparison Example:");
var measurements = new[] {
    new MixedNumber(1, 1, 4),
    new MixedNumber(2, 1, 2),
    new MixedNumber(1, 3, 4),
    new MixedNumber(2, 0, 1)
};

Console.WriteLine("Unsorted measurements:");
foreach (var m in measurements)
    Console.WriteLine($"  {m}");

Array.Sort(measurements);

Console.WriteLine("\nSorted measurements:");
foreach (var m in measurements)
    Console.WriteLine($"  {m}");

Console.WriteLine("\n=== Demo Complete ===");
