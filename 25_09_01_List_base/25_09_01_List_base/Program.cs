using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_09_01_List_base
{
    internal class Program
    {
        // Template method
        static void Show_list<T>(List<T> x)
        {
            foreach (T a in x)
                Console.Write($"{a,-10}");
            Console.WriteLine("\n");
        }


        static void Main(string[] args)
        {
            // Example 1 (Add, Count, Sort, Reverse, Remove)
            Console.WriteLine("Example 1:");

            List<int> pisunchiki = new List<int>();

            pisunchiki.Add(123);
            pisunchiki.Add(2);
            pisunchiki.Add(45);
            pisunchiki.Add(1);
            pisunchiki.Add(3);
            pisunchiki.Add(45);
            Show_list(pisunchiki);

            Console.WriteLine("Number of elements:");
            Console.WriteLine(pisunchiki.Count() + "\n\n");


            pisunchiki.Sort();                              // by ascending (from smallest to biggest)
            Show_list(pisunchiki);


            pisunchiki.Reverse();
            Show_list(pisunchiki);


            int rem = 45;
            Console.WriteLine($"After removing {rem}:");
            pisunchiki.Remove(rem);                         // value of element in List
            Show_list(pisunchiki);


            Console.WriteLine("Number of elements:");
            Console.WriteLine(pisunchiki.Count() + "\n\n");




            // Example 2 (RemoveAt)
            Console.WriteLine("Example 2:");

            List<float> temperature = new List<float> { 1.734f, 34.238f, -14.925f, 21.465f};
            Show_list(temperature);


            temperature.RemoveAt(2);                        // index of element in List

            for(int i = 0; i < temperature.Count(); i++)
            {
                Console.Write($"{temperature[i],-10}");
            }
            Console.WriteLine("\n");




            // Example 3 (Insert, Contains, IndexOf)
            Console.WriteLine("Example 3:");
            // Insert Syntax: exampleList.Insert(index, elementToInsert);
            List<int> numbers = new List<int> { 2, 4, 6, 10, 12 };
            Show_list(numbers);


            numbers.Insert(3, 8);
            Show_list(numbers);

            // Contains return True or False
            int x = 7, y = 8;
            Console.WriteLine($"List contain {x}: {numbers.Contains(x)}");
            Console.WriteLine($"             {y}: {numbers.Contains(y)} \n");

            // IndexOf Syntax: exampleList.IndexOf(element);
            // If the element doesn't exist in the list, it simply returns -1
            int targetindex;
            targetindex = numbers.IndexOf(10);

            Console.WriteLine($"Element with index of {targetindex} is {numbers[targetindex]}");


            numbers.Clear();
            Show_list(numbers); // nothing left




            Console.ReadLine();
        }
    }
}
