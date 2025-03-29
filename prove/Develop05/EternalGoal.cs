using static System.Console;

public class EternalGoal : Goal
{

    public EternalGoal(string[] goal) : base(goal)
    {
    }

    public EternalGoal():base()
    {
    }

    override public int GetPoints()
    {
        return points;
    }
    override public string GetName()
    {
        return name;
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
        type = "EternalGoal";
    }
    override public void RunGoal()
    {
        GetGoal();
    }
}