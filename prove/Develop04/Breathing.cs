using static System.Console;

class Breathing : Activities
{
    public Breathing(int activity) : base()
    {
        ActivityIntro(activity);
    }

    private void BreathIn(int seconds)
    {
        
        Write("Breath in...");

        for(int i = seconds; i > 0; i--) {
            Write(i);
            Thread.Sleep(1000);
            Write("\b \b");
        }
    }
    private void BreathOut(int seconds)
    {
        Write("Breath out...");

        for(int i = seconds; i > 0; i--) {
            Write(i);
            Thread.Sleep(1000);
            Write("\b \b");
        }
    }
    public void RunBreathing()
    {
        StartActivity();
        SetTime();
        do
        {
            BreathIn(3);
            WriteLine("");
            BreathOut(4);
            WriteLine("\n");
        } while (!TimeOut());
        
        EndActivity();
        Clear();
        
    }
}