using System;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public string displayName;
    public UpgradeType type;
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