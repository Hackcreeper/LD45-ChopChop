using UnityEngine;

namespace UI
{
    public class Upgrade : Interactable
    {
        public void Update()
        {
            if (Focus && Input.GetMouseButtonDown(0))
            {
                Debug.Log("SHOPPING!");
            }
        }
    }
}