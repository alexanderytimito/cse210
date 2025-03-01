using static System.Console;

class Activities
{
    private string activity;
    private int duration;
    private DateTime currentTime;
    private DateTime activityEndTime;

    protected void ActivityIntro(int activity)
    {
        switch (activity)
        {
            case 1:
                this.activity = "Breathing";
                WriteLine($"Welcome to {this.activity} Activity\n");
                WriteLine("This activity will help you relax by walking  you through breathing in and out slowly.");
                WriteLine("Clear your mind and foucus on your breathing.");
                break;
            case 2:
                this.activity = "Reflecting";
                WriteLine($"Welcome to {this.activity} Activity\n");
                WriteLine("This activity will help you reflect on times on your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.");
                break;
            case 3:
                this.activity = "Listing";
                WriteLine($"Welcome to {this.activity} Activity\n");
                WriteLine("This activity will help you reflect on the good things in your lfe by having you list as many thinghs as you can in a certain area");
                break;
            case 4:
                break;
            default:
                break;

        }
    }

    private void GetDuration()
    {
        Write("\n\nHow long, in seconds, would you like for your session? ");
        duration = int.Parse(ReadLine());
    }

    protected void SetTime()
    {
        activityEndTime = DateTime.Now.AddSeconds(duration);
    }
    protected bool TimeOut()
    {
        bool noTime;
        
        currentTime = DateTime.Now;
        if (currentTime < activityEndTime)
            noTime = false;
        else
            noTime = true;
        return noTime;
    }

    protected void Animation(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Write(".");
            Thread.Sleep(250);
            Write("\b \b");
            Write("..");
            Thread.Sleep(250);
            Write("\b\b  \b\b");
            Write("...");
            Thread.Sleep(250);
            Write("\b\b\b   \b\b\b");
            Write(" ");
            Thread.Sleep(250);
            Write("\b \b");
        }
    }
    protected void CountDown(int seconds)
    {
        for(int i = seconds; i > 0; i--) {
            Write(i);
            Thread.Sleep(1000);
            Write("\b \b");
        }
    }

    protected void StartActivity()
    {
        GetDuration();
        Clear();
        WriteLine("\nGet Ready\n\n");
        Animation(5);
        Clear();
    }

    protected void EndActivity()
    {
        WriteLine("\nWell Done\n");
        Animation(5);
        WriteLine($"You have completed another {duration} seconds of {activity} activity!!\n");
        Animation(5);
    }
}
