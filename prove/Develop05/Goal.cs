using static System.Console;

public abstract class Goal
{
    protected string type;
    protected string name;
    protected string description;
    protected int points;
    public char check = ' '; //\u2713
   
 
    public Goal()
    {
    }
    protected Goal(string [] goal)
    {
        type = goal[0];
        name = goal[1];
        description = goal[2];
        points = int.Parse(goal[3]);
    }
    protected void GetGoal()
    {
        Write("What is the name of your goal? ");
        name = ReadLine();
        Write("What is the short description of it? ");
        description = ReadLine();
        Write("How many points associated with this goal? ");
        points = int.Parse(ReadLine());
        WriteLine("");
    }
    public string GetGoalType()
    {
        return type;
    }
   
    abstract public int GetPoints();
    abstract public string GetName();
    abstract public string SaveGoal(); 
    abstract public string DisplayGoal();
    abstract public void SetType();
    abstract public void RunGoal();
}