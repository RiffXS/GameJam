using System;
using Utils.Singleton;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackScreenController : DontDestroySingleton<BlackScreenController>
{
    [SerializeField] private GameObject _blackScreen_Panel;
    [SerializeField] private CanvasGroup _blackScreen_CanvasGroup;
    
    private float _blackFadeTime => Utils.Helpers.BlackFadeTime;

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        if (GameManager.I._gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
                GameManager.I._gameStarted = false;
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeInSceneStart();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #region Fade in and out

    public void FadeInBlack()
    {
        _blackScreen_Panel.SetActive(true);
        _blackScreen_CanvasGroup.DOFade(1, _blackFadeTime);
    }

    public void FadeOutBlack()
    {
        _blackScreen_CanvasGroup.DOFade(0, _blackFadeTime);
        _blackScreen_Panel.SetActive(false);
    }

    #endregion

    #region Fades with scenes
    public void FadeInSceneStart()
    {
        _blackScreen_Panel.SetActive(true);
        _blackScreen_CanvasGroup.alpha = 1f;
        _blackScreen_CanvasGroup.DOFade(0, _blackFadeTime).onComplete = () => _blackScreen_Panel.SetActive(false);
    }

    public void FadeOutScene(string nextScene)
    {
        _blackScreen_CanvasGroup.alpha = 0f;
        _blackScreen_Panel.SetActive(true);
        _blackScreen_CanvasGroup.DOFade(1, _blackFadeTime).OnComplete(() => {
            SceneManager.LoadScene(nextScene);
            }).SetUpdate(true);
    }

    public void RestartGame()
    {
        _blackScreen_Panel.SetActive(true);
        _blackScreen_CanvasGroup.DOFade(1, _blackFadeTime).OnComplete(() =>
        {
            SceneManager.LoadScene("JessTest");
            GameManager.I._gameStarted = true;
        }).SetUpdate(true);
    }

    #endregion

    #region Fades with panels
    public void FadePanel(GameObject panel, bool estado)
    {
        _blackScreen_Panel.SetActive(true);
        _blackScreen_CanvasGroup.DOFade(1, _blackFadeTime).SetUpdate(true).onComplete = () => {
            panel.SetActive(estado);
            FadeInSceneStart();
        };
    }
    #endregion

    #region GET

    public bool GetBlackScreenOn()
    {
        return _blackScreen_CanvasGroup.alpha == 1;
    }

    public bool GetBlackScreenOff()
    {
        return _blackScreen_CanvasGroup.alpha == 0;
    }

    #endregion
}