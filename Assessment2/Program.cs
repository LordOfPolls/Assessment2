using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment2
{
    class Program
    {
        static void TypeWrite(string text)
        {
            Console.Title = text;
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                System.Threading.Thread.Sleep(30);
            }
            Console.Write("\n");
        }

        static void Sleep(int time)
        {
            try
            {
                System.Threading.Thread.Sleep(time);
            }
            catch
            {
                return;
            }
        }

        static string[] Load(string file) {
            try
            {
                string[] f = System.IO.File.ReadAllLines(@file);
                return (f);
            }
            catch
            {
                Console.Write($"CRITICAL ERROR READING FILE {file}");
                Sleep(1000);
                Environment.Exit(0);
            }
            return (new string[10] );//DumbyLineToAllowFunction 

        }

        static void Main(string[] args)
        {
            string[] files = { "Close_128.txt", "Change_128.txt", "Open_128.txt", "High_128.txt", "Low_128.txt" };
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWrite("Loading arrays...");
                string[] Close_128 = Load($"Resources/{files[0]}");
                string[] Change_128 = Load($"Resources/{files[1]}");
                string[] Open_128 = Load($"Resources/{files[2]}");
                string[] High_128 = Load($"Resources/{files[3]}");
                string[] Low_128 = Load($"Resources/{files[4]}");
                Console.Clear();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("CRITICAL ERROR IN ARRAY LOAD, QUITING");
                Sleep(10000);
                Environment.Exit(0);
            }

            TypeWrite("Arrays Loaded");
            Sleep(1000);
            Console.Clear();
            Console.ResetColor();
            TypeWrite($"Choose your array:\n1: {files[0]}\n2: {files[1]}\n3: {files[2]}\n4: {files[3]}\n5: {files[4]}");
            int chosen_file = Convert.ToInt32(Console.ReadLine()) -1;
            string[] Choice = Load($"Resources/{files[chosen_file]}");
            Console.Clear();

            DisplayArray(Choice, files, chosen_file);

            Console.ReadKey();
        }

        static void DisplayArray(string[] Array, string[] files, int chosen_file)
        {
            Console.ResetColor();
            TypeWrite($"{files[chosen_file]} Selected");
            TypeWrite("Display mode:");
            TypeWrite("1: Assending");
            TypeWrite("2: Descending");
            TypeWrite("3: Raw");
            int display_mode = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (display_mode)
            {
                case 1:
                    string[] tempAssend = Array.OrderBy(c => c).ToArray();
                    for (int i = 0; i < tempAssend.Length; i++)
                    {
                        Console.WriteLine(tempAssend[i]);
                    }
                    break;
                case 2:
                    string[] tempDescend = Array.OrderByDescending(c => c).ToArray();
                    for (int i = 0; i < tempDescend.Length; i++)
                    {
                        Console.WriteLine(tempDescend[i]);
                    }
                    break;
                case 3:
                    for (int i = 0; i < Array.Length; i++)
                    {
                        Console.WriteLine(Array[i]);
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite("Thats not a valid choice");
                    Sleep(200);
                    DisplayArray(Array, files, chosen_file);
                    return;
            }
        }

    }
}
