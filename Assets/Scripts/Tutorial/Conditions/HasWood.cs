namespace Tutorial.Conditions
{
    public class HasWood : ITaskCondition
    {
        private int _requires;

        public HasWood(int requires)
        {
            _requires = requires;
        }

        public string ReplaceVariables(string text)
        {
            return text
                .Replace("{{actual}}", Resources.Instance.Get(ResourceType.Wood).ToString())
                .Replace("{{max}}", _requires.ToString());
        }
        
        public bool IsComplete() => Resources.Instance.Get(ResourceType.Wood) >= _requires;
    }
}