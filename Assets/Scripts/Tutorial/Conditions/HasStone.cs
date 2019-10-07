namespace Tutorial.Conditions
{
    public class HasStone : ITaskCondition
    {
        private int _requires;

        public HasStone(int requires)
        {
            _requires = requires;
        }

        public string ReplaceVariables(string text)
        {
            return text
                .Replace("{{actual}}", Resources.Instance.Get(ResourceType.Stone).ToString())
                .Replace("{{max}}", _requires.ToString());
        }

        public bool IsComplete() => Resources.Instance.Get(ResourceType.Stone) >= _requires;
    }
}