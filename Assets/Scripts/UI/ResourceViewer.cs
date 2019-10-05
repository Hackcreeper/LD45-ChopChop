using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ResourceViewer : MonoBehaviour
    {
        public ResourceType type;

        private Text _text;

        private void Start()
        {
            _text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            _text.text = Resources.Instance.Get(type).ToString();
        }
    }
}