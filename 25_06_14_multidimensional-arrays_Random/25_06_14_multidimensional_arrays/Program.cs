using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_06_14_multidimensional_arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Syntax for creating a multidimensional array

            // DataType[] Name = new DataType[NumberOfElements];
            // DataType[,] Name = new DataType[NumberOfArrays, NumberOfElements];
            // DataType[,,] Name = new DataType[NumberOf2DArrays, NumberOfArrays, NumberOfElements];
            // ......

            int[] array1D = new int[5];                    // [0 0 0 0 0]
            double[,] array2D = new double[2, 3];          // [0 0 0], [0 0 0]
            string[,,] array3D = new string[2, 3, 4];      // [["" "" ""] ["" "" ""]]
                                                           // [["" "" ""] ["" "" ""]]


            // Creating an initialized multidimensional array

            int[] array1d = new int[5] { 1, 2, 3, 4, 5 };

            double[,] array2d = new double[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };

            string[,,] array3d = new string[2, 3, 4]
            {
                {
                    { "Demian1", "Helena1", "Vika1", "Anna1" },
                    { "Snigana1", "Iryna1", "Nazar1", "Adnriy1" },
                    { "Volodimir1", "Taras1", "Yana1", "Oksana1" }
                },
                {
                    { "Demian2", "Helena2", "Vika2", "Anna2" },
                    { "Snigana2", "Iryna2", "Nazar2", "Adnriy2" },
                    { "Volodimir2", "Taras2", "Yana2", "Oksana2" }
                }
            };


            // Length

            int[] Array1D = new int[5];
            double[,] Array2D = new double[2, 3];
            string[,,] Array3D = new string[2, 3, 4];

            Console.WriteLine(Array1D.Length); // 5
            Console.WriteLine(Array2D.Length); // 6
            Console.WriteLine(Array3D.Length); // 24
            Console.WriteLine();


            // GetLength

            int[] array_1D = new int[5];
            double[,] array_2D = new double[2, 3];
            string[,,] array_3D = new string[2, 3, 4];

            Console.WriteLine(array_1D.GetLength(0));     // 5

            Console.WriteLine(array_2D.GetLength(0));     // 2
            Console.WriteLine(array_2D.GetLength(1));     // 3

            Console.WriteLine(array_3D.GetLength(0));     // 2
            Console.WriteLine(array_3D.GetLength(1));     // 3
            Console.WriteLine(array_3D.GetLength(2));     // 4
            Console.WriteLine();


            // Two ways to output 2D array

            int[,] arr = new int[5, 4]
            {
                { 1, 2, 3, 4 },
                { 10, 22, 33, 40 },
                { 11, 22, 33, 44 },
                { 101, 102, 103, 104 },
                { 111, 222, 333, 444 },
            };

            // First way:
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Second way:
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write($"{arr[i, j],4} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();



            //Prictice (+ RANDOM in use)

            // Sum of row
            int[,] sarr = new int[5, 5];
            Random rnd = new Random();

            for (int i = 0; i < sarr.GetLength(0); i++)
            {
                for (int j = 0; j < sarr.GetLength(1); j++)
                {
                    sarr[i, j] = rnd.Next(0, 101);
                }
            }

            for (int i = 0; i < sarr.GetLength(0); i++)
            {
                int sum = 0;
                for (int j = 0; j < sarr.GetLength(1); j++)
                {
                    Console.Write($"{sarr[i, j],-5}");
                    sum += sarr[i, j];
                }
                Console.Write($"{sum,8}");
                Console.WriteLine();
            }
            Console.WriteLine();



            // Transformed matrix
            int[,] transformed = new int[5, 5];

            for (int i = 0; i < transformed.GetLength(0); i++)
            {
                for (int j = 0; j < transformed.GetLength(1); j++)
                {
                    transformed[i, j] = sarr[j, i];
                }
            }

            for (int i = 0; i < transformed.GetLength(0); i++)
            {
                for (int j = 0; j < transformed.GetLength(1); j++)
                {
                    Console.Write($"{transformed[i, j],-5}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();



            Console.ReadLine();
        }
    }
}
