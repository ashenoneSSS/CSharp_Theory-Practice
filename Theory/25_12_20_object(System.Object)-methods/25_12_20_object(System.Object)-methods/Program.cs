using System;
using System.Collections.Generic;

namespace _25_09_21_Object_Basics
{
    // ===========================
    // System.Object (object) basics
    // ===========================
    // - object is the C# keyword alias for System.Object
    // - Object is the .NET class name, object is the C# keyword, they are the same type
    // - Every type in C# ultimately implicitly derives from System.Object
    //   - class types derive from Object automatically
    //   - struct types derive from ValueType, and ValueType derives from Object
    //
    // The 4 core System.Object methods:
    //
    // 1) ToString()
    //    - Returns: string
    //    - Purpose: human-readable text representation of the object
    //    - Common use: logging/debugging/output (Console.WriteLine(obj) calls ToString())
    //    - Default: often the type name unless a type overrides it
    //
    // 2) GetType()
    //    - Returns: Type
    //    - Purpose: returns the real runtime type information of the object
    //    - Common use: diagnostics/debugging/reflection, especially when a variable is typed as object/base type
    //
    // 3) Equals(object other)
    //    - Returns: bool
    //    - Purpose: checks whether two objects are considered equal
    //    - Common use: value/content comparison (e.g., string compares characters)
    //    - Default: for many reference types, compares references unless overridden
    //
    // 4) GetHashCode()
    //    - Returns: int
    //    - Purpose: hash number used by hash-based collections (Dictionary/HashSet) for fast lookup
    //    - Rule: if a.Equals(b) is true, then a.GetHashCode() must equal b.GetHashCode()
    //
    // Extra method
    // 5) ReferenceEquals(object a, object b)
    //    - Static method on System.Object
    //    - Returns: bool
    //    - Purpose: checks reference identity (do both variables point to the exact same object in memory?)
    //    - Not affected by overriding Equals (it ignores "value equality" logic)
    //    - For value types, it usually returns false because values get boxed into separate objects

    // ===========================
    // User-defined classes and helpers
    // ===========================
    class PersonDefault
    {
        public string Name;
        public int Age;

        public PersonDefault(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // - No overrides here
        // - Equals will behave like reference equality (same object) by default
        // - ToString will print the type name by default
    }

    class PersonValue
    {
        public string Name { get; }
        public int Age { get; }

        public PersonValue(string name, int age)
        {
            Name = name;
            Age = age;
        }

        // - ToString is virtual in Object, so we can override it
        public override string ToString()
        {
            return $"PersonValue(Name={Name}, Age={Age})";
        }

        // - Equals is virtual in Object, so we can override it for value equality
        public override bool Equals(object obj)
        {
            if (obj is PersonValue other)
                return Name == other.Name && Age == other.Age;

            return false;
        }

        // - If two objects are Equal, they must return the same hash code
        // - Hash codes can collide, but Equal objects must match hash codes
        public override int GetHashCode()
        {
            int hash = 17;

            // - Using simple combination for demo
            // - null-safe for Name
            hash = hash * 31 + (Name == null ? 0 : Name.GetHashCode());
            hash = hash * 31 + Age.GetHashCode();

            return hash;
        }
    }

    class Animal
    {
        public string Nickname;

        public Animal(string nickname)
        {
            Nickname = nickname;
        }

        // - ToString default would be type name, but we make it readable
        public override string ToString()
        {
            return $"Animal(Nickname={Nickname})";
        }
    }

    class Dog : Animal
    {
        public string Breed;

        public Dog(string nickname, string breed) : base(nickname)
        {
            Breed = breed;
        }

        public override string ToString()
        {
            return $"Dog(Nickname={Nickname}, Breed={Breed})";
        }
    }

    static class Demo
    {
        static void PrintTitle(string title)
        {
            Console.WriteLine("=== " + title + " ===");
        }

