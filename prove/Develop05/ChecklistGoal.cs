using static System.Console;

public class ChecklistGoal : Goal
{
    public int numberOfTimesForBonus;
    public int numberOfTimesCompleted;
    private int bonus;

    public bool CheckIfAccomplished()
    {
        numberOfTimesCompleted = numberOfTimesCompleted + 1;
        if (numberOfTimesCompleted < numberOfTimesForBonus)
        {
            return false;
        }
        else if(numberOfTimesCompleted == numberOfTimesForBonus)
            return true;
        else
            return false;
    }
    public ChecklistGoal(string[] goal) : base(goal)
    {
        numberOfTimesCompleted = int.Parse(goal[4]);
        numberOfTimesForBonus = int.Parse(goal[5]);
        bonus = int.Parse(goal[6]);
    }

    public ChecklistGoal():base()
    {}

    public int GetBonus()
    {
        return bonus;
    }
    private void  Bonus()
    {
        Write("How many times does this goal need to be accomplished for a bonus? ");
        numberOfTimesForBonus = int.Parse(ReadLine());
        Write("What is the bonus for accomplishing it that many times? ");
        bonus = int.Parse(ReadLine());
        WriteLine("");
    }

    override public int GetPoints()
    {
        return points;
    }
    public override string GetName()
    {
        return name;
    }
    public override string SaveGoal()
    {
        return $"{type}|{name}|{description}|{points}|{numberOfTimesCompleted}|{numberOfTimesForBonus}|{bonus}";
    }

    public override string DisplayGoal()
    {
        return $"[{check}] {name} ({description}) --- {numberOfTimesCompleted} times completed out of {numberOfTimesForBonus}";
    }

    override public void SetType()
    {
        type = "ChecklistGoal";
    }

    override public void RunGoal()
    {
        GetGoal();
        Bonus();       
    }
}