using UnityEngine;
using Utils.Singleton;

namespace UI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private GameObject enterInteraction;

        public void ControlEnterInteraction(bool active)
        {
            enterInteraction.SetActive(active);
        }
    }
}
