using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScoreRenderer : MonoBehaviour
    {
        private Text _text;

        private void Start()
        {
            _text = GetComponent<Text>();
        }

        public void Update()
        {
            _text.text = $"{DayNight.Instance.GetNightsSurvived()} nights survived";
        }
    }
}
