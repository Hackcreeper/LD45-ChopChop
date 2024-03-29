﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeWindow : MonoBehaviour
    {
        public float maxHeight = 3;
        public List<UpgradeData> upgrades;
        public RectTransform upgradeContainer;
        public GameObject upgradePrefab;
        public GameObject infoNoUpgrades;
        public GameObject infoOnlyOnDay;
        public GameObject infoNotEnoughResources;
        
        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            ReRenderUpgrades();
        }

        private void Update()
        {
            infoNoUpgrades.SetActive(false);
            infoOnlyOnDay.SetActive(false);
            
            if (upgrades.Count == 0)
            {
                infoNoUpgrades.SetActive(true);
            }

            if (!DayNight.Instance.IsDay())
            {
                infoNoUpgrades.SetActive(false);
                infoOnlyOnDay.SetActive(true);
            }
            
            upgradeContainer.gameObject.SetActive(DayNight.Instance.IsDay());

            var targetHeight = Vector3.Distance(transform.position, Player.Instance.transform.position) > 25
                ? 0
                : maxHeight;

            var sizeDelta = _rectTransform.sizeDelta;
            _rectTransform.sizeDelta = Vector2.Lerp(
                sizeDelta,
                new Vector2(
                    sizeDelta.x,
                    targetHeight
                ),
                5 * Time.deltaTime
            );
        }

        public void ReRenderUpgrades()
        {
            for (var i = 0; i < upgradeContainer.childCount; i++)
            {
                Destroy(upgradeContainer.GetChild(i).gameObject);
            }

            foreach (var upgrade in upgrades)
            {
                var newBox = Instantiate(upgradePrefab, upgradeContainer);
                newBox.GetComponent<Upgrade>().data = upgrade;
                newBox.GetComponent<Upgrade>().upgradeWindow = this;
                
                var boxTransform = newBox.transform;
                
                boxTransform.Find("Icon").GetComponent<Image>().sprite = upgrade.icon;
                boxTransform.Find("Label").GetComponent<Text>().text = upgrade.displayName;

                foreach (var cost in upgrade.costs)
                {
                    var type = cost.type.ToString();

                    boxTransform.Find("Costs").Find(type).gameObject.SetActive(true);
                    boxTransform.Find("Costs")
                        .Find(type)
                        .Find("Label").GetComponent<Text>().text = cost.amount.ToString();
                }

                newBox.SetActive(true);
            }
        }
    }
}