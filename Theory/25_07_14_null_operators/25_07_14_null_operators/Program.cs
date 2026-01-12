using System;
using System.Collections.Generic;

namespace _25_09_20_Null_Operators
{
    // ===========================
    // null, ??, ?., ??=
    // ===========================
    // - null means "no object reference" or "no value" (for nullable types)
    // - reference types (string, class, array) can be null
    // - value types (int, double, struct) cannot be null unless you use nullable form: int?, double?, MyStruct?
    //
    // - NullReferenceException happens when you try to access members on a null reference
    //   example: text.Length when text is null
    //
    // - ??  (null-coalescing operator)
    //   returns left value if it is not null, otherwise returns right value
    //
    // - ?.  (null-conditional operator)
    //   safely accesses a member only if the left side is not null
    //   if left side is null, the whole expression becomes null (no exception)
    //
    // - ??= (null-coalescing assignment)
    //   assigns the right value only if the left variable is null

    // ===========================
    // User-defined classes
    // ===========================
    class Address
    {
        public string City { get; set; }

        public Address(string city)
        {
            City = city;
        }

        public void Print()
        {
            Console.WriteLine("Address.City = " + City);
        }
    }

    class Person
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        // - This list starts as null to demonstrate ??=
        public List<string> Notes { get; private set; }

        public Person(string name, Address address)
        {
            Name = name;
            Address = address;
            Notes = null;
        }

        public void AddNote(string note)
        {
            // - Create the list only when it's first needed
            Notes ??= new List<string>();
            Notes.Add(note);
        }
    }

    internal class Program
    {
        // - Method example that combines ?. and ??
        static string GetCityOrDefault(Person person)
        {
            // - If person is null -> person?.Address becomes null -> City becomes null -> return "No city"
            // - If Address is null -> Address?.City is null -> return "No city"
            // - If City is not null -> returns City
            return person?.Address?.City ?? "No city";
        }

        static void Main(string[] args)
        {
            // ===========================
            // 1) null basics
            // ===========================
            string text = null;

            // Console.WriteLine(text.Length); // NullReferenceException (because text is null)

            Console.WriteLine(text == null); // true
            Console.WriteLine();

            // ===========================
            // 2) ?? (null-coalescing)
            // ===========================
            string safeText = text ?? "Default text";
            Console.WriteLine("safeText = " + safeText);

            int? maybeNumber = null;
            int number = maybeNumber ?? -1;
            Console.WriteLine("number = " + number);
            Console.WriteLine();

            // ===========================
            // 3) ?. (null-conditional)
            // ===========================
            // - text?.Length becomes null if text is null
            // - because Length is int, the result becomes int? (nullable int)
            int? lenNullable = text?.Length;
            Console.WriteLine("lenNullable is null: " + (lenNullable == null));

            // - Use ?? to convert int? to int with a default
            int lenSafe = text?.Length ?? 0;
            Console.WriteLine("lenSafe = " + lenSafe);
            Console.WriteLine();

            // ===========================
            // 4) ?. with OOP objects and chaining
            // ===========================
            Person p1 = null;
            Console.WriteLine("City from p1: " + GetCityOrDefault(p1));

            Person p2 = new Person("Denys", null);
            Console.WriteLine("City from p2: " + GetCityOrDefault(p2));

            Person p3 = new Person("Oleh", new Address("Kyiv"));
            Console.WriteLine("City from p3: " + GetCityOrDefault(p3));
            Console.WriteLine();

            // - Calling method safely
            // - If Address is null, Address?.Print() does nothing
            p2.Address?.Print();
            p3.Address?.Print();
            Console.WriteLine();

            // ===========================
            // 5) ??= (null-coalescing assignment)
            // ===========================
            // - Assign only if null
            string nickname = null;
            nickname ??= "Guest";
            Console.WriteLine("nickname = " + nickname);

            // - Lazy initialization of an object
            Address shippingAddress = null;
            shippingAddress ??= new Address("Lviv");
            Console.WriteLine("shippingAddress.City = " + shippingAddress.City);
            Console.WriteLine();

            // - Lazy initialization inside a class method (Notes ??= new List<string>())
            p3.AddNote("First note");
            p3.AddNote("Second note");
            Console.WriteLine("Notes count = " + (p3.Notes?.Count ?? 0));
            Console.WriteLine();

            // ===========================
            // Important nuance about ?.
            // ===========================
            // - ?. only protects you from null on the left side
            // - it does NOT protect from other exceptions
            List<int> list = new List<int>();
            // int x = list?[0] ?? 0; // list is not null, but index 0 does not exist -> exception
            Console.WriteLine("?. does not protect from invalid indexes");

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/member-access-operators#null-conditional-operators--and-
