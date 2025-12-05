using static System.Console;

public class SimpleGoal : Goal
{

    public SimpleGoal(string[] goal) : base(goal)
    {
    }

    public SimpleGoal():base()
    {}

    
    override public string GetName()
    {
        return name;
    }

    override public int GetPoints()
    {
        return points;
    }
    override public string SaveGoal()
    {
        return $"{type}|{name}|{description}|{points}";
    }
    override public string DisplayGoal()
    {
        return $"[{check}] {name} ({description})";
    }
    override public void SetType()
    {
        type = "SimpleGoal";
    }
    override public void RunGoal()
    {
        GetGoal();
    }
}