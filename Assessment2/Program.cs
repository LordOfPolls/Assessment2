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

        static double[] Load(string file)
        {
            try
            {
                string[] f = System.IO.File.ReadAllLines(@file);
                Double[] x = Array.ConvertAll(f, Double.Parse);
                return (x);
            }
            catch
            {
                Console.Write($"CRITICAL ERROR READING FILE {file}");
                Sleep(1000);
                Environment.Exit(0);
            }
            return (new double[10]);//DumbyLineToAllowFunction 
        }

        private static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Boot...";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            string[] files = { "Close_128.txt", "Change_128.txt", "Open_128.txt", "High_128.txt", "Low_128.txt" };

            try
            {
                TypeWrite("Loading arrays...");
                double[] Close_128 = Load($"Resources/{files[0]}");
                double[] Change_128 = Load($"Resources/{files[1]}");
                double[] Open_128 = Load($"Resources/{files[2]}");
                double[] High_128 = Load($"Resources/{files[3]}");
                double[] Low_128 = Load($"Resources/{files[4]}");
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
            Console.ForegroundColor = ConsoleColor.White;
            TypeWrite($"Choose your array:\n1: {files[0]}\n2: {files[1]}\n3: {files[2]}\n4: {files[3]}\n5: {files[4]}");
            int chosen_file = Convert.ToInt32(Console.ReadLine()) - 1;
            double[] Choice = Load($"Resources/{files[chosen_file]}");
            Console.Title = $"Reading from {files[chosen_file]}";

            Console.Clear();
            TypeWrite("Choose mode:\n\n1: Display\n2: Search");
            int mode = Convert.ToInt32(Console.ReadLine());
            switch (mode)
            {
                case 1:
                    DisplayArray(Choice, files, chosen_file);
                    break;
                case 2:
                    SearchArray(Choice, files, chosen_file);
                    break;
            }

            Console.ReadKey();
        }

        static void DisplayArray(double[] Array, string[] files, int chosen_file)
        {
            Console.Clear();
            Console.ResetColor();
            TypeWrite($"{files[chosen_file]} Selected");
            TypeWrite("Display mode:");
            TypeWrite("1: Assending");
            TypeWrite("2: Descending");
            TypeWrite("3: Raw");
            TypeWrite("4: Quicksort Assending");
            TypeWrite("4: Quicksort Descending");

            int display_mode = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            switch (display_mode)
            {
                case 1:
                    double[] tempAssend = Array.OrderBy(c => c).ToArray();
                    for (int i = 0; i < tempAssend.Length; i++)
                    {
                        Console.WriteLine($"{(i+1).ToString("#000.##")}|  {tempAssend[i]}");
                    }
                    break;
                case 2:
                    double[] tempDescend = Array.OrderByDescending(c => c).ToArray();
                    for (int i = 0; i < tempDescend.Length; i++)
                    {
                        Console.WriteLine($"{(i + 1).ToString("#000.##")}|  {tempDescend[i]}");
                    }
                    break;
                case 3:
                    for (int i = 0; i < Array.Length; i++)
                    {
                        Console.WriteLine($"{(i+1).ToString("#000.##")}| {Array[i]}");
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

        static void SearchArray(double[] Array, string[] files, int chosen_file)
        {
            Console.Clear();
            Console.ResetColor();

            double searchItem = 0;
            TypeWrite("What would you like to search for? ");
            try
            {
                searchItem = Double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWrite("Invalid search, search must be a floating point int");
                Sleep(4000);
                SearchArray(Array, files, chosen_file);
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            TypeWrite($"Searching {Array.Length} items");
            Sleep(1000);
            bool found = false;
            Console.Clear();
            double mDistance = 0; //f*ck the compiler
            int Index = -1;

            for (int i = 0; i < Array.Length; i++)
            {
                var cDistance = Math.Abs(searchItem - Array[i]);
                if (Index == -1 || cDistance < mDistance)
                {
                    mDistance = cDistance;
                    Index = i;
                    if (mDistance == 0) //Found the f*cker
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        found = true;
                        TypeWrite("Sucess");
                        try //avoids overflows and underflows when outputting
                        {
                            TypeWrite($"{searchItem} on line {i + 1}\n\n{i}. {Array[i - 1]}\n{i + 1}. {Array[i]}\n{i + 2}. {Array[i + 1]}");
                        }
                        catch
                        {
                            try
                            {
                                TypeWrite($"{searchItem} on line {i + 1}\n\n{i + 1}. {Array[i]}\n{i + 2}. {Array[i + 1]}\n{i + 3}. {Array[i + 2]}");
                            }
                            catch
                            {
                                TypeWrite($"{searchItem} on line {i + 1}\n\n{i - 1}. {Array[i - 2]}\n{i}. {Array[i - 1]}\n{i + 1}. {Array[i]}");

                            }

                        }
                        break;
                    }
                }
            }

            if (found == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWrite($"Unable to find {searchItem}");
                TypeWrite($"Nearest value was {Array[Index]} on line {Index + 1}");
            }
            TypeWrite("search again? y or n");
            string reply = Console.ReadLine();
            bool restart;
            if (reply == "y")
                restart = true;
            else
                restart = false;
            if (restart)
                SearchArray(Array, files, chosen_file);
            return;


        }

    }
}
