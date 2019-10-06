using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UpgradeWindow : MonoBehaviour
    {
        public float maxHeight = 3;
        public UpgradeData[] upgrades;
        public RectTransform upgradeContainer;
        public GameObject upgradePrefab;

        private RectTransform _rectTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            ReRenderUpgrades();
        }

        private void Update()
        {
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


            var lastFrame = _rectTransform.rotation;

            _rectTransform.LookAt(Player.Instance.transform);
            var original = _rectTransform.rotation.eulerAngles;
            var euler = Quaternion.Euler(0, original.y + 180, 0);

            _rectTransform.rotation = Quaternion.Lerp(lastFrame, euler, 5 * Time.deltaTime);
        }

        private void ReRenderUpgrades()
        {
            for (var i = 0; i < upgradeContainer.childCount; i++)
            {
                Destroy(upgradeContainer.GetChild(0).gameObject);
            }

            foreach (var upgrade in upgrades)
            {
                var newBox = Instantiate(upgradePrefab, upgradeContainer);
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

//        public void BuyAxe()
//        {
//            const int costsWood = 2;
//            const int costsStone = 1;
//            var resources = Resources.Instance;
//
//            if (resources.Get(ResourceType.Wood) < costsWood)
//            {
//                return;
//            }
//            
//            if (resources.Get(ResourceType.Stone) < costsStone)
//            {
//                return;
//            }
//            
//            resources.Sub(ResourceType.Wood, costsWood);
//            resources.Sub(ResourceType.Stone, costsStone);
//            
//            Player.Instance.GainAxe();
//        }
    }
}