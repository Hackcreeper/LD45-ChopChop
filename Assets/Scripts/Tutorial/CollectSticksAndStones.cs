using Tutorial.Conditions;

namespace Tutorial
{
    public class CollectSticksAndStones : ITutorialStep
    {
        private readonly TutorialTask[] _tasks =
        {
            new TutorialTask("Collect 2 sticks from the ground ({{actual}} / {{max}})", new HasWood(2)),
            new TutorialTask("Collect one stone from the ground ({{actual}} / {{max}})", new HasStone(1))
        };

        public string GetInfo()
        {
            return "You can use the left mouse button to interact with stuff";
        }

        public TutorialTask[] GetTasks() => _tasks;
        
        public void OnStart()
        {
        }

        public void OnEnd()
        {
        }
    }
}