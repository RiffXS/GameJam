using System;
using UnityEngine;
using Utils.Singleton;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject enterInteraction;
        [SerializeField] private GameObject resetInteraction;

        private void Start()
        {
            GameManager.I.GameStartedEvent += () => ControlResetInteraction(true);
        }

        public void ControlEnterInteraction(bool active)
        {
            enterInteraction.SetActive(active);
        }
        
        private void ControlResetInteraction(bool active)
        {
            resetInteraction.SetActive(active);
        }
    }
}
