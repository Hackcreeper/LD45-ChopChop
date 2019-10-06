using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public int maxWidth = 200;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectTransform.sizeDelta = new Vector2(
            (float)maxWidth/health.maxHealth*health.Get(),
            _rectTransform.sizeDelta.y
        );
    }
}