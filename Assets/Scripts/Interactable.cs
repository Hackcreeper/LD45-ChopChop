using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float distance = 10;

    protected bool Focus;
    protected bool Active = true;

    public void SetFocus(bool focus)
    {
        Focus = focus;
    }

    public virtual bool IsActive() => Active;
}
