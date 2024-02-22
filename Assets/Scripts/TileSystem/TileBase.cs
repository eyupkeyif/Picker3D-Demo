using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileBase : MonoBehaviour
{
    public abstract TileType TileType{get;}
    public bool isActive;
    public Transform endPosition;
    public Transform startPosition;

    public virtual void Initialize()
    {
        isActive=false;
    }

    public void Activate()
    {
        isActive=true;
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        isActive=false;
        gameObject.SetActive(false);
    }
    
}

public enum TileType
{
    NormalTile,
    CheckPointTile,
    FinalTile
}
