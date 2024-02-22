using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AssetManager : Singleton<AssetManager>
{
    private const string LEVEL_PATH = "Levels";
    private const string TILE_PATH = "Tiles";
    private const string COLLECTABLEPARENT_PATH = "CollectableParents";
    

    private List<TileBase> tileBases;
    private List<CollectableParent> collectableParents;

    public void LoadTilePrefabs()
    {
        tileBases = Resources.LoadAll<TileBase>(TILE_PATH).ToList();
        collectableParents = Resources.LoadAll<CollectableParent>(COLLECTABLEPARENT_PATH).ToList();
    }

    public TileBase GetTile(TileType tileType)
    {
        return tileBases?.FirstOrDefault(x=>x.TileType==tileType);
    }
    public CollectableParent GetCollectableParent(CollectableParentType parentType)
    {
        return collectableParents?.FirstOrDefault(x => x.parentType == parentType);
    }
    public LevelData LoadLevel(int levelIndex)
    {
        return Resources.Load<LevelData>(LEVEL_PATH + "/Level" + levelIndex);
    }
    public List<LevelData> LoadAllLevels()
    {
        return Resources.LoadAll<LevelData>(LEVEL_PATH).ToList();
    }
}
