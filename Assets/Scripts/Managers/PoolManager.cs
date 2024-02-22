using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PoolManager : MonoBehaviour
{
    Dictionary<int,List<TileBase>> levelTiles = new Dictionary<int, List<TileBase>>();
    Dictionary<int,List<CollectableParent>> levelCollectables = new Dictionary<int, List<CollectableParent>>();

    public TileBase GetAvailableTile(TileType tileType,int levelIndex)
    {
        
        if (!levelTiles.ContainsKey(levelIndex))
        {
            levelTiles.Add(levelIndex,new List<TileBase>());
        }


        var levelTile = levelTiles[levelIndex]?.FirstOrDefault(x=>!x.isActive && x.TileType == tileType);

        if (levelTile==null)
        {
            levelTile = AssetManager.Instance.GetTile(tileType);
            levelTile = Instantiate(levelTile,transform);
            levelTile.Initialize();
            levelTiles[levelIndex].Add(levelTile);            
        }

        levelTile.Activate();
        return levelTile;

    }

    public CollectableParent GetCollectableParent(CollectableParentType parentType,int levelIndex)
    {
        
        if (!levelCollectables.ContainsKey(levelIndex))
        {
            levelCollectables.Add(levelIndex,new List<CollectableParent>());
        }

        var levelParent = levelCollectables[levelIndex]?.FirstOrDefault(x=> !x.isActive && x.parentType == parentType);
        if (levelParent==null)
        {
            levelParent = AssetManager.Instance.GetCollectableParent(parentType);
            levelParent = Instantiate(levelParent,transform);
            levelParent.Initialize();
            levelCollectables[levelIndex].Add(levelParent);
            
        }

        levelParent.Activate();
        return levelParent;
    }

    public void DeactivateLevel(int levelIndex)
    {
        

        foreach (var tile in levelTiles[levelIndex])
        {
            tile.Deactivate();
        }

        foreach (var parent in levelCollectables[levelIndex])
        {
            parent.Deactivate();
        }
    }
}
