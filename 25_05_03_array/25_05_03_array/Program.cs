using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_05_03_array
{
    class Program
    {
        static void Main()
        {
            //Syntax of arrays
            // 1) DataType[] ArrayName = new DataType[NumberOfElements];
            int[] array1 = new int[5];

            // 2) DataType[] ArrayName = new DataType[NumberOfElements] { InitializationList };
            double[] array2 = new double[3] { 1.1, 2.3, 4.5 };

            // 3) DataType[] ArrayName = { InitializationList };
            char[] array3 = { 'a', 'b', 'c' };


            //Extend the array
            int[] array_ne = new int[5] { 2, -6, 7, -3, 8 };
            int[] array_ext = new int[7];

            for (int i = 0; i < array_ne.Length; i++)
            {
                Console.Write(array_ne[i] + "\t");
            }
            Console.WriteLine();

            array_ext[0] = 666;
            for (int i = 1; i < 6; i++)
            {
                array_ext[i] = array_ne[i - 1];
            }
            array_ext[6] = 777;

            for (int i = 0; i < array_ext.Length; i++)
            {
                Console.Write(array_ext[i] + "\t");
            }
            Console.WriteLine("\n");


            //Allowed structure
            int[] array_x = new int[6] { 1, 2, 3, 4, 5, 6 };
            array_x = new int[7] { 30, 40, 50, 60, 70, 80, 90 };  //garbage Collector works, so you don't need to free arrays manually




            //Practice (Palindrom)
            Console.WriteLine("Enter array size: ");
            int size = int.Parse(Console.ReadLine());
            int[] arr = new int[size];


            Console.WriteLine("\nEnter elements of array:");
            for (int i = 0; i < size; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }

            bool isPalindrome = true;

            for (int i = 0; i < size / 2; i++)
            {
                if (arr[i] != arr[size - 1 - i])
                {
                    isPalindrome = false;
                    break;
                }
            }

            if (isPalindrome)
            {
                Console.WriteLine("Is Palindrome");
            }
            else
            {
                Console.WriteLine("Is Not Palindrome");
            }

            Console.ReadLine();
        }
    }
}
