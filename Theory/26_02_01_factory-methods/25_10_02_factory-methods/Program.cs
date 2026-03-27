using System;
using System.Globalization;

// =======================
// Factory methods (easy examples)
// =======================
//
// A factory method is a method that creates and returns an instance
// You call "UserName.Create(...)" instead of "new UserName(...)"
//
// Why it is useful
// Validation before an instance exists
// Clear names like FromFahrenheit instead of confusing overloads
// Central place to choose an implementation (factory returns an interface)
//
// Common shapes
// Create(...) throws on invalid input
// TryCreate(...) returns bool and avoids exceptions
// FromX(...) makes intent explicit
//
// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/constructor
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes

public sealed class UserName
{
    public string Value { get; }

    // Private constructor forces callers to use factory methods
    private UserName(string value) => Value = value;

    // Create is a factory method that throws if input is invalid
    public static UserName Create(string raw)
    {
        if (raw is null) throw new ArgumentNullException(nameof(raw));

        string v = raw.Trim();
        if (v.Length < 3) throw new ArgumentException("Username must be at least 3 characters");
        if (v.Contains(' ')) throw new ArgumentException("Username must not contain spaces");

        return new UserName(v);
    }

    // TryCreate is a factory method that avoids exceptions
    public static bool TryCreate(string raw, out UserName user_name)
    {
        user_name = null!;

        if (raw is null) return false;

        string v = raw.Trim();
        if (v.Length < 3) return false;
        if (v.Contains(' ')) return false;

        user_name = new UserName(v);
        return true;
    }

    public override string ToString() => Value;
}

public readonly struct Temperature
{
    public double Celsius { get; }

    private Temperature(double celsius) => Celsius = celsius;

    // Named factories make units explicit
    public static Temperature FromCelsius(double value) => new Temperature(value);
    public static Temperature FromFahrenheit(double value) => new Temperature((value - 32.0) * 5.0 / 9.0);

    public double ToFahrenheit() => Celsius * 9.0 / 5.0 + 32.0;

    public override string ToString() => $"{Celsius:0.##} °C";
}

public interface IShape
{
    double Area();
}

public sealed class Circle : IShape
{
    private readonly double _radius;
    public Circle(double radius) => _radius = radius;
    public double Area() => Math.PI * _radius * _radius;
    public override string ToString() => $"Circle(r={_radius:0.##})";
}

public sealed class Square : IShape
{
    private readonly double _side;
    public Square(double side) => _side = side;
    public double Area() => _side * _side;
    public override string ToString() => $"Square(a={_side:0.##})";
}

public static class ShapeFactory
{
    // Factory returns an interface, hiding concrete types from callers
    public static IShape Create(string kind, double size)
    {
        if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be > 0");
        if (kind is null) throw new ArgumentNullException(nameof(kind));

        return kind.Trim().ToLowerInvariant() switch
        {
            "circle" => new Circle(size),
            "square" => new Square(size),
            _ => throw new ArgumentException("Unknown shape kind, use: circle or square")
        };
    }

    // Example input: "circle 2.5" or "square 3"
    public static bool TryCreateFromText(string text, out IShape shape, out string error)
    {
        shape = null!;
        error = string.Empty;

        if (text is null) { error = "Text is null"; return false; }

        string[] parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) { error = "Expected: <kind> <size>"; return false; }

        string kind = parts[0];

        if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double size))
        {
            error = "Size must be a number (use '.' as decimal separator)";
            return false;
        }

        try
        {
            shape = Create(kind, size);
            return true;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            return false;
        }
    }
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("1) UserName factories");

        UserName u1 = UserName.Create("Denys");
        Console.WriteLine($"Create ok: {u1}");

        bool ok = UserName.TryCreate("ab", out UserName u2);
        Console.WriteLine(ok ? $"TryCreate ok: {u2}" : "TryCreate failed for 'ab'");

        // Typical mistake: calling a private constructor directly
        // UserName u3 = new UserName("Denys"); // Does not compile: constructor is private

        // Typical mistake: ignoring the bool result
        // UserName.TryCreate("ab", out UserName u4);
        // Console.WriteLine(u4.Value); // u4 is null when TryCreate returns false

        Console.WriteLine();
        Console.WriteLine("2) Temperature named factories");

        Temperature t1 = Temperature.FromCelsius(25);
        Temperature t2 = Temperature.FromFahrenheit(77);
        Console.WriteLine($"t1 = {t1} => {t1.ToFahrenheit():0.##} °F");
        Console.WriteLine($"t2 = {t2} => {t2.ToFahrenheit():0.##} °F");

        Console.WriteLine();
        Console.WriteLine("3) Shape factory returning an interface");

        IShape s1 = ShapeFactory.Create("circle", 2);
        IShape s2 = ShapeFactory.Create("square", 3);
        Console.WriteLine($"{s1}, area={s1.Area():0.###}");
        Console.WriteLine($"{s2}, area={s2.Area():0.###}");

        bool parsed_ok = ShapeFactory.TryCreateFromText("circle 2.5", out IShape s3, out string err1);
        Console.WriteLine(parsed_ok ? $"{s3}, area={s3.Area():0.###}" : $"Parse error: {err1}");

        bool parsed_bad = ShapeFactory.TryCreateFromText("triangle 10", out IShape s4, out string err2);
        Console.WriteLine(parsed_bad ? $"{s4}, area={s4.Area():0.###}" : $"Parse error: {err2}");

        // Typical mistake: designing a factory that returns null
        // If Create could return null, every call site would need null-checks
    }
}
