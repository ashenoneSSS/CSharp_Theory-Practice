using System.Diagnostics;
using System.Xml;

namespace _25_02_03_switch_feature_Cs9._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //in C#9.0 we can use relational peterns (>, >=, <, <=, and)

            //example 1 (same as previous solution but with relational peterns)
            int age = 20;

            switch (age)
            {
                case < 18:
                    Console.WriteLine("Minor");
                    break;
                case >= 18 and < 65:
                    Console.WriteLine("Adult");
                    break;
                default:
                    Console.WriteLine("Senior");
                    break;
            }
            Console.WriteLine();




            //example 2
            int num = -7;

            switch (num)
            {
                case <0:
                    Console.WriteLine("Number is negative");
                    break;
                case >15:
                    Console.WriteLine("Number is grater than 15");
                    break;
                case >0:
                    Console.WriteLine("Number is positive");
                    break;
                default:
                    Console.WriteLine("Number is equal 0");
                    break;
            }

            //but if we will replace second and third case it will error

            /*
            switch (num)
            {
                case <0:
                    Console.WriteLine("Number is negative");
                    break;
                case >0:
                        Console.WriteLine("Number is positive");
                        break;
                case >15:
                    Console.WriteLine("Number is grater than 15");
                    break;
                default:
                    Console.WriteLine("Number is equal 0");
                    break;
            }
            */

            //!!! Switch cases must be unique !!!
            //case >0 inclused case >15
            //and because of C# evaluates case blocks in order case >0 make case >15 unreachable

            //conclusion: Put the most restrictive conditions first to avoid conflicts

        }
    }
}
