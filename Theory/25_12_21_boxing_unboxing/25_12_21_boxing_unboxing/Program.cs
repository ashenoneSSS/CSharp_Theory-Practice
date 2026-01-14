using System;
using System.Collections.Generic;

namespace _25_09_22_Boxing_Unboxing
{
    // ===========================
    // Boxing / Unboxing (value types <-> reference types)
    // ===========================
    // - Value types: int, double, bool, char, struct, enum (stored as values)
    // - Reference types: class, string, array, object, interfaces (variables store references)
    //
    // Boxing
    // - Boxing = converting a value type to a reference type (object or an interface)
    // - When boxing happens, the runtime allocates an object on the heap and copies the value into it
    // - After boxing, you work with a reference to that boxed copy
    // - Changing the original value type variable does NOT change the boxed copy and vice versa
    //
    // Unboxing
    // - Unboxing = extracting the value type from the boxed object
    // - Requires an explicit cast to the exact value type (or correct underlying type)
    // - If the boxed object does not contain that value type -> InvalidCastException
    //
    // ValueType
    // - All structs inherit from System.ValueType
    // - ValueType itself derives from object
    // - When you call methods like ToString() on a value type, it may box in some scenarios
    //
    // Why boxing exists
    // - object can store ANY type, including value types (via boxing)
    // - interfaces can reference ANY type that implements them, including structs (via boxing)
    //
    // Performance note
    // - Boxing allocates memory and copies values -> can be slow in loops
    // - Avoid boxing in performance-critical code (use generics like List<int>, Dictionary<int, ...>)

    // ===========================
    // User-defined types
    // ===========================
    interface IPoint
    {
        int Sum();
    }

    struct PointStruct : IPoint
    {
        public int X;
        public int Y;

        public PointStruct(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Sum()
        {
            return X + Y;
        }

        public override string ToString()
        {
            return $"PointStruct({X}, {Y})";
        }
    }

    class PointClass : IPoint
    {
        public int X;
        public int Y;

        public PointClass(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int Sum()
        {
            return X + Y;
        }

        public override string ToString()
        {
            return $"PointClass({X}, {Y})";
        }
    }

    static class Demo
    {
        static void PrintTitle(string title)
        {
            Console.WriteLine("=== " + title + " ===");
        }

        // ===========================
        // Example 1: boxing to object
        // ===========================
        public static void BoxingToObject()
        {
            PrintTitle("Boxing to object");

            int a = 10;

            // - Boxing: int -> object
            object boxedA = a;

            Console.WriteLine("a = " + a);
            Console.WriteLine("boxedA (object) = " + boxedA);
            Console.WriteLine("boxedA runtime type = " + boxedA.GetType().Name);

            // - Changing a does not change boxedA (boxed copy)
            a = 99;
            Console.WriteLine("a changed to " + a);
            Console.WriteLine("boxedA still = " + boxedA);

            Console.WriteLine();
        }

        // ===========================
        // Example 2: unboxing from object
        // ===========================
        public static void UnboxingFromObject()
        {
            PrintTitle("Unboxing from object");

            object boxed = 123; // boxed int

            // - Unboxing: object -> int (explicit cast required)
            int x = (int)boxed;

            Console.WriteLine("boxed = " + boxed);
            Console.WriteLine("unboxed int x = " + x);

            Console.WriteLine();
        }

        // ===========================
        // Example 3: wrong unboxing -> exception
        // ===========================
        public static void WrongUnboxing()
        {
            PrintTitle("Wrong unboxing (InvalidCastException)");

            object boxed = 123; // boxed int

            try
            {
                // - ERROR: boxed contains int, not long
                // - Even though int can be converted to long normally, unboxing requires exact type
                long y = (long)boxed;
                Console.WriteLine(y);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("InvalidCastException: " + ex.Message);
            }

            Console.WriteLine();
        }

        // ===========================
        // Example 4: safe unboxing with pattern matching
        // ===========================
        public static void SafeUnboxingWithIs()
        {
            PrintTitle("Safe unboxing with 'is' pattern");

            object boxed = 555;

            // - No exception: checks type first, then unboxes into i
            if (boxed is int i)
            {
                Console.WriteLine("Unboxed int i = " + i);
            }

            object boxed2 = "text";
            if (boxed2 is int j)
            {
                Console.WriteLine(j);
            }
            else
            {
                Console.WriteLine("boxed2 is not int, cannot unbox");
            }

            Console.WriteLine();
        }

        // ===========================
        // Example 5: boxing with interfaces (struct implements interface)
        // ===========================
        public static void BoxingToInterface()
        {
            PrintTitle("Boxing to interface (struct -> interface)");

            PointStruct ps = new PointStruct(3, 4);

            // - Boxing happens here because interface is a reference type
            IPoint ip = ps;

            Console.WriteLine("ps = " + ps);
            Console.WriteLine("ip.Sum() = " + ip.Sum());
            Console.WriteLine("ip runtime type = " + ip.GetType().Name);

            // - Changing the struct variable does NOT change the boxed interface copy
            ps.X = 100;
            ps.Y = 200;

            Console.WriteLine("ps changed to " + ps);
            Console.WriteLine("ip.Sum() still uses boxed copy = " + ip.Sum());

            Console.WriteLine();
        }

