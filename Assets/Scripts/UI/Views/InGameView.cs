using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class InGameView : View
{
    [SerializeField] private TextMeshProUGUI currentLevelTMP,nextLevelTMP;
    [SerializeField] private Image[] levelParts;
    private int partCounter=0;
    private int currentLevel;
    public override void Initialize()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        currentLevelTMP.text = currentLevel.ToString();
        nextLevelTMP.text = (currentLevel + 1).ToString();

        EventManager.UIEvents.SuccessButtonEvent+=LevelCompleted;
        EventManager.UIEvents.FailButtonEvent+=LevelFailed;
    }

    public void PassedLevel()
    {
        if (partCounter<levelParts.Length)
        {
            levelParts[partCounter].color = Color.green;
        }
        
        partCounter++;
    }
    public void LevelCompleted()
    {
        partCounter=0;
        foreach (var part in levelParts)
        {
            part.color = Color.white;
        }
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        currentLevelTMP.text = currentLevel.ToString();
        nextLevelTMP.text = (currentLevel+1).ToString();

    }
    public void LevelFailed()
    {
        partCounter=0;
        foreach (var part in levelParts)
        {
            part.color = Color.white;
        }
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        currentLevelTMP.text = currentLevel.ToString();
        nextLevelTMP.text = (currentLevel+1).ToString();
    }
    
}
