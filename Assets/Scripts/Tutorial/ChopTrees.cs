using Tutorial.Conditions;

namespace Tutorial
{
    public class ChopTrees : ITutorialStep
    {
        private readonly TutorialTask[] _tasks =
        {
            new TutorialTask("Buy the wooden house upgrade", new HasWoodenHouse())
        };

        public string GetInfo() => "Chop 2 trees to get 20 wood!";
        public TutorialTask[] GetTasks() => _tasks;
        
        public void OnStart()
        {
        }

        public void OnEnd()
        {
        }
    }
}