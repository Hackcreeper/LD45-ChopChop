using Tutorial.Conditions;

namespace Tutorial
{
    public class TutorialTask
    {
        private string _text;
        private ITaskCondition _condition;
        
        public TutorialTask(string text, ITaskCondition condition)
        {
            _text = text;
            _condition = condition;
        }

        public string GetText()
        {
            return _condition.ReplaceVariables(_text);
        }

        public bool IsComplete()
        {
            return _condition.IsComplete();
        }
    }
}