        public static void Run_ObjectAliases()
        {
            PrintTitle("object keyword vs Object class name");

            object a = "Hello";
            Object b = a;
            System.Object c = b;

            Console.WriteLine(a.GetType());
            Console.WriteLine(b.GetType());
            Console.WriteLine(c.GetType());

            Console.WriteLine();
        }

        public static void Run_ToStringExamples()
        {
            PrintTitle("ToString examples");

            int x = 123;
            Console.WriteLine("int.ToString -> " + x.ToString());

            PersonDefault p1 = new PersonDefault("Denys", 19);
            Console.WriteLine("PersonDefault.ToString default -> " + p1.ToString());

            PersonValue p2 = new PersonValue("Denys", 19);
            Console.WriteLine("PersonValue.ToString override -> " + p2.ToString());

            Animal a = new Animal("Buddy");
            Dog d = new Dog("Rex", "Husky");
            Console.WriteLine("Animal.ToString override -> " + a.ToString());
            Console.WriteLine("Dog.ToString override -> " + d.ToString());

            Console.WriteLine();
        }

        public static void Run_GetTypeExamples()
        {
            PrintTitle("GetType examples");

            // - GetType is non-virtual
            // - It always returns the runtime type, not the variable type
            Animal a1 = new Animal("Buddy");
            Animal a2 = new Dog("Rex", "Husky");

            Console.WriteLine("a1 variable type: Animal, runtime type -> " + a1.GetType().Name);
            Console.WriteLine("a2 variable type: Animal, runtime type -> " + a2.GetType().Name);

            // - object variable can store anything
            object o1 = 10;
            object o2 = "text";
            object o3 = new PersonValue("Oleh", 20);

            Console.WriteLine("o1 runtime type -> " + o1.GetType().Name);
            Console.WriteLine("o2 runtime type -> " + o2.GetType().Name);
            Console.WriteLine("o3 runtime type -> " + o3.GetType().Name);

            Console.WriteLine();
        }

        public static void Run_EqualsExamples()
        {
            PrintTitle("Equals examples");

            // - For strings, Equals checks value (content)
            string s1 = "hi";
            string s2 = "hi";
            Console.WriteLine("string Equals -> " + s1.Equals(s2));

            // - For PersonDefault (no override), Equals checks reference by default
            PersonDefault r1 = new PersonDefault("Denys", 19);
            PersonDefault r2 = new PersonDefault("Denys", 19);
            PersonDefault r3 = r1;

            Console.WriteLine("PersonDefault r1.Equals(r2) -> " + r1.Equals(r2));
            Console.WriteLine("PersonDefault r1.Equals(r3) -> " + r1.Equals(r3));

            // - For PersonValue (override), Equals checks values
            PersonValue v1 = new PersonValue("Denys", 19);
            PersonValue v2 = new PersonValue("Denys", 19);
            PersonValue v3 = new PersonValue("Denys", 20);
             
            Console.WriteLine("PersonValue v1.Equals(v2) -> " + v1.Equals(v2));
            Console.WriteLine("PersonValue v1.Equals(v3) -> " + v1.Equals(v3));

            // - object.Equals(a, b) handles nulls safely
            PersonValue nv = null;
            Console.WriteLine("object.Equals(null, null) -> " + object.Equals(null, null));
            Console.WriteLine("object.Equals(nv, v1) -> " + object.Equals(nv, v1));
            Console.WriteLine("object.Equals(v1, v2) -> " + object.Equals(v1, v2));

            Console.WriteLine();
        }

