using System.Collections;
using System.Linq;
using UnityEngine;

namespace UI
{
    public class Upgrade : Interactable
    {
        public UpgradeData data;
        public UpgradeWindow upgradeWindow;

        // Analytics
        public UpgradeType type;

        public void Update()
        {
            if (data)
            {
                type = data.type;
            }

            if (!Focus || !Input.GetMouseButtonDown(0) || !IsActive())
            {
                return;
            }

            if (!CheckCosts())
            {
                upgradeWindow.infoNotEnoughResources.SetActive(true);
                StartCoroutine(HideNotEnoughResourcesInfo());
                
                return;
            }

            RemoveResources();
            HandleUpgrade();
            RemoveOldUpgrade();
            UnlockNewUpgrades();
        }

        private IEnumerator HideNotEnoughResourcesInfo()
        {
            yield return new WaitForSeconds(2);
            upgradeWindow.infoNotEnoughResources.SetActive(false);
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
                case UpgradeType.WoodenHouse:
                    Base.Instance.SetLevel(BaseLevel.WoodenHouse);
                    break;
                case UpgradeType.StoneHouse:
                    Base.Instance.SetLevel(BaseLevel.StoneHouse);
                    break;
                case UpgradeType.Fence:
                    Base.Instance.EnableFence();
                    break;
                case UpgradeType.Pickaxe:
                    Player.Instance.GainPickaxe();
                    break;
                case UpgradeType.Gun:
                    Base.Instance.EnableGun1();
                    break;
                case UpgradeType.Gun2:
                    Base.Instance.EnableGun2();
                    break;
                case UpgradeType.Gun3:
                    Base.Instance.EnableGun3();
                    break;
                case UpgradeType.Gun4:
                    Base.Instance.EnableGun4();
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
    Axe,
    WoodenHouse,
    StoneHouse,
    Fence,
    Gun,
    Pickaxe,
    Gun2,
    Gun3,
    Gun4
}