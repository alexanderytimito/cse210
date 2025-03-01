using System;
using static System.Console;

class Listing : Activities
{
    private List<string> prompts = new List<string>{
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?",
        };
    public Listing(int activity) : base()
    {
        ActivityIntro(activity);
    }

    private string GetRandomPrompt()
    {
        Random random = new Random();
        int randomPromptIndex = random.Next(prompts.Count);
        string randomPrompt = prompts[randomPromptIndex];
        return randomPrompt;
    }

    public void RunListing()
    {
        StartActivity();
        WriteLine("List as many responses as you can to the following prompt: ");
        WriteLine($"---{GetRandomPrompt()}---");
        Write("\nYou may begin in: ");
        CountDown(3);
        WriteLine("\n");
        SetTime();

        do
        {
            Write("> ");
            ReadLine();
        } while (!TimeOut());

        EndActivity();
    }
}