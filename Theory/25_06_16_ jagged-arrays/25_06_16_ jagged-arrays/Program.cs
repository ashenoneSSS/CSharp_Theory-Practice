using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_06_16__jagged_arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // A jagged array is an array whose elements are arrays,
            // possibly of different sizes. Its elements are reference types

            // Initialazing examples
            // first
            int[][] jaggedArray = new int[3][];

            jaggedArray[0] = new int[] { 1, 3, 5, 7, 9 };
            jaggedArray[1] = new int[] { 0, 2, 4, 6 };
            jaggedArray[2] = new int[] { 11, 22 };

            // second
            int[][] jaggedArray2 =
            {
                new int[] { 1, 3, 5, 7, 9 },
                new int[] { 0, 2, 4, 6 },
                new int[] { 11, 22 }
            };

            // Output
            for (int i = 0; i < jaggedArray2.Length; i++)
            {
                for (int j = 0; j < jaggedArray2[i].Length; j++)
                {
                    Console.Write(jaggedArray2[i][j] + " ");
                }
                Console.WriteLine();
            }

            // assign 77 to the second element ([1]) of the first array ([0]):
            jaggedArray2[0][1] = 77;

            // assign 88 to the second element ([1]) of the third array ([2]):
            jaggedArray2[2][1] = 88;



            // Also It's possible to mix jagged and multidimensional arrays

            int[][][] jagged3D = new int[2][][];

            jagged3D[0] = new int[2][];
            jagged3D[0][0] = new int[] { 1, 2 };
            jagged3D[0][1] = new int[] { 3 };

            jagged3D[1] = new int[1][];
            jagged3D[1][0] = new int[] { 4, 5, 6 };



            Console.ReadLine();
        }
    }
}
