using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ModeRenderer : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        public void Update()
        {
            _text.text = DayNight.Instance.IsDay() ? "Build mode" : "Fight mode";
        }
    }
}