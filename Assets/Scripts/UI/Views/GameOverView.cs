using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : View
{
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI gameOverTMP;
    public override void Initialize()
    {
        
    }

    public void GameOverSuccessHandler()
    {
        gameOverTMP.color = Color.green;
        gameOverTMP.text = "Level Completed!";
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(GameOverSuccess);
    }

    public void GameOverFailHandler()
    {
        gameOverTMP.color = Color.red;
        gameOverTMP.text = "Level Failed!";
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(GameOverFail);

    }

    public void GameOverSuccess()
    {
        EventManager.UIEvents.SuccessButtonEvent?.Invoke();
    }
    public void GameOverFail()
    {
        EventManager.UIEvents.FailButtonEvent?.Invoke();
    }
}
