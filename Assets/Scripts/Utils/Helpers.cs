using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public static class Helpers
    {
        public const float TempoPretoFade = 0.4f;
        public const float TempoPanelFade = 0.25f;
        public static Camera Cam => Camera.main;

        public static void FadeInPanel(GameObject panel)
        {
            panel.SetActive(true);
            panel.GetComponent<CanvasGroup>().DOFade(1, TempoPanelFade).SetUpdate(true);
        }

        public static void FadeOutPanel(GameObject panel)
        {
            panel.GetComponent<CanvasGroup>().DOFade(0, TempoPanelFade).OnComplete(() => panel.SetActive(false)).SetUpdate(true);
        }

        public static void FadeCrossPanel(GameObject panelDesligar, GameObject panelLigar)
        {
            panelDesligar.GetComponent<CanvasGroup>().DOFade(0, TempoPanelFade).OnComplete(() => {
                panelDesligar.SetActive(false);
                panelLigar.SetActive(true);
                panelLigar.GetComponent<CanvasGroup>().DOFade(1, TempoPanelFade);
            });
        }
    }
}