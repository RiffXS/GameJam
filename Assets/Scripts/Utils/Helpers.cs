using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public static class Helpers
    {
        public const float BlackFadeTime = 0.4f;
        public const float PanelFadeTime = 0.25f;
        public static Camera Cam => Camera.main;

        public static void FadeInPanel(GameObject panel)
        {
            panel.SetActive(true);
            panel.GetComponent<CanvasGroup>().DOFade(1, PanelFadeTime).SetUpdate(true);
        }

        public static void FadeOutPanel(GameObject panel)
        {
            panel.GetComponent<CanvasGroup>().DOFade(0, PanelFadeTime).OnComplete(() => panel.SetActive(false)).SetUpdate(true);
        }

        public static void FadeCrossPanel(GameObject panelDesligar, GameObject panelLigar)
        {
            panelDesligar.GetComponent<CanvasGroup>().DOFade(0, PanelFadeTime).OnComplete(() => {
                panelDesligar.SetActive(false);
                panelLigar.SetActive(true);
                panelLigar.GetComponent<CanvasGroup>().DOFade(1, PanelFadeTime);
            });
        }
    }
}