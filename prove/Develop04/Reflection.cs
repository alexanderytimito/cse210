using System;
using static System.Console;


class Reflection : Activities
{
    private List<string> prompts = new List<string>{
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless.",
        };
    private List<string> questions = new List<string>{
            "Why was this experience meaningful to you?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "Have you ever done anything like this before?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?",
        };

    public Reflection(int activity) : base()
    {
        ActivityIntro(activity);
    }
    private string GetRandomPrompt()
    {
        Random random = new Random();
        int randomPromptIndex = random.Next(prompts.Count);
        string randomPrompt = prompts[randomPromptIndex];
        prompts.RemoveAt(randomPromptIndex);
        return randomPrompt;
    }
    private string GetRandomQuestion()
    {
        Random random = new Random();
        int randomQuestionIndex = random.Next(questions.Count);
        string randomQuestion = questions[randomQuestionIndex];
        questions.RemoveAt(randomQuestionIndex);
        return randomQuestion;
    }
    

    public void RunReflection()
    {
        StartActivity();
        WriteLine("\nConsider the following prompt:");
        Thread.Sleep(1000);
        WriteLine($"\n--- {GetRandomPrompt()} ---");
        WriteLine("\nWhen you have something in mind, press enter to continue...");
        ReadKey();
        WriteLine("Now, ponder on each of the following questions as they relate to this experience.");
        Write("You may begin in...");
        CountDown(5);
        SetTime();
        Clear();

        do
        {
            Write("> ");
            Write(GetRandomQuestion());
            Animation(10);
            WriteLine("");
        } while (!TimeOut());

        EndActivity();
        Clear();
    }
}