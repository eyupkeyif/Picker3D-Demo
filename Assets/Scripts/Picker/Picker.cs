using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    [SerializeField] private PickerController pickerController;
    private List<CollectableBase> collectables = new List<CollectableBase>();
    [SerializeField] private Transform startingGround;
    void Start()
    {
        EventManager.GameEvents.StartGameEvent+=StartMovement;
        EventManager.LevelEvents.CheckPointEvent+=CheckPointHandler;
        EventManager.GameEvents.PassedEvent+=StartMovement;
        EventManager.GameEvents.SuccessEvent+=StopMovement;
    }

    private void OnTriggerStay(Collider other) {
        CollectableBase collectable = other.gameObject.GetComponent<CollectableBase>();
        if (collectable && !collectables.Contains(collectable))
        {
            collectables.Add(collectable);
        }
    }

    private void OnTriggerExit(Collider other) {
        CollectableBase collectable = other.gameObject.GetComponent<CollectableBase>();
        if (collectable && collectables.Contains(collectable))
        {
            collectables.Remove(collectable);
        }
    }

    public void StopMovement()
    {
        pickerController.IsMoving(false);
    }

    public void StartMovement()
    {
        pickerController.IsMoving(true);
    }
    public void SetStartingTile(Vector3 position)
    {
        startingGround.position = new Vector3(startingGround.position.x,startingGround.position.y,position.z);
    }

    public void CheckPointHandler()
    {
        pickerController.IsMoving(false);

        foreach (CollectableBase collectable in collectables)
        {
            collectable.PushCollectable();
        }
    }
}
