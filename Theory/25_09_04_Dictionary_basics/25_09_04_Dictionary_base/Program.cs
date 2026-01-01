using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_09_04_Dictionary_base
{
    internal class Program
    {
        static void Show_dictionary<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            foreach (KeyValuePair<TKey, TValue> kv in dict)
                Console.WriteLine($"{kv.Key,-10} {kv.Value,-10}");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Example 1 (Add, Count, Remove, Clear)
            Console.WriteLine("Example 1:");

            Dictionary<string, int> ages = new Dictionary<string, int>();

            ages.Add("Alice", 25);
            ages.Add("Bob", 30);
            ages.Add("Charlie", 22);
            Show_dictionary(ages);

            Console.WriteLine("Number of elements:");
            Console.WriteLine(ages.Count() + "\n");

            Console.WriteLine("After removing Bob:");
            ages.Remove("Bob");                // remove by key
            Show_dictionary(ages);

            Console.WriteLine("Number of elements:");
            Console.WriteLine(ages.Count() + "\n");

            ages.Clear();
            Console.WriteLine("After Clear:");
            Show_dictionary(ages);



            // Example 2 (Alternaitive initialize, access to value, update values)
            Console.WriteLine("Example 2:");

            Dictionary<string, string> capitals = new Dictionary<string, string>
            {
                { "Ukraine", "Kyiv" },
                { "France", "Paris" },
                { "Japan", "Tokyo" }
            };
            Show_dictionary(capitals);

            // Access value by key
            Console.WriteLine("Capital of France: " + capitals["France"]);

            // Update value
            capitals["France"] = "Huiris";
            Console.WriteLine("Capital of France after update: " + capitals["France"] + "\n");



            // Example 3 (ContainsKey, ContainsValue)
            Console.WriteLine("Example 3:");

            Dictionary<int, string> idNames = new Dictionary<int, string>
            {
                { 1, "Tom" },
                { 2, "Jerry" },
                { 3, "Spike" }
            };
            Show_dictionary(idNames);

            Console.WriteLine("ContainsKey(2): " + idNames.ContainsKey(2));
            Console.WriteLine("ContainsKey(5): " + idNames.ContainsKey(5));
            Console.WriteLine("ContainsValue(\"Spike\"): " + idNames.ContainsValue("Spike"));
            Console.WriteLine("ContainsValue(\"Butch\"): " + idNames.ContainsValue("Butch") + "\n");

            



            Console.ReadLine();

        }
    }
}
