using System;

namespace _25_09_13_Class_Advanced
{
    // ===========================
    // CLASS WITH NESTED OBJECTS
    // ===========================
    class Address
    {
        public string City;
        public string Street;
    }

    class Person
    {
        public string Name;
        public int Age;
        public Address address;   // field of another class

        public void Print()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
            if (address != null)
                Console.WriteLine($"Lives in {address.City}, {address.Street}");
            else
                Console.WriteLine("No address set");
        }
    }


    // ===========================
    // SELF-REFERENCE (field of same type)
    // ===========================
    class Node
    {
        public int Value;
        public Node Next; // field of same class → allows building linked structures
    }


    // ===========================
    // STATIC FIELDS
    // ===========================
    class Counter
    {
        public string Name;
        public static int TotalCount = 0; // shared by all objects

        public Counter(string name)
        {
            Name = name;
            TotalCount++; // increments global counter
        }

        public void Print()
        {
            Console.WriteLine($"Object {Name}, TotalCount = {TotalCount}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // 1) Fields that are other classes
            // ===========================
            Person p = new Person();
            p.Name = "Alice";
            p.Age = 25;
            p.address = new Address(); // must initialize before using
            p.address.City = "Kyiv";
            p.address.Street = "Main Street 123";

            p.Print();
            Console.WriteLine();


            // ===========================
            // 2) Chain assignment through dots
            // ===========================
            // You can go deeper like: person.address.City = "Kyiv"
            // Works only if "address" is not null
            p.address.City = "Lviv"; // update directly through chain
            p.Print();
            Console.WriteLine();


            // ===========================
            // 3) Self-referencing field example
            // ===========================
            Node n1 = new Node { Value = 1 };
            Node n2 = new Node { Value = 2 };
            Node n3 = new Node { Value = 3 };

            n1.Next = n2;
            n2.Next = n3;

            Console.WriteLine("Linked list values:");
            Console.WriteLine(n1.Value + " → " + n1.Next.Value + " → " + n1.Next.Next.Value);
            Console.WriteLine();


            // ===========================
            // 4) Static field shared across objects
            // ===========================
            Counter c1 = new Counter("first");
            Counter c2 = new Counter("second");
            Counter c3 = new Counter("third");

            // TotalCount is shared → all objects see the same value
            c1.Print();
            c2.Print();
            c3.Print();

            // Can also access directly via class name
            Console.WriteLine("Total created objects = " + Counter.TotalCount);

            Console.ReadLine();
        }
    }
}

/*
Summary:
- Class fields can be other classes → allows composition
- Self-reference fields allow building structures (lists, trees, etc.)
- Dot chaining (a.b.c) works if every step before is not null
- Static field belongs to the class itself, not to any object
  * one value shared across all objects
  * can be accessed via ClassName.FieldName
*/

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
