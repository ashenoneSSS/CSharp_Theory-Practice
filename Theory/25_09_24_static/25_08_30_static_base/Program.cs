using System;

namespace _25_08_30_static_base
{
    internal class Program
    {
        // =============================
        // Instance field (non-static)
        // =============================
        // Belongs to each object separately
        // Each instance has its own copy
        public int instanceCounter = 0;

        // =============================
        // Static field
        // =============================
        // Belongs to the class itself, not to any object
        // Shared by all instances
        public static int totalObjects = 0;

        // =============================
        // Another static field to show static constructor work
        // =============================
        // Initialized once for the whole class
        public static DateTime classLoadedAt;

        // =============================
        // Static constructor
        // =============================
        // - Runs automatically ONCE per type (per program run)
        // - Runs BEFORE the first use of the class:
        //   * before first object is created (new Program())
        //   * or before first static member is used (Program.totalObjects / Program.ShowTotal)
        // - Cannot have parameters
        // - Cannot be called manually
        // - Use it to initialize static fields
        static Program()
        {
            classLoadedAt = DateTime.Now;
            Console.WriteLine("Static constructor: Program class initialized");
            Console.WriteLine("Static constructor: classLoadedAt = " + classLoadedAt.ToString("HH:mm:ss"));
        }

        // =============================
        // Instance method (non-static)
        // =============================
        // Belongs to an object (instance of the class)
        // You must create an object before calling it
        bool IsPrimeInstance(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        // =============================
        // Static method
        // =============================
        // Belongs to the class itself, not to any object
        // You call it using ClassName.MethodName without creating an object
        static bool IsPrimeStatic(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i < n; i++)
                if (n % i == 0) return false;
            return true;
        }

        // =============================
        // Instance constructor
        // =============================
        // Every time an object is created,
        // the instance counter increases for that object,
        // and the static totalObjects increases for all
        public Program()
        {
            instanceCounter++;
            totalObjects++;
        }

        // =============================
        // Instance method using both fields
        // =============================
        public void ShowCounters()
        {
            Console.WriteLine($"Instance Counter: {instanceCounter}, Total Objects: {totalObjects}");
        }

        // =============================
        // Static method accessing static field
        // =============================
        public static void ShowTotal()
        {
            Console.WriteLine($"Total created objects (static): {totalObjects}");
            Console.WriteLine("Class loaded at: " + classLoadedAt.ToString("HH:mm:ss"));
        }

        static void Main(string[] args)
        {
            // - The static constructor will run before the first usage of Program
            // - Depending on what happens first, it runs before:
            //   * creating the first object
            //   * or accessing a static member

            // --- Using instance method ---
            Program obj = new Program(); // first object creation triggers static constructor (if not run yet)
            for (int i = 2; i < 10; i++)
            {
                if (obj.IsPrimeInstance(i))
                    Console.WriteLine("Instance prime: " + i);
            }

            Console.WriteLine();

            // --- Using static method ---
            for (int i = 2; i < 10; i++)
            {
                if (IsPrimeStatic(i))
                    Console.WriteLine("Static prime: " + i);
            }

            Console.WriteLine();

            // --- Example: static and instance fields ---
            Program p1 = new Program();
            Program p2 = new Program();
            Program p3 = new Program();

            p1.ShowCounters(); // instanceCounter = 1, totalObjects = 4
            p2.ShowCounters(); // instanceCounter = 1, totalObjects = 4
            p3.ShowCounters(); // instanceCounter = 1, totalObjects = 4

            // Accessing static field directly through class name
            Console.WriteLine("Access via class: " + Program.totalObjects);

            // Calling static method through class name (no object needed)
            Program.ShowTotal();

            Console.ReadLine();
        }
    }
}

/*
Summary:
- Instance method (non-static)
  * requires an object
  * can access both instance and static fields
  * belongs to an object, not to the class

- Static method
  * does not require an object
  * can access only static fields
  * belongs to the class itself
  * called via ClassName.MethodName()

- Instance field
  * every object has its own copy
  * changes in one object do not affect others

- Static field
  * belongs to the class, shared by all objects
  * all objects see the same value
  * can be accessed using ClassName.FieldName

- Static constructor
  * runs once automatically before the first use of the class
  * used to initialize static fields
  * cannot have parameters and cannot be called manually

Similarly, in other methods as well, when you are accessing fields, you are basically accessing
the fields of the objects which call those methods, and not of the class itself,
because a class normally does not contain any data.

However, there is a way to store data into a class directly and make a property accessible
without needing to create an object. You can do that by simply making that field or method static.
*/

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
