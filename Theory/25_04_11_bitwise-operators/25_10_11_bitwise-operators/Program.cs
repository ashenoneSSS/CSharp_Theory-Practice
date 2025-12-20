using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_10_11_bitwise_operators
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // RULE 1: all bitwise operators work only with integers.

            // RULE 2: bitwise operators (&, |, ^, ~, <<, >>) work with integer types (byte, short, int, long, sbyte, ushort, uint, ulong).
            // If operands have a type less than int (for example, byte or short), they are implicitly converted to int before the operation. The result will be of type int.


            // 1. Type: ~
            // Name: Bitwise complement
            // Syntax: ~integer_number

            // Sense: ~ operator produces a bitwise complement of its operand by reversing each bit

            int a = 0b11000111;             // 00000000 00000000 00000000 11000111
            int b = ~a;                     // 11111111 11111111 11111111 00111000
            Console.WriteLine(a);           // 199
            Console.WriteLine(b + "\n");    // -200

            byte c = 0b10101010;
            // byte d = ~c;                 // compilation error (because of RULE 2)
            byte d = (byte)~c;              // 01010101

            Console.WriteLine(c);           // 170
            Console.WriteLine(d + "\n\n");  // 85


            // 2. Type: &
            // Name: bitwise logical AND
            // Syntax: integer_number & integer_number

            // Bit comparison rules:
            // 1 & 1 = 1
            // 1 & 0 = 0
            // 0 & 1 = 0
            // 0 & 0 = 0

            byte number1 = 0b00110101;
            byte number2 = 0b01010101;
            //result         00010101  
            byte result1 = (byte)(number1 & number2);

            Console.WriteLine(result1 + "\n"); // 21


            // 3. Type: |
            // Name: bitwise logical OR
            // Syntax: integer_number | integer_number

            // Bit comparison rules:
            // 1 | 1 = 1
            // 1 | 0 = 1
            // 0 | 1 = 1
            // 0 | 0 = 0

            byte number3 = 0b00110101;
            byte number4 = 0b01010101;
            //result         01110101
            byte result2 = (byte)(number3 | number4); 

            Console.WriteLine(result2 + "\n"); // 117


            // 4. Type: ^
            // Name: bitwise logical exclusive OR
            // Syntax: integer_number ^ integer_number

            // Bit comparison rules:
            // 1 ^ 1 = 0
            // 1 ^ 0 = 1
            // 0 ^ 1 = 1
            // 0 ^ 0 = 0

            byte number5 = 0b00110101;
            byte number6 = 0b01010101;
            //result         01100000
            byte result3 = (byte)(number5 ^ number6);

            Console.WriteLine(result3 + "\n"); // 96


            // shift operators link:
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/bitwise-and-shift-operators#left-shift-operator-

            // 5. Type: <<
            // Name: bitwise left shift
            // Syntax: integer_number << integer_number

            // Sense: The left-shift operation discards the high-order bits that are outside the range of
            // the result type and sets the low-order empty bit positions to zero, as the following example shows

            byte number7 = 0b00001100;
            byte number8 = (byte)(number7 << 2);

            Console.WriteLine(number7); // 12
            Console.WriteLine(number8); // 48

            byte number9 = 0b11110000;
            Console.WriteLine(number9); // 240
            Console.WriteLine(number9 << 4); // 3840


            // 6. Type: >>
            // Name: bitwise right shift
            // Syntax: integer_number >> integer_number

            byte number10 = 0b00001100;
            byte number11 = (byte)(number10 >> 2);

            Console.WriteLine(number10); // 12
            Console.WriteLine(number11); // 3

            byte number12 = 0b11110000;
            Console.WriteLine(number12); // 240
            Console.WriteLine(number12 >> 2); // 60




            Console.ReadLine();
        }
    }
}
