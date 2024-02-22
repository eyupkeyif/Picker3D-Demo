using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MainMenuView : View
{
    [SerializeField] private TextMeshProUGUI startTMP;
    [SerializeField] private Button startButton;
    public override void Initialize()
    {
        startButton.onClick.AddListener(OnClickHandler);
        startTMP.text = "Tap to Play";
    }

    public void OnClickHandler()
    {
        EventManager.UIEvents.StartButtonEvent?.Invoke();
    }

  
}
