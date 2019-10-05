using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources Instance { private set; get; }

    private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
    
    private void Awake()
    {
        Instance = this;
    }

    public int Get(ResourceType type)
    {
        if (!_resources.ContainsKey(type))
        {
            _resources.Add(type, 0);
        }
        
        return _resources[type];
    }

    public void Add(ResourceType type, int amount)
    {
        _resources[type] = Get(type) + amount;
    }
}

public enum ResourceType
{
    Wood,
    Stone,
    Oil,
    Bone
}
