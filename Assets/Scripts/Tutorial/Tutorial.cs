using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class Tutorial : MonoBehaviour
    {
        public static Tutorial Instance { private set; get; }
        
        private readonly ITutorialStep[] _steps =
        {
            new CollectSticksAndStones(),
            new CraftAxe(),
            new ChopTrees(),
            new KillDrone(),
        };

        public Text stepText;
        public Text infoText;
        public Transform tasksContainer;
        public GameObject taskPrefab;
        public GameObject tutorialDrone;
        public GameObject tutorialFinished;

        private int _currentStep;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _steps[_currentStep].OnStart();
        }
        
        private void Update()
        {
            if (_currentStep == _steps.Length)
            {
                gameObject.SetActive(false);
                return;
            }

            EnableStep();

            if (_steps[_currentStep].GetTasks().All(task => task.IsComplete()))
            {
                _steps[_currentStep].OnEnd();
                _currentStep++;
                _steps[_currentStep].OnStart();
            }
        }

        private void EnableStep()
        {
            var step = _steps[_currentStep];

            stepText.text = $"{_currentStep + 1} / {_steps.Length}";
            infoText.text = step.GetInfo();

            for (var i = 0; i < tasksContainer.childCount; i++)
            {
                Destroy(tasksContainer.GetChild(i).gameObject);
            }

            foreach (var task in step.GetTasks())
            {
                var obj = Instantiate(taskPrefab, tasksContainer, false);
                obj.GetComponentInChildren<Text>().text = task.GetText();
                obj.transform.Find("Checkbox").Find("Checked").GetComponent<Image>().enabled = task.IsComplete();
            }
        }

        public void Finish()
        {
            gameObject.SetActive(false);
            tutorialFinished.gameObject.SetActive(true);
            DayNight.Instance.Activate();
        }
    }
}