        // ===========================
        // Example 6: no boxing with class + interface
        // ===========================
        public static void NoBoxingForClassInterface()
        {
            PrintTitle("No boxing for class -> interface");

            PointClass pc = new PointClass(5, 6);

            // - No boxing, because pc is already a reference type
            IPoint ip = pc;

            Console.WriteLine("pc = " + pc);
            Console.WriteLine("ip.Sum() = " + ip.Sum());

            // - Both references point to the same object
            pc.X = 1000;
            pc.Y = 2000;
            Console.WriteLine("pc changed to " + pc);
            Console.WriteLine("ip.Sum() changes too = " + ip.Sum());

            Console.WriteLine();
        }

        // ===========================
        // Example 7: boxing in non-generic collections
        // ===========================
        public static void NonGenericCollections_Boxing()
        {
            PrintTitle("Boxing in non-generic collections");

            // - ArrayList stores object, so value types are boxed
            // - We use List<object> here to show the same idea without extra namespaces
            List<object> list = new List<object>();

            list.Add(1);      // boxing int
            list.Add(2);      // boxing int
            list.Add("hi");   // no boxing (already reference type)

            Console.WriteLine("list[0] type = " + list[0].GetType().Name);
            Console.WriteLine("list[1] type = " + list[1].GetType().Name);
            Console.WriteLine("list[2] type = " + list[2].GetType().Name);

            // - To use int again, you must unbox
            int a = (int)list[0];
            int b = (int)list[1];
            Console.WriteLine("a + b = " + (a + b));

            Console.WriteLine();
        }

        // ===========================
        // Example 8: avoid boxing with generics
        // ===========================
        public static void Generics_AvoidBoxing()
        {
            PrintTitle("Generics avoid boxing");

            // - List<int> stores ints directly (no boxing)
            List<int> numbers = new List<int>();
            numbers.Add(1);
            numbers.Add(2);
            numbers.Add(3);

            int sum = 0;
            foreach (int n in numbers)
                sum += n;

            Console.WriteLine("Sum = " + sum);
            Console.WriteLine("No boxing here because List<int> stores int values, not object");
            Console.WriteLine();
        }

        // ===========================
        // Example 9: ValueType reference (still boxing)
        // ===========================
        public static void ValueTypeBase_Boxing()
        {
            PrintTitle("System.ValueType reference (boxing)");

            PointStruct ps = new PointStruct(7, 8);

            // - ValueType is a reference type, so assigning a struct to ValueType causes boxing
            ValueType vt = ps;

            Console.WriteLine("ps = " + ps);
            Console.WriteLine("vt.ToString() = " + vt.ToString());
            Console.WriteLine("vt runtime type = " + vt.GetType().Name);

            // - Unboxing back to PointStruct
            PointStruct unboxed = (PointStruct)vt;
            Console.WriteLine("unboxed = " + unboxed);

            Console.WriteLine();
        }

        // ===========================
        // Example 10: boxing in params object[]
        // ===========================
        static void PrintMany(params object[] items)
        {
            // - params object[] means all value type arguments are boxed
            for (int i = 0; i < items.Length; i++)
                Console.WriteLine($"Item {i}: {items[i]} ({items[i].GetType().Name})");
        }

        public static void ParamsBoxing()
        {
            PrintTitle("Boxing in params object[]");

            int a = 10;
            double b = 2.5;
            PointStruct ps = new PointStruct(1, 2);

            // - a is boxed, b is boxed, ps is boxed
            PrintMany(a, b, ps, "text");

            Console.WriteLine();
        }

        // ===========================
        // Example 11: method overload that avoids boxing
        // ===========================
        static void PrintInt(int x)
        {
            // - No boxing because parameter type is int
            Console.WriteLine("PrintInt: " + x);
        }

        static void PrintObject(object x)
        {
            // - If you pass an int here, it will box
            Console.WriteLine("PrintObject: " + x + " (" + x.GetType().Name + ")");
        }

        public static void OverloadBoxingDifference()
        {
            PrintTitle("Overloads: int vs object (boxing difference)");

            int x = 42;

            PrintInt(x);       // no boxing
            PrintObject(x);    // boxing

            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Demo.BoxingToObject();
            Demo.UnboxingFromObject();
            Demo.WrongUnboxing();
            Demo.SafeUnboxingWithIs();

            Demo.BoxingToInterface();
            Demo.NoBoxingForClassInterface();

            Demo.NonGenericCollections_Boxing();
            Demo.Generics_AvoidBoxing();

            Demo.ValueTypeBase_Boxing();

            Demo.ParamsBoxing();
            Demo.OverloadBoxingDifference();

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/boxing-and-unboxing
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
