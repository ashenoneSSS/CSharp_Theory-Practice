using System;

// =======================
// Value types vs reference types vs nullable types
// =======================
//
// Value type
// Examples: int, double, bool, DateTime, structs
// Stored "by value" (the variable contains the data)
// Cannot be null (unless wrapped as Nullable<T> using T?)
// default(T) is always a valid value (example: default(int) is 0)
//
// Reference type
// Examples: string, arrays, class instances, delegates
// The variable holds a reference to an object on the heap
// Can be null at runtime (meaning "no object")
//
// Nullable value type
// Form: T? where T is a non-nullable value type (example: int?)
// Runtime type is Nullable<T> (a struct with HasValue and Value)
// Use HasValue, GetValueOrDefault, ?? to safely handle missing value
// Accessing .Value when null throws InvalidOperationException
//
// Nullable reference type
// Form: string? (or any reference type with ?)
// This is a compiler feature (nullability annotations + warnings)
// Runtime behavior is the same as normal reference types (still can be null)
// Use it to express intent and get warnings when you forget null checks
//
// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types
// https://learn.microsoft.com/en-us/dotnet/csharp/nullable-references

public static class Demo
{
    public static void PrintHeader(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', 55));
        Console.WriteLine(title);
        Console.WriteLine(new string('=', 55));
    }

    // The exact set of calls from your screenshots, wrapped in a method
    public static void NullableIntScreenshotCalls(int? i)
    {
        Console.WriteLine(i);
        Console.WriteLine(i == null);
        Console.WriteLine(i.HasValue);
        Console.WriteLine(i.GetValueOrDefault());
        Console.WriteLine(i.GetValueOrDefault(3));
        Console.WriteLine(i ?? 55);

        try
        {
            Console.WriteLine(i.Value);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.GetType().Name);
        }
    }

    public static void ValueReferenceNullableOverview()
    {
        PrintHeader("Value type vs reference type vs nullable types");

        int value_type_int = 10;
        Console.WriteLine(value_type_int);

        // int value_type_null = null; // Does not compile: int is a non-nullable value type

        string reference_type_string = "Hello";
        Console.WriteLine(reference_type_string);

        // A reference type can be null at runtime
        string? nullable_reference_string = null;
        Console.WriteLine(nullable_reference_string == null);

        // Non-nullable reference type means "should not be null" (compiler checks)
        // string non_nullable_string = null; // Compiles with warning under #nullable enable

        // null! suppresses warnings, but it is still null at runtime
        string non_nullable_suppressed = null!;
        Console.WriteLine(non_nullable_suppressed == null);

        // Nullable value type is a real wrapper: Nullable<int>
        int? nullable_value_int = null;
        Console.WriteLine(nullable_value_int == null);
        Console.WriteLine(nullable_value_int.HasValue);
    }
}

public static class Program
{
    public static void Main()
    {
        Demo.ValueReferenceNullableOverview();

        Demo.PrintHeader("Nullable<int> demo like screenshot: int? i = 2");
        int? i = 2;
        Demo.NullableIntScreenshotCalls(i);
        // Expected output (main idea)
        // i prints 2
        // i == null prints False
        // HasValue prints True
        // GetValueOrDefault prints 2
        // GetValueOrDefault(3) prints 2
        // i ?? 55 prints 2
        // i.Value prints 2

        Demo.PrintHeader("Nullable<int> demo like screenshot: int? i = null");
        i = null;
        Demo.NullableIntScreenshotCalls(i);
        // Expected output (main idea)
        // i prints empty line
        // i == null prints True
        // HasValue prints False
        // GetValueOrDefault prints 0
        // GetValueOrDefault(3) prints 3
        // i ?? 55 prints 55
        // i.Value throws, we print InvalidOperationException
    }
}
