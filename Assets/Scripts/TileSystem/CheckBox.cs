using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CheckBox : MonoBehaviour
{
    int counter = 0;
    int reqNumber;
    [SerializeField] private TextMeshProUGUI counterTMP;

    public void Setup(int reqNumber)
    {
        this.reqNumber = reqNumber;
        counterTMP.text = counter + "/" + reqNumber;
    }
    public void Reset()
    {
        counter=0;
        UpdateCounter();
    }

    private void OnTriggerEnter(Collider other) 
    {
        CollectableBase collectable = other.gameObject.GetComponent<CollectableBase>();

        if (collectable)
        {
            EventManager.LevelEvents.CollectableCounterEvent?.Invoke();
            counter++;
            UpdateCounter();
            
        }
    }

    private void UpdateCounter()
    {
        counterTMP.text = counter + "/" + reqNumber;
    }

    public int GetCounter()
    {
        return counter;
    }
}
