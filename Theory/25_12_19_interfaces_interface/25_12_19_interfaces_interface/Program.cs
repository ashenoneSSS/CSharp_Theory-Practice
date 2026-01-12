using System;
using System.Collections.Generic;

namespace _25_09_18_Interfaces_ExplicitImplementation
{
    // ===========================
    // INTERFACES (OOP concept)
    // ===========================
    // - An interface defines a contract: what members a type must have
    // - Interface usually describes capabilities/roles, not a shared "thing"
    // - A class can implement multiple interfaces (multiple inheritance of behavior contracts)
    // - Interfaces are great when you do NOT need shared state or shared base implementation
    //
    // Explicit interface implementation:
    // - Member is implemented with "InterfaceName.MemberName"
    // - It is NOT accessible through the class type variable, only through an interface variable/cast
    // - Useful when
    //   - you want to hide interface members from normal class API
    //   - you have name conflicts between interfaces
    //   - you want different behavior depending on which interface view is used

    interface IPrintable
    {
        void Print();
    }

    interface IPayable
    {
        decimal GetPaymentAmount();
    }

    interface IShippable
    {
        decimal GetShippingCost();
        string GetShippingLabel();
    }

    // - Interface inheritance (interface -> interface)
    interface IOrder : IPrintable, IPayable
    {
        int Id { get; }
    }

    // ===========================
    // Unrelated classes implementing interfaces
    // ===========================
    class Student : IPrintable
    {
        public string Name { get; private set; }
        public int Course { get; private set; }

        public Student(string name, int course)
        {
            Name = name;
            Course = course;
        }

        public void Print()
        {
            Console.WriteLine($"Student: {Name}, Course: {Course}");
        }
    }

    // ===========================
    // Invoice implements IOrder
    // - uses normal (implicit) interface implementation
    // ===========================
    class Invoice : IOrder
    {
        public int Id { get; private set; }
        public decimal Total { get; private set; }

        public Invoice(int id, decimal total)
        {
            Id = id;
            Total = total;
        }

        public decimal GetPaymentAmount()
        {
            return Total;
        }

        public void Print()
        {
            Console.WriteLine($"Invoice #{Id}, Total: {Total}");
        }
    }

    // ===========================
    // Product implements IPrintable + IShippable
    // - mixes implicit and explicit implementations
    // ===========================
    class Product : IPrintable, IShippable
    {
        public string Title { get; private set; }
        public decimal Price { get; private set; }
        public decimal WeightKg { get; private set; }

        public Product(string title, decimal price, decimal weightKg)
        {
            Title = title;
            Price = price;
            WeightKg = weightKg;
        }

        // - Implicit implementation (normal method)
        // - Can be called directly via Product variable
        public void Print()
        {
            Console.WriteLine($"Product: {Title}, Price: {Price}, Weight: {WeightKg}kg");
        }

        // - Explicit implementation
        // - Can NOT be called via Product variable
        // - Must cast to IShippable
        decimal IShippable.GetShippingCost()
        {
            return 50m + 20m * WeightKg;
        }

        string IShippable.GetShippingLabel()
        {
            return $"SHIP: {Title} ({WeightKg}kg)";
        }
    }

    // ===========================
    // Multiple interface implementation + explicit implementation demo
    // ===========================
    class OnlineOrder : IOrder, IShippable
    {
        public int Id { get; private set; }
        public decimal Total { get; private set; }
        public string Address { get; private set; }

        public OnlineOrder(int id, decimal total, string address)
        {
            Id = id;
            Total = total;
            Address = address;
        }

        // - Explicit implementation for IPrintable
        // - This hides Print() from the normal OnlineOrder API
        void IPrintable.Print()
        {
            Console.WriteLine($"OnlineOrder #{Id}, Total: {Total}, Address: {Address}");
        }

        // - Explicit implementation for IPayable
        decimal IPayable.GetPaymentAmount()
        {
            return Total;
        }

        // - Explicit implementation for IShippable
        decimal IShippable.GetShippingCost()
        {
            return 100m;
        }

