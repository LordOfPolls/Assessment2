/*
Name: Assessment2
Author: Daniel Bearman
Creation: 18/03/2018
State: INCOMPLETE
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Assessment2
{
    class Program
    {
        static void TypeWrite(object text)
        {
            string T = text.ToString();
            for (int i = 0; i < T.Length; i++)
            {
                Console.Write(T[i]);
                System.Threading.Thread.Sleep(new Random().Next(10, 40));
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
                Console.Write("Sleep command error");
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            string[] files = { "Close_[size].txt", "Change_[size].txt", "Open_[size].txt", "High_[size].txt", "Low_[size].txt", "Merge_[size].txt" };
            Console.ForegroundColor = ConsoleColor.White;
            TypeWrite("Choose array size:\n\n1: 128\n2: 256\n3: 1024");
            int size = int.Parse(Console.ReadLine());

            switch (size)
            {
                case 1:
                    size = 128;
                    break;
                case 2:
                    size = 256;
                    break;
                case 3:
                    size = 1024;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{size} is not a valid choice, please use 1, 2 or 3");
                    Sleep(3000);
                    Console.Clear();
                    Main(new string[0]);
                    return;
            }

            string element;
            for (int i = 0; i < files.Length; i++)
            {
                element = files[i];
                element = element.Replace("[size]", Convert.ToString(size));
                files[i] = element;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            try
            {
                // I literally never use these again, theyre just here to make sure the script can actually load all of the files
                TypeWrite("Loading arrays...");
                double[] Close = Load($"Resources/{files[0]}");
                double[] Change = Load($"Resources/{files[1]}");
                double[] Open = Load($"Resources/{files[2]}");
                double[] High = Load($"Resources/{files[3]}");
                double[] Low = Load($"Resources/{files[4]}");
                double[] Merge = Load($"Resources/{files[5]}");
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
            TypeWrite($"Choose your array:\n1: {files[0]}\n2: {files[1]}\n3: {files[2]}\n4: {files[3]}\n5: {files[4]}\n6: {files[5]}");
            int chosen_file = Convert.ToInt32(Console.ReadLine()) - 1;
            if (chosen_file > 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{chosen_file} is not a valid choice, please use 1 - 6");
                Sleep(3000);
                Console.Clear();
                Main(new string[0]);
                return;

            }
            double[] Choice = new double[0]; //because the compiler will throw a tantrum without this
            try
            {
                Choice = Load($"Resources/{files[chosen_file]}");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Error loading Resources /{ files[chosen_file]}");
                Sleep(3000);
                Main(new string[0]);
            }
            Console.Title = $"Reading from {files[chosen_file]}";
            Menu(Choice, files, chosen_file);
        }

        private static void Menu(double[] Choice, string[] files, int chosen_file)
        {
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
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{mode} is not a valid choice, please use 1 or 2");
                    Sleep(3000);
                    Console.Clear();
                    Main(new string[0]);
                    return;
            }
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
                        Console.WriteLine($"{(i + 1).ToString("#000.##")}|  {tempAssend[i]}");
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
                        Console.WriteLine($"{(i + 1).ToString("#000.##")}| {Array[i]}");
                    }
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite("Thats not a valid choice");
                    Sleep(200);
                    DisplayArray(Array, files, chosen_file);
                    return;
            }
            Console.Write("Press any key to return to the main menu");
            Console.ReadKey();
            Menu(Array, files, chosen_file);
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
                            TypeWrite($"{searchItem} on line {i + 1}\n\n{i}. {Array[i - 1]}\n{i + 1}. {Array[i]} <---\n{i + 2}. {Array[i + 1]}");
                        }
                        catch
                        {
                            try
                            {
                                TypeWrite($"{searchItem} on line {i + 1}\n\n{i + 1}. {Array[i]} <---\n{i + 2}. {Array[i + 1]}\n{i + 3}. {Array[i + 2]}");
                            }
                            catch
                            {
                                TypeWrite($"{searchItem} on line {i + 1}\n\n{i - 1}. {Array[i - 2]}\n{i}. {Array[i - 1]}\n{i + 1}. {Array[i]} <---");

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
                try //avoids overflows and underflows when outputting
                {
                    TypeWrite($"\n{Index}. {Array[Index - 1]}\n{Index + 1}. {Array[Index]} <---\n{Index + 2}. {Array[Index + 1]}");
                }
                catch
                {
                    try
                    {
                        TypeWrite($"\n\n{Index + 1}. {Array[Index]} <---\n{Index + 2}. {Array[Index + 1]}\n{Index + 3}. {Array[Index + 2]}");
                    }
                    catch
                    {
                        TypeWrite($"\n{Index - 1}. {Array[Index - 2]}\n{Index}. {Array[Index - 1]}\n{Index + 1}. {Array[Index]} <---");

                    }
                }

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
