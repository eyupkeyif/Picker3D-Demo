using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    private void Start() 
    {
        EventManager.UIEvents.StartButtonEvent+=StartGame;
        EventManager.UIEvents.SuccessButtonEvent+=StartGame;
    }

    public void StartGame()
    {
        EventManager.GameEvents.StartGameEvent?.Invoke();
    }

    public void GameOverSuccess()
    {
        EventManager.GameEvents.SuccessEvent?.Invoke();
    }

    public void GameOverFail()
    {
        EventManager.GameEvents.FailEvent?.Invoke();
    }
    
}
