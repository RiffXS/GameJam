using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Singleton;

namespace UI
{
    public class ControleFadePreto : Singleton<ControleFadePreto>
    {
        [SerializeField] public GameObject telaPretaPanel;
        [SerializeField] public CanvasGroup cgTelaPreta;
        private bool _restart;

        private float TempoFadePreto => Helpers.TempoPretoFade;
        //private AudioManager _audioManager => AudioManager.I;

        protected override void Awake()
        {
            base.Awake();

            Time.timeScale = 1;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            FadeInSceneStart();
            if (_restart)
            {
                //_audioManager.FadeInMusic("Main");
                _restart = false;
            }
        }

        public void FadeInSceneStart()
        {
            telaPretaPanel.SetActive(true);
            cgTelaPreta.alpha = 1f;
            cgTelaPreta.DOFade(0, TempoFadePreto).onComplete = () => telaPretaPanel.SetActive(false);
        }

        public void FadeOutScene(string nomeScene)
        {
            telaPretaPanel.SetActive(true);
            cgTelaPreta.DOFade(1, TempoFadePreto).OnComplete(() => SceneManager.LoadScene(nomeScene)).SetUpdate(true);
        }

        public void FadePanel(GameObject panel, bool estado)
        {
            telaPretaPanel.SetActive(true);
            cgTelaPreta.DOFade(1, TempoFadePreto).onComplete = () => { 
                panel.SetActive(estado); 
                FadeInSceneStart(); 
            };
        }

        public void RestartStraightGame()
        {
            telaPretaPanel.SetActive(true);
            cgTelaPreta.DOFade(1, TempoFadePreto).OnComplete(() => {
                _restart = true;
                SceneManager.LoadScene("Main");
            
            }).SetUpdate(true);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}