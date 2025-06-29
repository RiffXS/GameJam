using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Utils.Singleton;

public class GameManager : Singleton<GameManager>
{
    public UnityAction GameStartedEvent;
    public bool _gameStarted;
    
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        GameStartedEvent?.Invoke();
        _gameStarted = true;
    }
}
