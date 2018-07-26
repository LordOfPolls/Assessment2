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

        static void QuickSort(Double[] array, int low, int high)
        {
            if (low < high)
            {
                double pivot = array[high];
                // index of smaller element
                int index = (low - 1);
                for (int j = low; j < high; j++)
                {
                    // If current element is smaller 
                    // than or equal to pivot
                    if (array[j] <= pivot)
                    {
                        index++;
                        // swap arr[i] and arr[j]
                        double temp = array[index];
                        array[index] = array[j];
                        array[j] = temp;
                    }
                }
                // swap arr[i+1] and arr[high] (or pivot)
                double temp1 = array[index + 1];
                array[index + 1] = array[high];
                array[high] = temp1;
                
                // Recursively sort elements before
                // partition and after partition
                QuickSort(array, low, index);
                QuickSort(array, index + 1, high);
            }
        }

        static void BubbleSort(Double[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = 0; j < array.Length - i - 1; j++)
                    if (array[j] <= array[j + 1])
                    {
                        // swap temp and arr[i]
                        double temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
        }


        public static int BinarySearch(double[] Array, double searchItem, int min, int max)
        {// A recursive binary Search method
            if (min > max) // if there are no values left
            {
                return -1; // return something that says there was no result
            }
            else
            {
                int mid = (min + max) / 2; // get the middle value
                if (searchItem == Array[mid]) 
                {// if result has been found
                    return mid; //return the ID
                }
                else if (searchItem < Array[mid])
                {//if search term is lower than the middle value
                    return BinarySearch(Array, searchItem, min, mid - 1);
                }   //tell the next loop to only look at the lower half
                else
                {//if search term is higher than the middle value
                    return BinarySearch(Array, searchItem, mid + 1, max);
                }   //tell the next loop to only look at the upper half
            }
        }

        public static int CustomSearch(double[] Array, double searchItem)
        {
            /*
             * Ok so basically what this big scary loop does is see how far away the current number is from the desired one
             * If it finds a number closer to the one it found last time, itll set that as the "bestDistance"
             * Whatever it finds as the "bestDistance" is set as the "index"
             * If it finds an exact match, itll output that straight away
             * If it cant find an exact match, itll output the value with the "bestDistance"
            */
            double bestDistance = 0;
            int Index = -1;
            double cDistance = 0;
            for (int i = 0; i < Array.Length; i++)
            {
                cDistance = Math.Abs(searchItem - Array[i]); // How far away is this number, from the last best we found
                if (Index == -1 || cDistance < bestDistance) // Is this value closer to our target than our last best? Or have we not run yet?
                {
                    bestDistance = cDistance; // Sets the minimum difference to the current 
                    Index = i; // The index of the current val
                    if (bestDistance == 0) // Found the target vakye
                    {
                        break;
                    }
                }
            }
            return Index;
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
            string[] files = { "Close_[size].txt", "Change_[size].txt", "Open_[size].txt", "High_[size].txt", "Low_[size].txt" }; // A fancy array holding all the files names so i never have to type them again
            Console.ForegroundColor = ConsoleColor.White; // Oooh more cosmetic stuff

            TypeWrite("Choose array size:\n\n1: 128\n2: 256\n3: 1024"); // User input junk
            int size;
            try
            {
               size = int.Parse(Console.ReadLine()); // Convert the user input string into an int so i can actually use it properly
            }
            catch {
                Console.ForegroundColor = ConsoleColor.Red; // Moar cosmetics
                Console.Write($"Thats not a valid choice, please use 1, 2 or 3");
                Sleep(3000);
                Console.Clear();
                Main(new string[0]); // Recall the method, theres probably a better way to restart it, but i dont really care, this works
                return;
            }
        
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
            {// loop that ammends the filenames from early to have the correct names, saving me creating 15 arrays/options
                element = files[i]; // get the entry
                element = element.Replace("[size]", Convert.ToString(size)); // Ammend that lazy array of file names from earlier to have the correct sizes in the names
                files[i] = element; // update the entry with a new value
            }
            // Task 6: Id prefer to do this like i did before, with a premerged file, but that wasnt allowed
            Console.Clear();
            TypeWrite("Merge close and high files?");
            string mergeQuery = (Console.ReadLine()).ToLower();
            if (mergeQuery.Contains('y'))
            {
                Double[] Array1 = Load($"Resources/{files[0]}"); // Load Close
                Double[] Array2 = Load($"Resources/{files[3]}"); // Load High
                Double[] Array = Array1.Concat(Array2).ToArray(); // Merge the two temp arrays
                try
                {
                    Menu(Array, files); // Shove the user into the action's menu
                }
                catch
                {
                    Console.Clear(); // Clears the screen
                    TypeWrite("Closing..."); // Tells the user the program is closing
                    Sleep(1000); // waits
                    Environment.Exit(0); // Exits
                }
            }
            else if (mergeQuery.Contains('n'))
            {// Lets the user choose a file
                Console.Clear();
                TypeWrite($"Choose your array:\n1: {files[0]}\n2: {files[1]}\n3: {files[2]}\n4: {files[3]}\n5: {files[4]}\n");
                int chosen_file;
                try
                {
                     chosen_file = Convert.ToInt32(Console.ReadLine()) - 1; // Get the users input and make it usable
                }
                catch
                {
                    // Handles the user inputting an invalid option
                    Console.ForegroundColor = ConsoleColor.Red; // Makes the text red
                    Console.Write($"Thats is not a valid choice, please use 1 - 6");
                    Sleep(3000); // Give the user chance to read
                    Console.Clear(); // Clear the screen
                    Main(new string[0]); // Restarts the method
                    return;
                }
                if (chosen_file > 6) // User input vallidation
                {
                    // Handles the user inputting an invalid option
                    Console.ForegroundColor = ConsoleColor.Red; // Makes the text red
                    Console.Write($"{chosen_file} is not a valid choice, please use 1 - 6");
                    Sleep(3000); // Give the user chance to read
                    Console.Clear(); // Clear the screen
                    Main(new string[0]); // Restarts the method
                    return;
                }
                else
                {
                    double[] Choice = Load($"Resources/{files[chosen_file]}");
                    Console.Title = $"Reading from {files[chosen_file]}"; // Cosmetic thingy magingy
                    try
                    {
                        Menu(Choice, files); // Shove the user into the action's menu
                    }
                    catch
                    {
                        Console.Clear(); // Clears the screen
                        TypeWrite("Closing..."); // Tells the user the program is closing
                        Sleep(1000); // waits
                       Environment.Exit(0); // Exits
                    }
                }
            }
            else
            {// Handles the user inputting an invalid option
                Console.ForegroundColor = ConsoleColor.Red; // Makes the text red
                TypeWrite($"{mergeQuery} is not a valid choice, please use (y)es or (n)o");
                Sleep(3000);// Give the user chance to read`
                Console.Clear(); // Clear the screen
                Main(new string[0]); // Restarts the method
                return;
            }


        }


        private static void Menu(double[] Choice, string[] files)
        {// The actions menu that lets the user choose if they want to display or search
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // Another cosmetic
            TypeWrite("Choose mode:\n\n1: Display\n2: Search");
            int mode = Convert.ToInt32(Console.ReadLine());
            switch (mode)
            {
                case 1:
                    DisplayArray(Choice, files); // Calls the the method that outputs the data in a nice format
                    break;
                case 2:
                    SearchArray(Choice, files); // Calls the search method
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

        static void DisplayArray(double[] Array, string[] files)
        { // Displays the users input in a sensible way
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White; // Cosmetic
            TypeWrite("Display mode:"); //These are all in seperate lines because my typewrite function wont output /n nicely
            TypeWrite("1: Assending");
            TypeWrite("2: Descending");
            TypeWrite("3: Raw");
            TypeWrite("4: Quicksort Assending");
            TypeWrite("5: Bubblesort Descending");

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
                case 4:
                    {
                        QuickSort(Array, 0, Array.Length - 1);
                        for (int i = 0; i < Array.Length; i++)
                        {
                            Console.WriteLine($"{(i + 1).ToString("#0000.##")}| {Array[i]}");
                        }
                        break;
                    }
                case 5:
                    {
                        BubbleSort(Array);
                        for (int i = 0; i < Array.Length; i++)
                        {
                            Console.WriteLine($"{(i + 1).ToString("#0000.##")}| {Array[i]}");
                        }
                        break;
                    }
                default: // User input validation
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeWrite("Thats not a valid choice");
                    Sleep(200);
                    DisplayArray(Array, files);
                    return;
            }
            Console.Write("Press any key to return to the main menu"); // Allow the user to return back to the menu
            Console.ReadKey();
            Menu(Array, files);
        }

        static void SearchArray(double[] Array, string[] files, int choice=0)
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
                SearchArray(Array, files);
                return;
            }

            int Index = -1;
            if (choice == 0) // if restarting method, dont ask again
            {
                Console.Clear();
                TypeWrite("Choose search mode:\n1)Binary Search\n2)Custom Search (finds nearest match too)");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            Console.Clear();
            TypeWrite($"Searching {Array.Length} items"); // Tell them whats going on
            Console.ForegroundColor = ConsoleColor.Green; // Reassure the user that its working
            switch (choice){
                case 1:
                    QuickSort(Array, 0, Array.Length - 1); //binary search only works on sorted arrays
                    Index = Convert.ToInt32(BinarySearch(Array, searchItem, 0, Array.Length-1));
                    break;
                case 2:
                    Index = CustomSearch(Array, searchItem);
                    break;
            }
            
            if (Index <= -1 && choice == 2) // it couldnt find an exact match
            { // this only works with my custom search because i couldnt make it work with a binary search
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
            }else if (Index <= -1 && choice == 1) // if no result, and binary search was used
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeWrite($"Unable to find {searchItem}"); // tell the user it couldnt find the result
            }
            else
            { // Search item was found
                Console.ForegroundColor = ConsoleColor.Green;
                TypeWrite("Sucess");
                try // avoids overflows and underflows when outputting
                { // This scary scary line outputs the result before the target, and after the target, to give a nicer output. completely unnecesary but why not make it look good
                    TypeWrite($"{searchItem} on line {Index + 1}\n\n{Index}. {Array[Index - 1]}\n{Index + 1}. {Array[Index]} <---\n{Index + 2}. {Array[Index + 1]}");
                }
                catch
                {
                    try // these are here just in case the value found is right at the end or start of the array and the above line would throw an error
                    {
                        TypeWrite($"{searchItem} on line {Index+ 1}\n\n{Index+ 1}. {Array[Index]} <---\n{Index+ 2}. {Array[Index+ 1]}\n{Index+ 3}. {Array[Index+ 2]}");
                    }
                    catch
                    {
                        TypeWrite($"{searchItem} on line {Index+ 1}\n\n{Index- 1}. {Array[Index- 2]}\n{Index}. {Array[Index- 1]}\n{Index+ 1}. {Array[Index]} <---");
                    }
                }
            }
            TypeWrite("search again? y or n"); 
            string reply = Console.ReadLine().ToLower();
            bool restart;
            if (reply == "y" || reply == "yes" || reply == "yeah") // allows the user to restart
                SearchArray(Array, files, choice); //restarts the method with all required data
            else
                Menu(Array, files); // returns the user to the menu

        }
    }
}