        public static void Run_GetHashCodeExamples()
        {
            PrintTitle("GetHashCode examples");

            // - If Equals says two objects are equal, their hash codes should match
            PersonValue v1 = new PersonValue("Denys", 19);
            PersonValue v2 = new PersonValue("Denys", 19);
            PersonValue v3 = new PersonValue("Denys", 20);

            Console.WriteLine("v1.GetHashCode -> " + v1.GetHashCode());
            Console.WriteLine("v2.GetHashCode -> " + v2.GetHashCode());
            Console.WriteLine("v3.GetHashCode -> " + v3.GetHashCode());

            Console.WriteLine("v1.Equals(v2) -> " + v1.Equals(v2));
            Console.WriteLine("v1 hash == v2 hash -> " + (v1.GetHashCode() == v2.GetHashCode()));
            Console.WriteLine("v1.Equals(v3) -> " + v1.Equals(v3));

            Console.WriteLine();

            // - HashSet and Dictionary rely on Equals + GetHashCode
            HashSet<PersonValue> set = new HashSet<PersonValue>();
            set.Add(new PersonValue("Denys", 19));
            set.Add(new PersonValue("Denys", 19));
            set.Add(new PersonValue("Denys", 20));

            Console.WriteLine("HashSet count (value equality) -> " + set.Count);

            Dictionary<PersonValue, string> dict = new Dictionary<PersonValue, string>();
            dict[new PersonValue("Denys", 19)] = "first";
            dict[new PersonValue("Denys", 19)] = "overwritten by equal key";
            dict[new PersonValue("Denys", 20)] = "second";

            Console.WriteLine("Dictionary count -> " + dict.Count);
            Console.WriteLine("Dictionary[Denys,19] -> " + dict[new PersonValue("Denys", 19)]);

            Console.WriteLine();
        }

        public static void Run_ReferenceEqualsExamples()
        {
            PrintTitle("ReferenceEquals examples");

            // - ReferenceEquals checks identity (same object)
            PersonDefault r1 = new PersonDefault("Denys", 19);
            PersonDefault r2 = new PersonDefault("Denys", 19);
            PersonDefault r3 = r1;

            Console.WriteLine("ReferenceEquals(r1, r2) -> " + object.ReferenceEquals(r1, r2));
            Console.WriteLine("ReferenceEquals(r1, r3) -> " + object.ReferenceEquals(r1, r3));

            // - Even if Equals is true, ReferenceEquals can be false
            PersonValue v1 = new PersonValue("Denys", 19);
            PersonValue v2 = new PersonValue("Denys", 19);

            Console.WriteLine("v1.Equals(v2) -> " + v1.Equals(v2));
            Console.WriteLine("ReferenceEquals(v1, v2) -> " + object.ReferenceEquals(v1, v2));

            Console.WriteLine();

            // - Strings: Equals compares content, ReferenceEquals compares same interned object
            // - String literals are usually interned, so two identical literals may be the same reference
            string s1 = "hello";
            string s2 = "hello";
            Console.WriteLine("s1.Equals(s2) -> " + s1.Equals(s2));
            Console.WriteLine("ReferenceEquals(literals) -> " + object.ReferenceEquals(s1, s2));

            // - Creating a new string object makes a different reference
            string s3 = new string(new char[] { 'h', 'e', 'l', 'l', 'o' });
            Console.WriteLine("s1.Equals(s3) -> " + s1.Equals(s3));
            Console.WriteLine("ReferenceEquals(s1, s3) -> " + object.ReferenceEquals(s1, s3));

            // - You can intern a string to force reference sharing
            string s4 = string.Intern(s3);
            Console.WriteLine("ReferenceEquals(s1, s4) -> " + object.ReferenceEquals(s1, s4));

            Console.WriteLine();

            // - Value types and boxing
            // - Each boxing creates a separate object wrapper
            object a = 5;
            object b = 5;

            Console.WriteLine("a.Equals(b) -> " + a.Equals(b));
            Console.WriteLine("ReferenceEquals(a, b) -> " + object.ReferenceEquals(a, b));

            Console.WriteLine();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Demo.Run_ObjectAliases();
            Demo.Run_ToStringExamples();
            Demo.Run_GetTypeExamples();
            Demo.Run_EqualsExamples();
            Demo.Run_GetHashCodeExamples();
            Demo.Run_ReferenceEqualsExamples();

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types#the-object-type
// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object.equals
// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object.gethashcode
// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object.gettype
// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object.tostring
// Docs: https://learn.microsoft.com/en-us/dotnet/api/system.object.referenceequals
