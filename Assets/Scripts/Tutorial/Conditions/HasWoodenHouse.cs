namespace Tutorial.Conditions
{
    public class HasWoodenHouse : ITaskCondition
    {
        public string ReplaceVariables(string text)
        {
            return text;
        }

        public bool IsComplete()
        {
            return Base.Instance.GetLevel() == BaseLevel.WoodenHouse;
        }
    }
}