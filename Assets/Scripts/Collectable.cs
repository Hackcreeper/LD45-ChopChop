public class Collectable : Interactable
{
    public ResourceType type;

    public int amount = 1;
    
    private void OnMouseDown()
    {
        Resources.Instance.Add(type, amount);
        Destroy(gameObject);
    }
}
