namespace Tutorial.Conditions
{
    public class HasAxe : ITaskCondition
    {
        public string ReplaceVariables(string text)
        {
            return text;
        }

        public bool IsComplete()
        {
            return Player.Instance.HasAxe();
        }
    }
}