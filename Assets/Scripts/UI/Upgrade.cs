using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UI
{
    public class Upgrade : Interactable
    {
        public UpgradeData data;
        public UpgradeWindow upgradeWindow;

        public void Update()
        {
            if (!Focus || !Input.GetMouseButtonDown(0) || !IsActive())
            {
                return;
            }

            if (!CheckCosts())
            {
                return;
            }

            RemoveResources();
            HandleUpgrade();
            RemoveOldUpgrade();
            UnlockNewUpgrades();
        }

        private void UnlockNewUpgrades()
        {
            upgradeWindow.upgrades.AddRange(data.unlocks);
            upgradeWindow.ReRenderUpgrades();
        }

        private void RemoveOldUpgrade()
        {
            upgradeWindow.upgrades.Remove(data);
        }

        private void HandleUpgrade()
        {
            switch (data.type)
            {
                case UpgradeType.Axe:
                    Player.Instance.GainAxe();
                    break;
                default:
                    Debug.LogError("Upgrade not implemented!");
                    break;
            }
        }

        private bool CheckCosts() => data.costs.All(cost => Resources.Instance.Get(cost.type) >= cost.amount);

        private void RemoveResources()
        {
            foreach (var cost in data.costs)
            {
                Resources.Instance.Sub(cost.type, cost.amount);
            }
        }
    }
}

public enum UpgradeType
{
    Axe
}