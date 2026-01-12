using System;

namespace _25_09_19_Const_vs_ReadOnly
{
    // ===========================
    // const vs readonly
    // ===========================
    // - const is a compile-time constant
    // - const value is baked into compiled code where it is used
    // - const must be assigned immediately at declaration
    // - const is always static implicitly
    // - const works only with values known at compile time (numbers, char, bool, string, enum, null)
    //
    // - readonly is a runtime constant per object (or per class if static readonly)
    // - readonly can be assigned at declaration OR inside a constructor
    // - readonly is not baked the same way as const, it is evaluated at runtime
    // - readonly is great when the value is only known when creating the object (or when program starts)
    //
    // Main idea:
    // - Use const for true constants that never change and are compile-time known
    // - Use readonly for values that must not change after construction but are not compile-time constants

    // ===========================
    // const examples
    // ===========================
    internal static class TimeConstants
    {
        // - const must be assigned at declaration
        // - implicitly static
        public const int SecondsInMinute = 60;
        public const int MinutesInHour = 60;
        public const int HoursInDay = 24;
    }

    internal static class MathConstants
    {
        public const double PiApprox = 3.141592653589793;
        public const string DefaultCurrency = "UAH";
    }

    // ===========================
    // readonly per object example
    // ===========================
    internal class User
    {
        // - readonly can be set in constructor or here
        public readonly int Id;

        // - get-only property also behaves like "readonly after construction"
        public string Name { get; }

        private static int nextId = 1;

        public User(string name)
        {
            Id = nextId;
            nextId++;
            Name = name;
        }
    }

    // ===========================
    // static readonly example
    // ===========================
    internal static class AppInfo
    {
        // - static readonly can be set at declaration with runtime values
        // - evaluated when the type is initialized
        public static readonly DateTime StartedAtUtc = DateTime.UtcNow;

        // - Environment.MachineName is runtime, so it cannot be const
        public static readonly string MachineName = Environment.MachineName;
    }

    // ===========================
    // readonly reference nuance example
    // ===========================
    internal class Basket
    {
        public readonly string Code;
        public readonly System.Collections.Generic.List<string> Items;

        public Basket(string code)
        {
            Code = code;
            Items = new System.Collections.Generic.List<string>();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // Example 1: const basic use
            // ===========================
            Console.WriteLine("Example 1: const basics");
            Console.WriteLine("Seconds in minute: " + TimeConstants.SecondsInMinute);
            Console.WriteLine("Pi approx: " + MathConstants.PiApprox);
            Console.WriteLine();

            // ===========================
            // Example 2: readonly per object
            // ===========================
            Console.WriteLine("Example 2: readonly per object");
            User u1 = new User("Denys");
            User u2 = new User("Oleh");

            Console.WriteLine("u1.Id: " + u1.Id);
            Console.WriteLine("u2.Id: " + u2.Id);
            Console.WriteLine("u1.Name: " + u1.Name);
            Console.WriteLine("u2.Name: " + u2.Name);
            Console.WriteLine();

            // ===========================
            // Example 3: static readonly (computed once at runtime)
            // ===========================
            Console.WriteLine("Example 3: static readonly runtime value");
            Console.WriteLine("App started at: " + AppInfo.StartedAtUtc);
            Console.WriteLine("Machine name: " + AppInfo.MachineName);
            Console.WriteLine();

            // ===========================
            // Example 4: readonly reference vs object mutability
            // ===========================
            Console.WriteLine("Example 4: readonly reference nuance");
            Basket b = new Basket("B-001");
            Console.WriteLine("Basket code: " + b.Code);

            // b.Code = "B-002"; // ERROR: readonly field cannot be assigned after constructor

            // - readonly blocks reassigning the reference, but does not freeze the object
            b.Items.Add("Milk");
            b.Items.Add("Bread");
            Console.WriteLine("Items count: " + b.Items.Count);
            Console.WriteLine();

            // ===========================
            // Example 5: why const can be dangerous across projects
            // ===========================
            Console.WriteLine("Example 5: const versioning idea");
            Console.WriteLine("If a const value changes in a library, other assemblies may still use the old baked value");
            Console.WriteLine("For shared constants across assemblies, static readonly is often safer");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
