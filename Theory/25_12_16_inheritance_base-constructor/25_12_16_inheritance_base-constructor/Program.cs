using System;

namespace Inheritance_Mammal_Cat_Kitten_NoVirtual
{
    class Mammal
    {
        protected int ageMonths;

        // - ONLY parameterized ctor (no parameterless ctor exists)
        public Mammal(int ageMonths)
        {
            Console.WriteLine($"Mammal constructor: ageMonths={ageMonths}");
            this.ageMonths = ageMonths;
        }

        // - This method can still be called on the same Kitten object
        public void PrintMammalState()
        {
            Console.WriteLine($"MammalState: ageMonths={ageMonths}");
        }
    }

    class Cat : Mammal
    {
        // - protected so Kitten can technically access it
        protected string fur;

        // - Overload #1 (calls Mammal constructor directly)
        public Cat(int ageMonths, string fur) : base(ageMonths)
        {
            Console.WriteLine($"Cat constructor: fur={fur}");
            // - Good design: Cat should initialize Cat-specific fields itself
            this.fur = fur;
        }

        // - Overload #2 (different base(...) usage, still must call base(ageMonths))
        public Cat(int ageMonths) : base(ageMonths)
        {
            // - This overload chooses a default fur value
            Console.WriteLine("Cat constructor: default fur");
            this.fur = "Unknown";
        }

        public void PrintCatState()
        {
            Console.WriteLine($"CatState: ageMonths={ageMonths}, fur={fur}");
        }
    }

    class Kitten : Cat
    {
        private string nickname;

        // - Overload #1 (full)
        // - Calls base(ageMonths, fur) → Cat(ageMonths, fur) → Mammal(ageMonths)
        public Kitten(int ageMonths, string fur, string nickname) : base(ageMonths, fur)
        {
            Console.WriteLine($"Kitten constructor: nickname={nickname}");
            this.nickname = nickname;
        }

        // - Overload #2 (short)
        // - Calls base(ageMonths) → Cat(ageMonths) → Mammal(ageMonths)
        // - Shows that different Kitten constructors can choose different Cat base constructors
        public Kitten(int ageMonths) : base(ageMonths)
        {
            Console.WriteLine("Kitten constructor: default nickname");
            this.nickname = "NoName";
        }

        // - Overload #3 (another short)
        // - Calls base(ageMonths, fur) but supplies default nickname
        public Kitten(int ageMonths, string fur) : base(ageMonths, fur)
        {
            Console.WriteLine("Kitten constructor: default nickname (with custom fur)");
            this.nickname = "NoName";
        }

        public void PrintKittenState()
        {
            Console.WriteLine($"KittenState: ageMonths={ageMonths}, fur={fur}, nickname={nickname}");
        }

        // - Design nuance:
        // - Kitten CAN modify "fur" because it is protected in Cat
        // - But usually this is worse design because Cat should control its own invariants
        public void BadDesign_ChangeFur(string newFur)
        {
            fur = newFur;
        }

        // - Constructor initializer rule:
        // - a constructor can call either : base(...) OR : this(...)
        // - you cannot do both at the same time, only one initializer is allowed

        /*
        // Example of NOT allowed (won't compile):
        public Kitten(int a) : base(a), this(a, "Soft") { }
        */
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== k1: Kitten(ageMonths, fur, nickname) ===");
            Kitten k1 = new Kitten(4, "Orange", "Milo");
            k1.PrintMammalState();
            k1.PrintCatState();
            k1.PrintKittenState();

            Console.WriteLine("\n=== k2: Kitten(ageMonths) ===");
            Kitten k2 = new Kitten(2);
            k2.PrintMammalState();
            k2.PrintCatState();
            k2.PrintKittenState();

            Console.WriteLine("\n=== k3: Kitten(ageMonths, fur) ===");
            Kitten k3 = new Kitten(3, "Gray");
            k3.PrintMammalState();
            k3.PrintCatState();
            k3.PrintKittenState();

            Console.WriteLine("\n=== protected access (bad design demo) ===");
            k3.BadDesign_ChangeFur("Changed by Kitten");
            k3.PrintKittenState();

            // ===========================
            // Mistake demo (commented out)
            // ===========================

            /*
            // This would NOT compile because Mammal has no parameterless constructor
            class WrongCat : Mammal
            {
                public WrongCat() { }
                // ERROR: "Mammal" does not contain a constructor that takes 0 arguments
            }
            */

            /*
            // Methods cannot exist directly inside a namespace (must be inside a type)
            void Foo() { }
            */

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance
