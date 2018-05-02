/*
Name: Assessment2
Author: Daniel Bearman
Creation: 18/03/2018
State: COMPLETE
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
        {// Completely cosmetic effect to make the UI more interesting and less... sterile
            string T = text.ToString(); // Convert whatever was passed into a string
            for (int i = 0; i < T.Length; i++)
            {
                Console.Write(T[i]); // ouput 1 character
                System.Threading.Thread.Sleep(new Random().Next(10, 20)); // wait for a few milliseconds
            }
            Console.Write("\n"); // Newline to mimic Console.WriteLine behaviour
        }

        static void Sleep(int time) 
        {// Sleep function so i dont have to always type out the line below whenever i want the program to wait
            try
            {
                System.Threading.Thread.Sleep(time);
            }
            catch
            {
                Console.Write("Sleep command error"); // i mean... i doubt this will ever happen... but just in case?
                return;
            }
        }

        public static void Quicksort(IComparable[] elements)
        {// Woo a quicksort method i never used
            int left = 0;
            int right = elements.Length - 1;
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];
            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }
                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }
                if (i <= j)
                {
                    IComparable tmp = elements[i];// Swap

                    elements[i] = elements[j];
                    elements[j] = tmp;
                    i++;
                    j--;
                }
            }
            // Recursive calls
            if (left < j)
            {
                Quicksort(elements);
            }

            if (i < right)
            {
                Quicksort(elements);
            }
        }

        static double[] Load(string file)
        {// Helper function to allow me to load files, and convert them to an array with a single line of code
            try
            {
                string[] f = System.IO.File.ReadAllLines(@file);
                Double[] x = Array.ConvertAll(f, Double.Parse);
                return (x);
            }
            catch // Error handling so i dont need to worry about adding it everywhere in my code whenever i want to load a file 
            {
                Console.Write($"CRITICAL ERROR READING FILE {file}");
                Sleep(10000);
                Environment.Exit(0);
            }
            return (new double[10]);// DumbyLineToAllowFunction 
        }

        private static void Main(string[] args)
        {
            Console.CursorVisible = false; // Gets rid of that annoying blinky line thats in console
            Console.Title = "Boot..."; // Cosmetic thing
            Console.ForegroundColor = ConsoleColor.Cyan; // More cosmetics
            string[] files = { "Close_[size].txt", "Change_[size].txt", "Open_[size].txt", "High_[size].txt", "Low_[size].txt", "Merge_[size].txt" }; // A fancy array holding all the files names so i never have to type them again
            Console.ForegroundColor = ConsoleColor.White; // Oooh more cosmetic stuff
            TypeWrite("Choose array size:\n\n1: 128\n2: 256\n3: 1024"); // User input junk
            int size = int.Parse(Console.ReadLine()); // Convert the user input string into an int so i can actually use it properly

            switch (size) // a switch statement because nested Ifs are ugly in code
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
                    Console.ForegroundColor = ConsoleColor.Red; // Moar cosmetics
                    Console.Write($"{size} is not a valid choice, please use 1, 2 or 3");
                    Sleep(3000);
                    Console.Clear();
                    Main(new string[0]); // Recall the method, theres probably a better way to restart it, but i dont really care, this works
                    return;
            }

            string element; // The compiler cries if i dont define this outside of a loop ¯\_(ツ)_/¯
            for (int i = 0; i < files.Length; i++)
            {
                element = files[i];
                element = element.Replace("[size]", Convert.ToString(size)); // Ammend that lazy array of file names from earlier to have the correct names
                files[i] = element;
            }

            Console.ForegroundColor = ConsoleColor.Cyan; // You guessed it, more cosmetic stuff
            try
            {
                // I literally never use these again, theyre just here to make sure the script can actually load all of the files without errors
                TypeWrite("Loading arrays..."); // This is here for like half a second, but who cares, it makes the code look sexy
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

            TypeWrite("Arrays Loaded"); // Debug thingy for me, that i ended up liking so its in the finished code
            Sleep(1000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // MOAR COSMETIC STUFF
            TypeWrite($"Choose your array:\n1: {files[0]}\n2: {files[1]}\n3: {files[2]}\n4: {files[3]}\n5: {files[4]}\n6: {files[5]}");
            int chosen_file = Convert.ToInt32(Console.ReadLine()) - 1; // Get the users input and make it usable
            if (chosen_file > 6) // User input vallidation
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{chosen_file} is not a valid choice, please use 1 - 6");
                Sleep(3000);
                Console.Clear();
                Main(new string[0]);
                return;

            }
            else
            {
                double[] Choice = Load($"Resources/{files[chosen_file]}");

                Console.Title = $"Reading from {files[chosen_file]}"; // Cosmetic thingy magingy
                Menu(Choice, files, chosen_file); // Shove the user into the action's menu
            }
        }

        private static void Menu(double[] Choice, string[] files, int chosen_file)
        {// The actions menu that lets the user choose if they want to display or search
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // Another cosmetic
            TypeWrite("Choose mode:\n\n1: Display\n2: Search");
            int mode = Convert.ToInt32(Console.ReadLine());
            switch (mode)
            {
                case 1:
                    DisplayArray(Choice, files, chosen_file); // Calls the the method that outputs the data in a nice format
                    break;
                case 2:
                    SearchArray(Choice, files, chosen_file); // Calls the search method
                    break;
                default: // User input validation
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{mode} is not a valid choice, please use 1 or 2");
                    Sleep(3000);
                    Console.Clear();
                    Main(new string[0]);
                    return;
            }
        }

        static void DisplayArray(double[] Array, string[] files, int chosen_file)
        { // Displays the users input in a sensible way
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // Cosmetic
            TypeWrite($"{files[chosen_file]} Selected"); // Tells the user what file its reading from
            TypeWrite("Display mode:"); //These are all in seperate lines because my typewrite function wont output /n nicely
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
                    double[] tempAssend = Array.OrderBy(c => c).ToArray(); // Sort the array using linQ and array.orderby
                    for (int i = 0; i < tempAssend.Length; i++) 
                    {
                        // #0000.## = output to 4 significant figures so the output stays inline
                        // so {(i + 1).ToString("#0000.##")} outputs 0001 when i is 1
                        Console.WriteLine($"{(i + 1).ToString("#0000.##")}|  {tempAssend[i]}");
                    }
                    break;
                case 2:
                    double[] tempDescend = Array.OrderByDescending(c => c).ToArray();
                    for (int i = 0; i < tempDescend.Length; i++)
                    {
                        Console.WriteLine($"{(i + 1).ToString("#0000.##")}|  {tempDescend[i]}");
                    }
                    break;
                case 3:
                    for (int i = 0; i < Array.Length; i++)
                    {
                        Console.WriteLine($"{(i + 1).ToString("#0000.##")}| {Array[i]}");
                    }
                    break;
                default: // User input validation
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite("Thats not a valid choice");
                    Sleep(200);
                    DisplayArray(Array, files, chosen_file);
                    return;
            }
            Console.Write("Press any key to return to the main menu"); // Allow the user to return back to the menu
            Console.ReadKey();
            Menu(Array, files, chosen_file);
        }

        static void SearchArray(double[] Array, string[] files, int chosen_file)
        {
            Console.Clear();
            Console.ResetColor(); // Cosmetics

            double searchItem = 0;
            TypeWrite("What would you like to search for? ");
            try // handle an error if the user decides to input ascii instead of an int
            {
                searchItem = Double.Parse(Console.ReadLine()); //take the users input and convert it into a double precision float

            }
            catch
            { // Handle the resulting error and restart the function
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWrite("Invalid search, search must be an int");
                Sleep(4000);
                SearchArray(Array, files, chosen_file);
                return;
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green; // Reassure the user that its working
            TypeWrite($"Searching {Array.Length} items"); // Tell them whats going on
            bool found = false; // helps me out later so i can keep the code cleaner. when this is true, the term has been found
            Console.Clear();
            double bestDistance = 0; // The minimum difference
            int Index = -1; // an index can never be a negative int, so this acts as a way of showing the script ran

            /*
             * Ok so basically what this big scary loop does is see how far away the current number is from the desired one
             * If it finds a number closer to the one it found last time, itll set that as the "bestDistance"
             * Whatever it finds as the "bestDistance" is set as the "index"
             * If it finds an exact match, itll output that straight away
             * If it cant find an exact match, itll output the value with the "bestDistance"
            */
            for (int i = 0; i < Array.Length; i++)
            {
                var cDistance = Math.Abs(searchItem - Array[i]); // How far away is this number, from the last best we found
                if (Index == -1 || cDistance < bestDistance) // Is this value closer to our target than our last best? Or have we not run yet?
                {
                    bestDistance = cDistance; // Sets the minimum difference to the current 
                    Index = i; // The index of the current val
                    if (bestDistance == 0) // Found the target vakye
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        found = true;
                        TypeWrite("Sucess"); 

                        try // avoids overflows and underflows when outputting
                        { // This scary scary line outputs the result before the target, and after the target, to give a nicer output. completely unnecesary but why not make it look good
                            TypeWrite($"{searchItem} on line {i + 1}\n\n{i}. {Array[i - 1]}\n{i + 1}. {Array[i]} <---\n{i + 2}. {Array[i + 1]}");
                        }
                        catch
                        {
                            try // these are here just in case the value found is right at the end or start of the array and the above line would throw an error
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

            if (found == false) // it couldnt find an exact match
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWrite($"Unable to find {searchItem}"); 
                TypeWrite($"Nearest value was {Array[Index]} on line {Index + 1}");
                try // avoids overflows and underflows when outputting
                {// This scary scary line outputs the result before the value, and after the target, to give a nicer output. completely unnecesary but why not make it look good
                    TypeWrite($"\n{Index}. {Array[Index - 1]}\n{Index + 1}. {Array[Index]} <---\n{Index + 2}. {Array[Index + 1]}");
                }
                catch
                {
                    try // these are here just in case the value found is right at the end or start of the array and the above line would throw an error
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
            string reply = Console.ReadLine().ToLower();
            bool restart;
            if (reply == "y" || reply == "yes" || reply == "yeah") // allows the user to restart
                SearchArray(Array, files, chosen_file); //restarts the method with all required data
            else
                Menu(Array, files, chosen_file); // returns the user to the menu

        }
    }
}
