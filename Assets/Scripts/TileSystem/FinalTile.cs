using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Misc;
public class FinalTile : TileBase
{
    public override TileType TileType => TileType.FinalTile;
    
    private void OnTriggerEnter(Collider other) {
        Picker picker = other.gameObject.GetComponent<Picker>();

        if (picker)
        {
            Timer.Instance.TimerWait(1,()=>LevelPassed());
        }
    }

    public void LevelPassed()
    {
        GameManager.Instance.GameOverSuccess();
    }
}
