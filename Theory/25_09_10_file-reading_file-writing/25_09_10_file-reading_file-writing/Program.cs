using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_09_10_file_reading_file_writing
{
    internal class Program
    {
        // Root directory for all examples (VERBATIM STRING → backslashes are not escaped)
        static readonly string root = @"C:\Users\ne_kazual\Desktop\Cs_Theory-Practice\25_09_10_file-reading_file-writing";

        // Helper to build full paths quickly
        static string P(string fileName) => Path.Combine(root, fileName);

        static void Main(string[] args)
        {
            // ===========================
            // STREAMWRITER BASICS (WRITE)
            // ===========================
            // StreamWriter writes text to a file.

            // Common overloads:
            // new StreamWriter(path) → create/overwrite file
            // new StreamWriter(path, append: true) → append to existing content in file
            // Close() will flush buffers and release the file handle.

            // Example A1: overwrite file and write a few lines
            StreamWriter w1 = new StreamWriter(P("sample_overwrite.txt"));
            w1.WriteLine("Line 1");   // writes with newline
            w1.Write("Line 2");       // writes without newline
            w1.WriteLine();           // manual newline
            w1.WriteLine("Line 3");
            w1.Close(); // disposed here → file closed

            // Example A2: append mode (append: true) — will add to existing content instead of replacing
            StreamWriter w2 = new StreamWriter(P("sample_overwrite.txt"), append: true);
            w2.WriteLine("Appended line 4");  // appended at the end
            w2.Close();


            // ===========================
            // STREAMREADER BASICS (READ)
            // ===========================
            // StreamReader reads text from a file.
            // Key methods:
            // ReadLine() → reads one line, returns null at EOF
            // ReadToEnd() → reads the rest of the stream as a single string
            // Read() → reads next character (int), returns -1 at EOF
            // Peek() → lookahead next char without consuming, returns -1 at EOF

            // Example B1: read line-by-line
            StreamReader r1 = new StreamReader(P("sample_overwrite.txt"));
            string line;
            while ((line = r1.ReadLine()) != null)
            {
                Console.WriteLine("[B1] " + line);
            }
            r1.Close();

            // Example B2: read everything at once (useful for small files)
            StreamReader r2 = new StreamReader(P("sample_overwrite.txt"));
            string all = r2.ReadToEnd();
            Console.WriteLine("\n[B2] WHOLE FILE:\n" + all);
            r2.Close();

            // Example B3: char-by-char with Peek()
            StreamReader r3 = new StreamReader(P("sample_overwrite.txt"));
            Console.WriteLine("\n[B3] CHAR BY CHAR:");
            while (r3.Peek() != -1)           // -1 means EOF
            {
                int ch = r3.Read();           // returns Unicode code point as int
                Console.Write((char)ch);
            }
            Console.WriteLine();
            r3.Close();


            // =========
            // PRACTICE
            // =========

            // -------- TASK 1 --------
            // Create "task1.txt" and write 20 identical lines
            StreamWriter task1 = new StreamWriter(P("task1.txt"));
            for (int i = 0; i < 20; i++)
            {
                task1.WriteLine("Penis"); // simple demo string as in your original code
            }
            task1.Close(); // file closed manually

            // -------- TASK 2 --------
            // Merge two files ("task1.txt" and "fucmaass.txt") into "merget.txt"
            // Notes:
            // - "fucmaass.txt" is filled manually as you said.
            // - We open "merget.txt" in overwrite mode (append=false by default).
            StreamReader text1 = new StreamReader(P("task1.txt"));
            StreamReader text2 = new StreamReader(P("fucmaass.txt"));
            StreamWriter task2 = new StreamWriter(P("merget.txt"));

            while ((line = text1.ReadLine()) != null)
                task2.WriteLine(line);

            while ((line = text2.ReadLine()) != null)
                task2.WriteLine(line);

            text1.Close();
            text2.Close();
            task2.Close();

            // -------- TASK 3 --------
            // Read "merget.txt" and count how many lines it has
            int lineCount = 0;
            StreamReader task3 = new StreamReader(P("merget.txt"));
            while ((line = task3.ReadLine()) != null)
                lineCount++;
            task3.Close();
            Console.WriteLine("\n[Task 3] Line count in merget.txt = " + lineCount);

            // ===========================================================
            // APPEND MODE DEMO (IMPORTANT PART YOU ASKED ABOUT)
            // ===========================================================
            // When you pass 'append: true' to StreamWriter, new data is ADDED
            // after the existing content rather than replacing the file content.
            StreamWriter appendDemo = new StreamWriter(P("append_demo.txt"), append: true);
            appendDemo.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | appended record");
            appendDemo.Close();
            // Run the program multiple times and watch "append_demo.txt" grow.

            Console.WriteLine("\nDone. Check files in: " + root);
            Console.ReadLine();
        }

    }
}

/*
CHEATSHEET:
- StreamWriter:
  * OVERWRITE: new StreamWriter(path)
  * APPEND:    new StreamWriter(path, true)
  * WriteLine(...) adds a newline; Write(...) does not.
  * Always Close() or wrap with 'using' to flush and release the file.
  
- StreamReader:
  * ReadLine() returns null at EOF.
  * ReadToEnd() returns the rest as one string.
  * Read() returns next char code (int), -1 at EOF.
  * Peek() lets you check for EOF without consuming characters.
  

*/

// Docs:
// StreamWriter: https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter
// StreamReader: https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader