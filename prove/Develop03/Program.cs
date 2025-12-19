using System;

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture();
        bool allHidden = false;

        Console.WriteLine("Welcome to the Scripture Memorization Program!\n");

        while (!allHidden)
        {
            Console.Clear();
            Console.WriteLine("Reference: " + scripture.GetFullReference() + "\n");
            Console.WriteLine(scripture.GetVisibleWords());
            Console.WriteLine("\nPress ENTER to hide more words, or type 'quit' to exit.");
            
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); // hide 3 random words at a time
            allHidden = scripture.AllWordsHidden;
        }

        Console.Clear();
        Console.WriteLine("Reference: " + scripture.GetFullReference() + "\n");
        Console.WriteLine(scripture.GetVisibleWords());
        Console.WriteLine("\nAll words are now hidden. Good job memorizing!");
    }
}
