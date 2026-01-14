using System;
using System.Collections.Generic;

namespace _25_09_23_Generics
{
    // ===========================
    // Generics (T, TKey, TValue) basics
    // ===========================
    // - Generics let you write code once and reuse it for many types
    // - Instead of writing BoxInt, BoxString, BoxPerson you write Box<T>
    // - Generics give you
    //   - type safety (compile-time checks)
    //   - no casts for common scenarios
    //   - better performance for value types (often avoids boxing)
    //
    // Where generics appear
    // - generic classes: List<T>, Dictionary<TKey, TValue>
    // - generic methods: Swap<T>(...), Print<T>(...)
    // - generic interfaces: IEnumerable<T>, IComparable<T>
    //
    // Constraints (where T : ...)
    // - Constraints limit what types T can be
    // - This lets you call members safely inside generic code
    // - Examples
    //   - where T : class
    //   - where T : struct
    //   - where T : new()
    //   - where T : SomeBaseClass
    //   - where T : ISomeInterface
    //
    // default(T)
    // - default(T) gives the default value for T
    // - If T is a value type -> 0/false/etc
    // - If T is a reference type -> null

    // ===========================
    // User-defined types for examples
    // ===========================
    class Person
    {
        public string Name { get; }
        public int Age { get; }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"Person(Name={Name}, Age={Age})";
        }
    }

    class Animal
    {
        public string Name { get; }

        public Animal(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Animal(Name={Name})";
        }
    }

    class Dog : Animal
    {
        public string Breed { get; }

        public Dog(string name, string breed) : base(name)
        {
            Breed = breed;
        }

        public override string ToString()
        {
            return $"Dog(Name={Name}, Breed={Breed})";
        }
    }

    // ===========================
    // Example 1: Generic class Box<T>
    // ===========================
    class Box<T>
    {
        public T Value { get; private set; }

        public Box(T value)
        {
            Value = value;
        }

        public void Set(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Box<{typeof(T).Name}>(Value={Value})";
        }
    }

    // ===========================
    // Example 2: Generic class with multiple type parameters
    // ===========================
    class Pair<TFirst, TSecond>
    {
        public TFirst First { get; }
        public TSecond Second { get; }

        public Pair(TFirst first, TSecond second)
        {
            First = first;
            Second = second;
        }

        public override string ToString()
        {
            return $"Pair<{typeof(TFirst).Name}, {typeof(TSecond).Name}>(First={First}, Second={Second})";
        }
    }

    // ===========================
    // Example 3: Generic methods
    // ===========================
    static class GenericMethods
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void PrintArray<T>(T[] items)
        {
            for (int i = 0; i < items.Length; i++)
                Console.Write(items[i] + (i + 1 < items.Length ? ", " : ""));
            Console.WriteLine();
        }

        // - Constraint lets us call CompareTo
        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }

        // - class constraint means "T must be a reference type"
        // - so null checks are valid and meaningful
        public static bool IsNull<T>(T value) where T : class
        {
            return value == null;
        }

        // - struct constraint means "T must be a non-nullable value type"
        public static T MakeDefaultValueType<T>() where T : struct
        {
            return default(T);
        }
    }

    // ===========================
    // Example 4: Constraints with base type + interface + new()
    // ===========================
    interface IEntity
    {
        int Id { get; set; }
    }

    class UserEntity : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }

        // - new() constraint needs a public parameterless constructor
        public UserEntity()
        {
            Id = 0;
            Login = "unknown";
        }

        public override string ToString()
        {
            return $"UserEntity(Id={Id}, Login={Login})";
        }
    }

    class ProductEntity : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ProductEntity()
        {
            Id = 0;
            Title = "none";
        }

        public override string ToString()
        {
            return $"ProductEntity(Id={Id}, Title={Title})";
        }
    }

    class Repository<T> where T : class, IEntity, new()
    {
        private readonly Dictionary<int, T> storage = new Dictionary<int, T>();

        public T CreateDefault(int id)
        {
            // - new() constraint lets us do this safely
            T entity = new T();
            entity.Id = id;
            storage[id] = entity;
            return entity;
        }

        public void Save(T entity)
        {
            storage[entity.Id] = entity;
        }

        public bool TryGet(int id, out T entity)
        {
            return storage.TryGetValue(id, out entity);
        }
    }

    // ===========================
    // Example 5: Generic interface and implementation
    // ===========================
    interface IStorage<T>
    {
        void Add(T item);
        T GetLast();
        int Count { get; }
    }

    class SimpleStack<T> : IStorage<T>
    {
        private readonly List<T> items = new List<T>();

        public int Count
        {
            get { return items.Count; }
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public T GetLast()
        {
            if (items.Count == 0)
                return default(T);

            return items[items.Count - 1];
        }
    }

    // ===========================
    // Example 6: Variance idea (covariance) with IEnumerable<T>
    // ===========================
    // - IEnumerable<out T> is covariant
    // - That means you can use IEnumerable<Dog> where IEnumerable<Animal> is expected
    // - List<T> is NOT covariant, so List<Dog> cannot be assigned to List<Animal>
    static class VarianceDemo
    {
        public static void PrintAnimals(IEnumerable<Animal> animals)
        {
            foreach (Animal a in animals)
                Console.WriteLine(a);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // A) Generic collections
            // ===========================
            List<int> numbers = new List<int> { 1, 2, 3 };
            numbers.Add(4);
            Console.WriteLine("numbers[0] = " + numbers[0]);

            Dictionary<string, int> ages = new Dictionary<string, int>();
            ages["Denys"] = 19;
            ages["Oleh"] = 20;
            Console.WriteLine("ages[Denys] = " + ages["Denys"]);
            Console.WriteLine();

            // ===========================
            // B) Generic class Box<T>
            // ===========================
            Box<int> boxInt = new Box<int>(10);
            Box<string> boxStr = new Box<string>("hello");
            Box<Person> boxPerson = new Box<Person>(new Person("Denys", 19));

            Console.WriteLine(boxInt);
            Console.WriteLine(boxStr);
            Console.WriteLine(boxPerson);
            Console.WriteLine();

            // ===========================
            // C) Generic class Pair<TFirst, TSecond>
            // ===========================
            Pair<string, int> p1 = new Pair<string, int>("age", 19);
            Pair<Person, string> p2 = new Pair<Person, string>(new Person("Oleh", 20), "student");

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine();

            // ===========================
            // D) Generic methods + type inference
            // ===========================
            int a = 5, b = 9;
            GenericMethods.Swap(ref a, ref b); // T is inferred as int
            Console.WriteLine("After Swap<int>: a=" + a + " b=" + b);

            string s1 = "A", s2 = "B";
            GenericMethods.Swap(ref s1, ref s2); // T is inferred as string
            Console.WriteLine("After Swap<string>: s1=" + s1 + " s2=" + s2);

            GenericMethods.PrintArray(new int[] { 10, 20, 30 });
            GenericMethods.PrintArray(new string[] { "x", "y", "z" });
            Console.WriteLine();

            // ===========================
            // E) Constraints: where T : IComparable<T>
            // ===========================
            Console.WriteLine("Max(7, 3) = " + GenericMethods.Max(7, 3));
            Console.WriteLine("Max(\"b\", \"a\") = " + GenericMethods.Max("b", "a"));
            Console.WriteLine();

            // ===========================
            // F) Constraints: class / struct + default(T)
            // ===========================
            Person maybePerson = null;
            Console.WriteLine("IsNull<Person>(null) = " + GenericMethods.IsNull(maybePerson));

            int defaultInt = GenericMethods.MakeDefaultValueType<int>();
            Console.WriteLine("MakeDefaultValueType<int>() = " + defaultInt);

            DateTime defaultDate = GenericMethods.MakeDefaultValueType<DateTime>();
            Console.WriteLine("MakeDefaultValueType<DateTime>() = " + defaultDate);
            Console.WriteLine();

            // ===========================
            // G) Repository<T> with constraints: class, IEntity, new()
            // ===========================
            Repository<UserEntity> users = new Repository<UserEntity>();
            UserEntity u = users.CreateDefault(1);
            u.Login = "denyska";
            users.Save(u);

            if (users.TryGet(1, out UserEntity loadedUser))
                Console.WriteLine("Loaded: " + loadedUser);

            Repository<ProductEntity> products = new Repository<ProductEntity>();
            ProductEntity pr = products.CreateDefault(10);
            pr.Title = "Keyboard";
            products.Save(pr);

            if (products.TryGet(10, out ProductEntity loadedProduct))
                Console.WriteLine("Loaded: " + loadedProduct);

            Console.WriteLine();

            // ===========================
            // H) Generic interface IStorage<T>
            // ===========================
            SimpleStack<string> stack = new SimpleStack<string>();
            stack.Add("first");
            stack.Add("second");
            Console.WriteLine("Stack.Count = " + stack.Count);
            Console.WriteLine("Stack.GetLast() = " + stack.GetLast());

            SimpleStack<int> stackInt = new SimpleStack<int>();
            Console.WriteLine("Empty int stack GetLast() -> default(T) = " + stackInt.GetLast());
            Console.WriteLine();

            // ===========================
            // I) Variance idea: IEnumerable<Dog> -> IEnumerable<Animal>
            // ===========================
            List<Dog> dogs = new List<Dog>();
            dogs.Add(new Dog("Rex", "Husky"));
            dogs.Add(new Dog("Max", "Beagle"));

            // List<Animal> animalsList = dogs; // ERROR: List<T> is invariant
            IEnumerable<Animal> animalsEnumerable = dogs; // OK: IEnumerable<out T> is covariant

            VarianceDemo.PrintAnimals(animalsEnumerable);

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