        string IShippable.GetShippingLabel()
        {
            return $"SHIP TO: {Address}";
        }

        // - Normal class method (not interface)
        // - Accessible directly via OnlineOrder variable
        public void PrintInternal()
        {
            Console.WriteLine($"(Internal) Order #{Id} for {Address}");
        }
    }

    // ===========================
    // Name conflict demo:
    // - two interfaces with the same member name
    // - explicit implementation allows both to exist
    // ===========================
    interface ITextPrinter
    {
        void Print();
    }

    class MultiPrinter : IPrintable, ITextPrinter
    {
        // - Two different Print() behaviors depending on interface view
        void IPrintable.Print()
        {
            Console.WriteLine("IPrintable.Print -> general printing");
        }

        void ITextPrinter.Print()
        {
            Console.WriteLine("ITextPrinter.Print -> text-only printing");
        }

        // - If you want, you can also provide a normal method with the same name
        // - but then interface calls can still be different
        public void Print()
        {
            Console.WriteLine("MultiPrinter.Print -> normal class method");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ===========================
            // Example A: interface variables (polymorphism)
            // ===========================
            IPrintable p1 = new Student("Denys", 3);
            IPrintable p2 = new Product("Keyboard", 1200m, 1.2m);
            IPrintable p3 = new Invoice(101, 999m);

            p1.Print();
            p2.Print();
            p3.Print();

            Console.WriteLine();

            // ===========================
            // Example B: explicit implementation visibility
            // ===========================
            Product prod = new Product("Mouse", 500m, 0.3m);

            // - Works because Print() is implicit implementation
            prod.Print();

            // prod.GetShippingCost(); // ERROR: not visible on Product type
            // prod.GetShippingLabel(); // ERROR: not visible on Product type

            // - To access explicit members, use interface variable or cast
            IShippable shipProd = prod;
            Console.WriteLine("Shipping cost: " + shipProd.GetShippingCost());
            Console.WriteLine("Shipping label: " + shipProd.GetShippingLabel());

            Console.WriteLine();

            // ===========================
            // Example C: list of interface type
            // ===========================
            List<IPrintable> printableItems = new List<IPrintable>();
            printableItems.Add(new Student("Oleh", 2));
            printableItems.Add(new Product("Headphones", 2000m, 0.6m));
            printableItems.Add(new Invoice(202, 1500m));
            printableItems.Add(new OnlineOrder(303, 2500m, "Kyiv, Main St 1"));

            foreach (IPrintable item in printableItems)
                item.Print(); // OnlineOrder.Print is explicit, but works via interface reference

            Console.WriteLine();

            // ===========================
            // Example D: same object, different interface views
            // ===========================
            OnlineOrder order = new OnlineOrder(777, 3000m, "Lviv, Freedom Ave 10");

            // order.Print(); // ERROR: explicit IPrintable.Print is hidden from class API
            // order.GetPaymentAmount(); // ERROR: explicit IPayable.GetPaymentAmount is hidden
            // order.GetShippingLabel(); // ERROR: explicit IShippable.GetShippingLabel is hidden

            order.PrintInternal(); // normal method is visible

            IOrder asOrder = order;       // IOrder includes IPrintable + IPayable
            IShippable asShip = order;

            asOrder.Print(); // calls explicit IPrintable.Print
            Console.WriteLine("Payment: " + asOrder.GetPaymentAmount());
            Console.WriteLine("Label: " + asShip.GetShippingLabel());
            Console.WriteLine("Shipping cost: " + asShip.GetShippingCost());

            Console.WriteLine();

            // ===========================
            // Example E: name conflict resolved by explicit implementation
            // ===========================
            MultiPrinter mp = new MultiPrinter();

            mp.Print(); // normal class method

            IPrintable ip = mp;
            ip.Print(); // IPrintable.Print

            ITextPrinter it = mp;
            it.Print(); // ITextPrinter.Print

            Console.ReadLine();
        }
    }
}

// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/interfaces
// Docs: https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation
