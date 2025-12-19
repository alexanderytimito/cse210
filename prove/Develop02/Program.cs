
class Program
{
    static void Main()
    {
        string filePath = "journal.txt"; 
        Journal myJournal;

        
        if (File.Exists(filePath))
        {
            string[] importedEntries = File.ReadAllLines(filePath);
            myJournal = new Journal(importedEntries);
            Console.WriteLine("Journal loaded successfully!\n");
        }
        else
        {
            myJournal = new Journal();
            Console.WriteLine("No existing journal found. Starting a new journal.\n");
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter prompt: ");
                    string prompt = Console.ReadLine();

                    Console.Write("Enter response: ");
                    string response = Console.ReadLine();

                    string date = DateTime.Now.ToString("yyyy-MM-dd");
                    myJournal.AddEntry(prompt, response, date);

                    Console.WriteLine("Entry added!\n");
                    break;

                case "2":
                    Console.WriteLine("Journal Entries:\n");
                    myJournal.DisplayJournal();
                    break;

                case "3":
                    File.WriteAllLines(filePath, myJournal.ExportJournal());
                    Console.WriteLine("Journal saved successfully!\n");
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.\n");
                    break;
            }
        }
    }
}
