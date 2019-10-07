namespace Tutorial
{
    public interface ITutorialStep
    {
        string GetInfo();
        TutorialTask[] GetTasks();

        void OnStart();
        void OnEnd();
    }
}