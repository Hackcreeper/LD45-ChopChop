using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DayNightViewer : MonoBehaviour
    {
        private Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            _text.text = DayNight.Instance.IsDay()
                ? "Day"
                : "Night";
        }
    }
}