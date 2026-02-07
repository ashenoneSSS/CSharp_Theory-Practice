using System;
using System.Collections;
using System.Collections.Generic;

// =======================
// IEnumerable and IEnumerator
// =======================
//
// IEnumerable<T>
// Has GetEnumerator()
// Foreach calls GetEnumerator() to get an enumerator
//
// IEnumerator<T>
// Has Current
// Has MoveNext() that advances and returns true while there are items
// Implements IDisposable so foreach can Dispose() it in a finally block
//
// Foreach expansion (conceptually)
// var e = sequence.GetEnumerator()
// try
//   while (e.MoveNext())
//     var item = e.Current
// finally
//   e.Dispose()


// =======================
// yield return and yield break
// =======================
//
// An iterator block is a method with yield return / yield break
// The compiler generates a hidden state machine that implements IEnumerable<T> and IEnumerator<T>
//
// What happens on each MoveNext()
// The state machine runs until it hits yield return or finishes
// yield return X sets Current = X and returns true
// yield break (or reaching the end) returns false and stops enumeration immediately


// Key difference vs returning a List
// 
// yield return is lazy (deferred execution)
// The method does NOT generate all items at once
// It produces items one-by-one only when someone starts iterating (foreach / MoveNext)
// Each MoveNext() continues the method from the last yield return
// If iteration stops early (break/return), the rest of the method is never executed
//
// Returning a List is eager (immediate execution)
// The method builds the entire list right away when the method is called
// All items are created and stored in memory before you can use the result
// Even if you later take only a few items, the whole list was already built

public static class Iterators
{
    public static IEnumerable<int> yield_even(List<int> list)
    {
        foreach (int x in list)
            if (x % 2 == 0)
                yield return x;
    }

    public static IEnumerable<int> yield_with_logging(List<int> list)
    {
        Console.WriteLine("iterator: started");
        foreach (int x in list)
        {
            Console.WriteLine($"iterator: checking {x}");
            if (x % 2 == 0)
            {
                Console.WriteLine($"iterator: yielding {x}");
                yield return x;
            }
        }
        Console.WriteLine("iterator: finished");
    }

    public static IEnumerable<int> yield_first_n(List<int> list, int n)
    {
        if (n <= 0)
            yield break;

        int taken = 0;
        foreach (int x in list)
        {
            yield return x;
            taken++;
            if (taken == n)
                yield break;
        }
    }

    public static IEnumerable<int> yield_until_value(List<int> list, int stop_value)
    {
        foreach (int x in list)
        {
            if (x == stop_value)
                yield break;
            yield return x;
        }
    }

    public static IEnumerable<int> yield_squares(int count)
    {
        for (int i = 1; i <= count; i++)
            yield return i * i;
    }

    public static IEnumerable<int> yield_squares_from(IEnumerable<int> source)
    {
        foreach (int x in source)
            yield return x * x;
    }

    public static List<int> eager_even_list(List<int> list)
    {
        Console.WriteLine("list: building now");
        var result = new List<int>();
        foreach (int x in list)
            if (x % 2 == 0)
                result.Add(x);
        Console.WriteLine("list: built");
        return result;
    }

    public static void manual_foreach_over(IEnumerable<int> seq)
    {
        IEnumerator<int> e = seq.GetEnumerator();
        try
        {
            while (e.MoveNext())
            {
                int x = e.Current;
                Console.WriteLine(x);
            }
        }
        finally
        {
            e.Dispose();
        }
    }

    // Typical mistakes
    // public static IEnumerable<int> bad_current_before_movenext(IEnumerable<int> seq)
    // {
    //     var e = seq.GetEnumerator();
    //     Console.WriteLine(e.Current); // Often throws or is undefined
    //     return seq;
    // }

    // public static IEnumerable<int> bad_yield_in_finally()
    // {
    //     try { }
    //     finally { yield return 1; } // Does not compile
    // }
}

public static class Program
{
    public static void Main()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6 };

        Console.WriteLine("1) Simple yield_even over a ready List");
        foreach (int x in Iterators.yield_even(list))
            Console.WriteLine(x);

        Console.WriteLine();
        Console.WriteLine("2) Same foreach but expanded manually (IEnumerator + MoveNext + Current)");
        Console.WriteLine("Foreach version:");
        foreach (int x in Iterators.yield_even(list))
            Console.WriteLine(x);

        Console.WriteLine("Manual version:");
        Iterators.manual_foreach_over(Iterators.yield_even(list));

        Console.WriteLine();
        Console.WriteLine("3) How yield runs only during enumeration (logging inside iterator)");
        IEnumerable<int> seq = Iterators.yield_with_logging(list);
        Console.WriteLine("Created IEnumerable, no iteration yet");

        foreach (int x in seq)
            Console.WriteLine($"consumer: got {x}");

        Console.WriteLine();
        Console.WriteLine("4) yield and modifying the same List during iteration");
        Console.WriteLine("The iterator reads the list while foreach is running");
        Console.WriteLine("Modifying the list during enumeration usually throws InvalidOperationException");

        try
        {
            foreach (int x in Iterators.yield_with_logging(list))
            {
                Console.WriteLine($"consumer: got {x}");
                if (x == 2)
                    list.Add(100); // typical bug: changes collection during enumeration
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.GetType().Name);
        }

        Console.WriteLine();
        Console.WriteLine("Reset list back for the next examples");
        list = new List<int> { 1, 2, 3, 4, 5, 6 };

        Console.WriteLine();
        Console.WriteLine("5) yield break examples");
        Console.WriteLine("First 3 elements:");
        foreach (int x in Iterators.yield_first_n(list, 3))
            Console.WriteLine(x); // 1 2 3

        Console.WriteLine("Until value 4 (4 not included):");
        foreach (int x in Iterators.yield_until_value(list, 4))
            Console.WriteLine(x); // 1 2 3

        Console.WriteLine();
        Console.WriteLine("6) yield generating values without a List (squares)");
        foreach (int x in Iterators.yield_squares(5))
            Console.WriteLine(x); // 1 4 9 16 25

        Console.WriteLine();
        Console.WriteLine("7) yield piping: squares from another sequence");
        foreach (int x in Iterators.yield_squares_from(Iterators.yield_even(list)))
            Console.WriteLine(x); // 4 16 36

        Console.WriteLine();
        Console.WriteLine("8) Contrast: eager list builds immediately");
        List<int> eager = Iterators.eager_even_list(list);
        Console.WriteLine("After eager list is built, foreach is just reading results");
        foreach (int x in eager)
            Console.WriteLine(x);
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iterator
// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/yield
// https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1
