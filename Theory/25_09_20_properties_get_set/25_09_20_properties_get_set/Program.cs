using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_09_20_properties_get_set
{
    // ===========================
    // What is a property
    // ===========================
    // - A property looks like a field from outside, but it's actually methods (get/set) inside
    // - Properties are used to control access to data (encapsulation)
    // - You can validate values in set
    // - You can make read-only or restrict writing (private set)
    // - You can compute values on the fly in get

    class Person
    {
        // ===========================
        // 1) Full property + backing field
        // ===========================
        // - Backing field is a normal private field that stores the value
        // - Property wraps it with get/set and can validate input

        private string _name; // backing field

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                // - Example of validation logic
                // - If invalid input comes, we keep old value (or could throw exception)
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Name cannot be empty");
                    return;
                }

                _name = value;
            }
        }



        // ===========================
        // 2) Auto-property
        // ===========================
        // - Compiler creates a hidden backing field (private field that stores the data for a corresponding property) automatically
        // - Use it when no custom logic is needed

        public int Age { get; set; }



        // ===========================
        // 3) Get-only property
        // ===========================
        // - Can be assigned in the constructor (or inline initializer)
        // - After construction it becomes read-only from outside

        public int Id { get; }



        // ===========================
        // 4) Property with private set
        // ===========================
        // - Readable from outside
        // - Writable only inside the class
        // - Useful when only methods of the class should change the value

        public decimal Balance { get; private set; }


        // Constructor
        public Person(int id)
        {
            Id = id;            // set get-only property
            Balance = 0m;       // set private-set property inside class
            Age = 0;            // auto-property default can be overwritten
            _name = "Unknown";  // backing field default
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Deposit amount must be positive");
                return;
            }

            Balance += amount; // allowed because private set can be used inside the class
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person(1001);

            // Full property + backing field (validation inside set)
            p.Name = "Denys";
            Console.WriteLine("Name: " + p.Name);

            // Auto-property (simple storage without custom logic)
            p.Age = 19;
            Console.WriteLine("Age: " + p.Age);

            // Get-only property (read-only from outside, value set in constructor)
            Console.WriteLine("Id (get-only): " + p.Id);

            // { get; private set; } (read outside, write only inside the class)
            Console.WriteLine("Balance before: " + p.Balance);
            p.Deposit(200);
            Console.WriteLine("Balance after deposit: " + p.Balance);

            // p.Balance = 999; // ERROR: private set, cannot set from outside

            Console.ReadLine();
        }
    }

    
}
