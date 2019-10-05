using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Collectable : MonoBehaviour
{
    public ResourceType type;

    public int amount = 1;
    
    private void OnMouseDown()
    {
        Resources.Instance.Add(type, amount);
        Destroy(gameObject);
    }
}
