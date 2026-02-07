using System;

// =======================
// Tuples in C# (ValueTuple)
// =======================
//
// Tuple literal
// (1, "hi") creates a tuple value
//
// Tuple types
// (int, string) is a tuple type
// Tuples are value types (System.ValueTuple<...>)
//
// Named elements
// (int id, string name) gives element names id and name
// If elements are not named, you use Item1, Item2, ...
//
// Returning multiple values
// A method can return (string message, bool is_valid)
// You can read it as a single variable (var result = ...)
// Or you can deconstruct it into separate variables
//
// Deconstruction
// (var a, var b) = SomeMethod()
// You can use discards: (_, var b) = SomeMethod()
//
// Typical mistakes
// Confusing old reference type Tuple<...> with modern ValueTuple syntax
// Deconstructing but then trying to use a variable that does not exist

public static class TupleExamples
{
    public static (string address, bool is_valid) ValidateAddress(string address)
    {
        if (address is null)
            return ("<null>", false);

        string trimmed = address.Trim();

        if (trimmed.Equals("123 sesame st", StringComparison.OrdinalIgnoreCase))
            return ("123 Sesame Street", true);

        if (trimmed.Length < 5)
            return (trimmed, false);

        return (trimmed, false);
    }

    public static (int min, int max) FindMinMax(int[] numbers)
    {
        if (numbers is null || numbers.Length == 0)
            throw new ArgumentException("numbers must contain at least one element");

        int min = numbers[0];
        int max = numbers[0];

        for (int i = 1; i < numbers.Length; i++)
        {
            int x = numbers[i];
            if (x < min) min = x;
            if (x > max) max = x;
        }

        return (min, max);
    }

    public static (string first, string last, bool ok) SplitFullName(string full_name)
    {
        if (string.IsNullOrWhiteSpace(full_name))
            return ("", "", false);

        string trimmed = full_name.Trim();
        int space = trimmed.IndexOf(' ');

        if (space <= 0 || space == trimmed.Length - 1)
            return ("", "", false);

        string first = trimmed.Substring(0, space);
        string last = trimmed.Substring(space + 1).Trim();

        if (last.Length == 0)
            return ("", "", false);

        return (first, last, true);
    }
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Example 1: tuple literal, names, Item1/Item2");

        var point = (x: 10, y: 20);
        Console.WriteLine($"point.x={point.x}, point.y={point.y}");

        (int, string) pair = (7, "seven");
        Console.WriteLine($"pair.Item1={pair.Item1}, pair.Item2={pair.Item2}");

        // Typical mistake: this is not the same as (int, string)
        // Tuple<int, string> old_tuple = Tuple.Create(7, "seven"); // reference type Tuple, different API

        Console.WriteLine();
        Console.WriteLine("Example 2: deconstruction like in the screenshot");

        (string address, bool is_valid) = TupleExamples.ValidateAddress("123 sesame st");
        if (is_valid)
            Console.WriteLine($"Your validated address is {address}");
        else
            Console.WriteLine("That is an invalid address.");

        // Typical mistake: after deconstruction there is no variable named result
        // Console.WriteLine(result.address); // Does not compile: result does not exist

        Console.WriteLine();
        Console.WriteLine("Example 3: var result = ... and dot-access to named elements");

        var result = TupleExamples.FindMinMax(new[] { 5, 2, 9, -1, 4 });
        Console.WriteLine($"min={result.min}, max={result.max}");

        var name_result = TupleExamples.SplitFullName("Denys Koval");
        Console.WriteLine($"ok={name_result.ok}, first={name_result.first}, last={name_result.last}");

        // Discard example: ignore max, keep min only
        (int only_min, _) = TupleExamples.FindMinMax(new[] { 3, 1, 2 });
        Console.WriteLine($"only_min={only_min}");
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples
// https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/tuples
// https://learn.microsoft.com/en-us/dotnet/api/system.valuetuple

