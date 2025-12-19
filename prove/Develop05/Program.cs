using System;
using System.Collections.Generic;
using static System.Console;

class Program
{
    static List<Goal> goals = new List<Goal>();
    static FileManager fileManager = new FileManager();

    static void Main()
    {
        bool running = true;

        while (running)
        {
            Clear();
            WriteLine("=== Goal Tracker Program ===\n");
            WriteLine("1. Create a new goal");
            WriteLine("2. List goals");
            WriteLine("3. Record goal completion");
            WriteLine("4. Save goals");
            WriteLine("5. Load goals");
            WriteLine("6. Exit");
            Write("\nEnter your choice: ");
            string choice = ReadLine();

            Clear();
            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    RecordCompletion();
                    break;
                case "4":
                    SaveGoals();
                    break;
                case "5":
                    LoadGoals();
                    break;
                case "6":
                    running = false;
                    WriteLine("Exiting Goal Tracker. Goodbye!");
                    break;
                default:
                    WriteLine("Invalid choice. Press Enter to continue.");
                    ReadLine();
                    break;
            }
        }
    }

    static void CreateGoal()
    {
        WriteLine("Choose Goal Type:");
        WriteLine("1. Simple Goal");
        WriteLine("2. Eternal Goal");
        WriteLine("3. Checklist Goal");
        Write("\nEnter choice: ");
        string choice = ReadLine();

        Goal goal = choice switch
        {
            "1" => new SimpleGoal(),
            "2" => new EternalGoal(),
            "3" => new ChecklistGoal(),
            _ => null
        };

        if (goal == null)
        {
            WriteLine("Invalid choice. Press Enter to continue.");
            ReadLine();
            return;
        }

        goal.SetType();
        goal.RunGoal();
        goals.Add(goal);
        WriteLine("Goal created! Press Enter to continue.");
        ReadLine();
    }

    static void ListGoals()
    {
        WriteLine("=== Goals ===\n");
        if (goals.Count == 0)
        {
            WriteLine("No goals available.");
        }
        else
        {
            for (int i = 0; i < goals.Count; i++)
            {
                WriteLine($"{i + 1}. {goals[i].DisplayGoal()}");
            }
        }
        WriteLine("\nPress Enter to continue.");
        ReadLine();
    }

    static void RecordCompletion()
    {
        if (goals.Count == 0)
        {
            WriteLine("No goals to complete. Press Enter to continue.");
            ReadLine();
            return;
        }

        ListGoals();
        Write("\nEnter goal number to record completion: ");
        if (int.TryParse(ReadLine(), out int choice) && choice > 0 && choice <= goals.Count)
        {
            Goal g = goals[choice - 1];

            if (g is ChecklistGoal cg)
            {
                bool accomplished = cg.CheckIfAccomplished();
                WriteLine(accomplished 
                    ? $"Goal accomplished! Bonus: {cg.GetBonus()}" 
                    : "Goal progress recorded.");
            }
            else
            {
                g.check = '✓';
                WriteLine("Goal completed!");
            }
        }
        else
        {
            WriteLine("Invalid choice.");
        }
        ReadLine();
    }

    static void SaveGoals()
    {
        Write("Enter filename to save goals: ");
        string filename = ReadLine();
        fileManager.SaveGoals(filename, goals);
        WriteLine("Press Enter to continue.");
        ReadLine();
    }

    static void LoadGoals()
    {
        Write("Enter filename to load goals: ");
        string filename = ReadLine();
        goals = fileManager.LoadGoals(filename);
        WriteLine("Press Enter to continue.");
        ReadLine();
    }
}
