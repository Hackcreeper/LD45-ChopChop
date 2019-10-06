using System;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public string displayName;
    public string uniqueIdentifier;
    public UpgradeData[] unlocks;
    public UpgradeCosts[] costs;
    public Sprite icon;
}

[Serializable]
public struct UpgradeCosts
{
    public ResourceType type;
    public int amount;
}