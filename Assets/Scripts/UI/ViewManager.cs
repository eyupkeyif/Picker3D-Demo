using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private MainMenuView mainMenuView;
    private InGameView inGameView;
    private GameOverView gameOverView;
    
    private void Start() 
    {
        mainMenuView = ViewController.instance.GetView<MainMenuView>();
        inGameView = ViewController.instance.GetView<InGameView>();
        gameOverView = ViewController.instance.GetView<GameOverView>();

        if (mainMenuView!=null)
        {
            ViewController.instance.ShowStartingView();
        }
        
        #region Game Events
        EventManager.GameEvents.PassedEvent+=PassedLevel;
        EventManager.GameEvents.SuccessEvent+=GameOverSuccessHandler;
        EventManager.GameEvents.FailEvent+=GameOverFailHandler;
        #endregion

        #region UI Events
        EventManager.UIEvents.StartButtonEvent+=ShowInGame;
        EventManager.UIEvents.SuccessButtonEvent+=ShowInGame;
        EventManager.UIEvents.FailButtonEvent+=ReturnMainMenu;
        #endregion
    }

    public void PassedLevel()
    {
        inGameView.PassedLevel();
    }
    public void ShowInGame()
    {
        gameOverView.Hide();
        mainMenuView.Hide();
        inGameView.Show();
    }

    public void GameOverSuccessHandler()
    {
        gameOverView.GameOverSuccessHandler();
        gameOverView.Show();
    }
    public void GameOverFailHandler()
    {
        gameOverView.GameOverFailHandler();
        gameOverView.Show();
    }
    public void ReturnMainMenu()
    {
        gameOverView.Hide();
        mainMenuView.Show();
    }
}
