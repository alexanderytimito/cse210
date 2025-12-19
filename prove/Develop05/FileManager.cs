using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

class FileManager
{
    // Save goals to a file
    public void SaveGoals(string filename, List<Goal> goals)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(filename);
            foreach (Goal g in goals)
            {
                writer.WriteLine(g.SaveGoal());
            }
            WriteLine("Goals saved successfully!");
        }
        catch (Exception ex)
        {
            WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    // Load goals from a file and return a list
    public List<Goal> LoadGoals(string filename)
    {
        List<Goal> goals = new List<Goal>();

        if (!File.Exists(filename))
        {
            WriteLine("File does not exist.");
            return goals;
        }

        try
        {
            foreach (string line in File.ReadAllLines(filename))
            {
                string[] parts = line.Split('|');
                Goal g = parts[0] switch
                {
                    "SimpleGoal" => new SimpleGoal(parts),
                    "EternalGoal" => new EternalGoal(parts),
                    "ChecklistGoal" => new ChecklistGoal(parts),
                    _ => null
                };

                if (g != null)
                    goals.Add(g);
            }
            WriteLine("Goals loaded successfully!");
        }
        catch (Exception ex)
        {
            WriteLine($"Error loading goals: {ex.Message}");
        }

        return goals;
    }
}
