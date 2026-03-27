using System;
using System.Collections.Generic;
using System.Linq;

// =======================
// LINQ methods quick conspect (one file, sequential in Main)
// =======================
//
// What LINQ is
// LINQ is a set of extension methods for querying sequences
// Most LINQ methods work on IEnumerable<T>
//
// Deferred execution (lazy)
// Many LINQ methods do not execute immediately
// The query runs when you enumerate it (foreach) or materialize it (ToList, ToArray)
//
// Immediate execution (eager)
// Methods like Count, Any, First, Sum execute immediately because they must produce a single value
//
// Important idea
// If you enumerate a lazy query multiple times, the logic runs multiple times
// If the source changes between enumerations, results can change too
//
// In this file
// Each LINQ method has 2 examples
// Each example uses its own separate list
// After the first example of each method there is an empty Console.WriteLine() for spacing

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("1) Where");
        // What it does
        // Filters a sequence by a condition
        // Keeps elements where predicate(element) is true
        // Preserves the original order of elements
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Signature idea
        // Takes: IEnumerable<T> source, Func<T,bool> predicate
        // Returns: IEnumerable<T> filtered sequence
        {
            var nums = new List<int> { 10, 11, 12, 13, 14 };
            var evens = nums.Where(x => x % 2 == 0).ToList();
            Console.WriteLine(string.Join(", ", evens));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { -5, 0, 1, 8, -2 };
            var positives = nums.Where(x => x > 0).ToList();
            Console.WriteLine(string.Join(", ", positives));
        }

        Console.WriteLine("\n2) Select");
        // What it does
        // Transforms each element into a new element
        // One input element produces one output element
        // Output sequence length equals input sequence length
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Signature idea
        // Takes: IEnumerable<TSource> source, Func<TSource,TResult> selector
        // Returns: IEnumerable<TResult>
        {
            var nums = new List<int> { 2, 3, 4 };
            var squares = nums.Select(x => x * x).ToList();
            Console.WriteLine(string.Join(", ", squares));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 7, 15 };
            var texts = nums.Select(x => $"value={x}").ToList();
            Console.WriteLine(string.Join(" | ", texts));
        }

        Console.WriteLine("\n3) OrderBy");
        // What it does
        // Sorts elements by a key
        // Does not modify the original list
        // Returns a new ordered sequence
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Signature idea
        // Takes: IEnumerable<TSource> source, Func<TSource,TKey> keySelector
        // Returns: IOrderedEnumerable<TSource>
        {
            var nums = new List<int> { 9, 1, 5, 3 };
            var sorted = nums.OrderBy(x => x).ToList();
            Console.WriteLine(string.Join(", ", sorted));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 8, 1, 5, 6, 12 };
            var sorted = nums.OrderBy(x => Math.Abs(x - 5)).ToList();
            Console.WriteLine(string.Join(", ", sorted));
        }

        Console.WriteLine("\n4) ThenBy");
        // What it does
        // Adds a second (or third, etc) sorting rule after OrderBy
        // First sorts by the first key, then sorts ties by the next key
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Signature idea
        // Takes: IOrderedEnumerable<TSource> + Func<TSource,TKey>
        // Returns: IOrderedEnumerable<TSource>
        {
            var words = new List<string> { "ccc", "a", "bb", "b", "aa" };
            var sorted = words.OrderBy(s => s.Length).ThenBy(s => s).ToList();
            Console.WriteLine(string.Join(", ", sorted));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 7, 2, 5, 4, 1, 6 };
            var sorted = nums.OrderBy(x => x % 2).ThenBy(x => x).ToList();
            Console.WriteLine(string.Join(", ", sorted));
        }

        Console.WriteLine("\n5) First / FirstOrDefault");
        // What it does
        // First returns the first element (optionally matching a predicate)
        // FirstOrDefault returns the first element or default(T) if none found
        //
        // When it runs
        // Immediate execution (must choose a single element)
        //
        // Typical behavior
        // First throws InvalidOperationException if sequence is empty or no match
        // FirstOrDefault does not throw in that case
        {
            var nums = new List<int> { 100, 200, 300 };
            int x = nums.First();
            Console.WriteLine(x);
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 1, 3, 5, 7 };
            int first_even_or_default = nums.FirstOrDefault(n => n % 2 == 0);
            Console.WriteLine(first_even_or_default); // 0 because int default is 0
        }

        Console.WriteLine("\n6) Single / SingleOrDefault");
        // What it does
        // Single expects exactly one element (optionally matching a predicate)
        // SingleOrDefault expects zero or one element
        //
        // When it runs
        // Immediate execution
        //
        // Typical behavior
        // Single throws if there are 0 matches or 2+ matches
        // SingleOrDefault throws if there are 2+ matches
        {
            var nums = new List<int> { 42 };
            int x = nums.Single();
            Console.WriteLine(x);
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 1, 2, 3, 5 };
            int even = nums.Single(n => n % 2 == 0);
            Console.WriteLine(even);
        }

        Console.WriteLine("\n7) Any");
        // What it does
        // Checks if there is at least one element
        // Or at least one element matching a predicate
        //
        // When it runs
        // Immediate execution
        //
        // Performance idea
        // Stops early as soon as it finds a match
        {
            var nums = new List<int> { 9 };
            bool has_any = nums.Any();
            Console.WriteLine(has_any);
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 1, 3, 5 };
            bool has_even = nums.Any(x => x % 2 == 0);
            Console.WriteLine(has_even);
        }

        Console.WriteLine("\n8) All");
        // What it does
        // Checks if all elements satisfy a predicate
        //
        // When it runs
        // Immediate execution
        //
        // Performance idea
        // Stops early as soon as it finds a counterexample (predicate false)
        {
            var nums = new List<int> { 2, 4, 6 };
            bool all_even = nums.All(x => x % 2 == 0);
            Console.WriteLine(all_even);
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 2, 4, 5 };
            bool all_even = nums.All(x => x % 2 == 0);
            Console.WriteLine(all_even);
        }

        Console.WriteLine("\n9) Count");
        // What it does
        // Counts elements
        // Or counts elements matching a predicate
        //
        // When it runs
        // Immediate execution
        //
        // Note
        // On List<T> Count() is O(1) because it uses the list Count property
        // On general IEnumerable<T> it may enumerate (O(n))
        {
            var nums = new List<int> { 1, 2, 3, 4 };
            int c = nums.Count();
            Console.WriteLine(c);
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 10, 11, 12, 13, 14, 15 };
            int c = nums.Count(x => x % 3 == 0);
            Console.WriteLine(c);
        }

        Console.WriteLine("\n10) Distinct");
        // What it does
        // Removes duplicates
        // Keeps the first occurrence of each value
        // Preserves first-seen order
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Note
        // Uses equality comparer (default or custom)
        {
            var nums = new List<int> { 1, 1, 2, 2, 2, 3, 1 };
            var unique = nums.Distinct().ToList();
            Console.WriteLine(string.Join(", ", unique));
            Console.WriteLine();
        }
        {
            var words = new List<string> { "Cat", "cat", "DOG", "dog", "bird" };
            var unique = words.Distinct(StringComparer.OrdinalIgnoreCase).ToList();
            Console.WriteLine(string.Join(", ", unique));
        }

        Console.WriteLine("\n11) Skip / Take");
        // What it does
        // Skip(n) ignores first n elements
        // Take(n) returns first n elements
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Typical use
        // Pagination: Skip(page * size).Take(size)
        {
            var nums = new List<int> { 5, 6, 7, 8, 9 };
            var r = nums.Skip(2).ToList();
            Console.WriteLine(string.Join(", ", r));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 100, 200, 300, 400, 500 };
            var r = nums.Skip(1).Take(2).ToList();
            Console.WriteLine(string.Join(", ", r));
        }

        Console.WriteLine("\n12) GroupBy");
        // What it does
        // Groups elements by a key
        // Produces IGrouping<TKey,TElement> objects
        // Each grouping has a Key and a sequence of items
        //
        // When it runs
        // Deferred execution until enumeration
        //
        // Typical use
        // Categorizing data: by length, by status, by type
        {
            var words = new List<string> { "a", "bb", "ccc", "dd", "eee" };
            var groups = words.GroupBy(s => s.Length);

            foreach (var g in groups)
                Console.WriteLine($"{g.Key}: {string.Join(", ", g)}");

            Console.WriteLine();
        }
        {
            var nums = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            var groups = nums.GroupBy(x => x % 2 == 0 ? "even" : "odd");

            foreach (var g in groups)
                Console.WriteLine($"{g.Key}: {string.Join(", ", g)}");
        }

        Console.WriteLine("\n13) ToList / ToArray");
        // What it does
        // Forces execution and materializes results
        // After ToList/ToArray you get a real collection stored in memory
        //
        // When it runs
        // Immediate execution (materialization is immediate)
        //
        // Typical use
        // You want to store results and reuse them without re-running the query
        {
            var nums = new List<int> { 1, 2, 3, 4, 5 };
            var filtered = nums.Where(x => x > 3).ToList();
            Console.WriteLine(string.Join(", ", filtered));
            Console.WriteLine();
        }
        {
            var nums = new List<int> { 2, 4, 6 };
            var arr = nums.Select(x => x * 10).ToArray();
            Console.WriteLine(string.Join(", ", arr));
        }

        Console.WriteLine("\n14) Sum / Min / Max / Average");
        // What it does
        // Sum adds up numbers
        // Min and Max find extremes
        // Average returns the arithmetic mean (double)
        //
        // When it runs
        // Immediate execution
        //
        // Notes
        // Overloads exist for selector functions: Sum(x => ...)
        {
            var nums = new List<int> { 3, 6, 9 };
            Console.WriteLine(nums.Sum());
            Console.WriteLine(nums.Min());
            Console.WriteLine(nums.Max());
            Console.WriteLine(nums.Average());
            Console.WriteLine();
        }
        {
            var words = new List<string> { "aa", "bbbb", "c" };
            int total_len = words.Sum(s => s.Length);
            Console.WriteLine(total_len);
        }

        Console.WriteLine("\n15) SelectMany");
        // What it does
        // Flattens a sequence of sequences into a single sequence
        // Think: List<List<int>> -> List<int>
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Typical use
        // Flatten nested collections, split strings into characters, merge child collections
        {
            var lists = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4, 5 }
            };

            var flat = lists.SelectMany(x => x).ToList();
            Console.WriteLine(string.Join(", ", flat));
            Console.WriteLine();
        }
        {
            var words = new List<string> { "ab", "XYZ" };
            var chars = words.SelectMany(s => s).ToList();
            Console.WriteLine(string.Join(", ", chars));
        }

        Console.WriteLine("\n16) Join");
        // What it does
        // Joins two sequences by matching keys (like an inner join)
        // Produces results only for matching key pairs
        //
        // When it runs
        // Deferred execution until enumeration or ToList/ToArray
        //
        // Signature idea
        // Join(inner, outerKeySelector, innerKeySelector, resultSelector)
        {
            var a = new List<int> { 1, 2, 3, 4 };
            var b = new List<int> { 2, 4, 6 };

            var common = a.Join(b, x => x, y => y, (x, y) => x).ToList();
            Console.WriteLine(string.Join(", ", common));
            Console.WriteLine();
        }
        {
            var users = new List<(int id, string name)>
            {
                (1, "Ann"),
                (2, "Bob"),
                (3, "Cory")
            };

            var orders = new List<(int user_id, int total)>
            {
                (2, 10),
                (2, 25),
                (3, 7)
            };

            var r = users.Join(
                orders,
                u => u.id,
                o => o.user_id,
                (u, o) => $"{u.name}:{o.total}"
            ).ToList();

            Console.WriteLine(string.Join(" | ", r));
        }

        Console.WriteLine("\n17) Chunk");
        // What it does
        // Splits a sequence into chunks (arrays) of size N
        // Last chunk may be smaller
        //
        // When it runs
        // Deferred execution until enumeration
        //
        // Note
        // Chunk is available in .NET 6+
        {
            var nums = new List<int> { 1, 2, 3, 4, 5 };
            foreach (var chunk in nums.Chunk(2))
                Console.WriteLine($"[{string.Join(", ", chunk)}]");

            Console.WriteLine();
        }
        {
            var nums = new List<int> { 10, 20, 30, 40, 50, 60, 70 };
            foreach (var chunk in nums.Chunk(3))
                Console.WriteLine($"[{string.Join(", ", chunk)}]");
        }
    }
}

// Microsoft Learn
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.where
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.select
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.thenby
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.first
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.single
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.any
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.all
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.count
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinct
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skip
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.groupby
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tolist
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.toarray
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.sum
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.selectmany
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.join
// https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk