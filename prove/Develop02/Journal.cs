using System;
using System.ComponentModel;
using static System.Console;

class Journal
{
    private  List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public Journal(string[] importEntries)
    {
        entries = new List<Entry>();
        foreach (var _entry in importEntries) {
            var entry  = new Entry(_entry);
            entries.Add(entry);
        }
    }

    public void DisplayJournal() 
    {
        foreach (var entry in entries) {
            WriteLine(entry.DisplayEntries()); 
        }
    }

    public string[] ExportJournal()
    {
        var exportEntries = new List<string>();
        foreach (var entry in entries) {
            exportEntries.Add(entry.ExportEntries());
        }
        return exportEntries.ToArray();
    }

    public void AddEntry(string prompt, string response, string date)
    {
        var entry = new Entry(prompt, response, date);
        entries.Add(entry);
    }


}
