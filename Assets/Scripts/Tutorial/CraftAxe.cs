using Tutorial.Conditions;

namespace Tutorial
{
    public class CraftAxe : ITutorialStep
    {
        private readonly TutorialTask[] _tasks =
        {
            new TutorialTask("Craft an axe", new HasAxe())
        };

        public string GetInfo()
        {
            return "You can craft anything in the middle of the map.";
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