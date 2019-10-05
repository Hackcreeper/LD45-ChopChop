using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    
    private int _amount = 10;

    private void Start()
    {
        _amount = maxHealth;
    }

    public void Sub(int amount)
    {
        _amount -= amount;
    }

    public int Get() => _amount;
}