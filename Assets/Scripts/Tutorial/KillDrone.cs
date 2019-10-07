using System.Globalization;
using Tutorial.Conditions;

namespace Tutorial
{
    public class KillDrone : ITutorialStep
    {
        private readonly TutorialTask[] _tasks =
        {
            new TutorialTask("Kill a drone", new HasWood(200))
        };

        public string GetInfo() =>
            "During the day you can build and collect resources, but during the night you need to defend your home!";

        public TutorialTask[] GetTasks() => _tasks;

        public void OnStart()
        {
            DayNight.Instance.MakeNight();
            Tutorial.Instance.tutorialDrone.SetActive(true);
        }

        public void OnEnd()
        {
            
        }
    }
}