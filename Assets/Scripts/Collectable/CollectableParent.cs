using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Misc;
using UnityEngine;

public class CollectableParent : MonoBehaviour
{
    public bool isActive;
    public CollectableParentType parentType;
    private List<CollectableBase> collectables;
    private Vector3 parentPosition;
    private List<Vector3> positions;
    private List<Quaternion> rotations;
    public void Initialize()
    {
        collectables = GetComponentsInChildren<CollectableBase>(true).ToList();
        parentPosition = transform.position;
        positions = new List<Vector3>(collectables.Count);
        rotations = new List<Quaternion>(collectables.Count);
        foreach (CollectableBase collectable in collectables)
        {
            positions.Add(collectable.transform.localPosition);
            rotations.Add(collectable.transform.rotation);
            
        }

        isActive=false;

        EventManager.UIEvents.FailButtonEvent+=Reset;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        isActive=true;

        transform.position = parentPosition;

        for (int i = 0; i < collectables.Count; i++)
        {
            collectables[i].transform.localPosition = positions[i];
            collectables[i].transform.rotation = rotations[i];
            collectables[i].Activate();
        }

        PyhsicsActivison(true);
    }

    public void Deactivate()
    {
        isActive=false;
        gameObject.SetActive(false);
        PyhsicsActivison(false);
    }

    public void Reset()
    {
        for (int i = 0; i < collectables.Count; i++)
        {
            collectables[i].transform.localPosition = positions[i];
            collectables[i].transform.rotation = rotations[i];
        }

        PyhsicsActivison(false);
        Timer.Instance.TimerWait(1,()=>PyhsicsActivison(true));
    }



    public void PyhsicsActivison(bool check)
    {
        foreach (var collectableBase in collectables)
        {
            collectableBase.GetComponent<Rigidbody>().isKinematic = !check;
        }
    }
}

public enum CollectableParentType
{
    Cube10,
    Cube20,
    Cube30,
    Sphere10,
    Sphere20,
    Sphere30,
    Cylinder10,
    Cylinder20,
    Cylinder30,
    
    
}
