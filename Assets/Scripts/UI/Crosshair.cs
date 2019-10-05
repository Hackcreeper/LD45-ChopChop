using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        public static Crosshair Instance { private set; get; }

        private CursorState _state = CursorState.Normal;
        private Image _cursor;
    
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _cursor = GetComponent<Image>();
        }

        public void SetState(CursorState state)
        {
            _state = state;
            RedrawCursor();
        }

        private void RedrawCursor()
        {
            var color = Color.white;
            if (_state == CursorState.HoveringOverInteractable)
            {
                color = Color.red;
            }

            _cursor.color = color;
        }
    }
}

public enum CursorState
{
    Normal,
    HoveringOverInteractable
}
