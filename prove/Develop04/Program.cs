using System;
using static System.Console;

class Program
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            Clear();
            WriteLine("=== Mindfulness Activities Program ===\n");
            WriteLine("Choose an activity to begin:");
            WriteLine("1. Breathing Activity");
            WriteLine("2. Reflecting Activity");
            WriteLine("3. Listing Activity");
            WriteLine("4. Exit\n");
            Write("Enter your choice: ");

            string choice = ReadLine();
            Clear();

            switch (choice)
            {
                case "1":
                    Breathing breathing = new Breathing(1); // 1 = Breathing
                    breathing.RunBreathing();
                    break;

                case "2":
                    Reflection reflection = new Reflection(2); // 2 = Reflecting
                    reflection.RunReflection();
                    break;

                case "3":
                    Listing listing = new Listing(3); // 3 = Listing
                    listing.RunListing();
                    break;

                case "4":
                    running = false;
                    WriteLine("Thank you for using the Mindfulness Program. Goodbye!");
                    break;

                default:
                    WriteLine("Invalid choice. Press Enter to try again.");
                    ReadLine();
                    break;
            }
        }
    }
}
