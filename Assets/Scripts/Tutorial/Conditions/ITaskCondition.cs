namespace Tutorial.Conditions
{
    public interface ITaskCondition
    {
        string ReplaceVariables(string text);
        bool IsComplete();
    }
}