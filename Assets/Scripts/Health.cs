using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    
    private int _amount = 10;

    private void Start()
    {
        _amount = maxHealth;
    }

    public void Sub(int amount = 1)
    {
        _amount -= amount;
    }

    public int Get() => _amount;
}