class Entry
{
    private string prompt;
    private string response;
    private string date;

    public Entry(string prompt, string response, string date)
    {
        this.prompt = prompt;
        this.response = response;
        this.date = date;
    }

    public Entry(string import)
    {
        var parts = import.Split("|");
        this.date = parts[0];
        this.prompt = parts[1];
        this.response = parts[2];
    }
    public string ExportEntries()
    {
        return $"{date}|{prompt}|{response}|";
    }

    public string DisplayEntries()
    {
        return $"{date}: {prompt}\n{response}\n";
    }
}
 