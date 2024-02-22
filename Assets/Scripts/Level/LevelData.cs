using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName ="Levels/CreateLevel")]
public class LevelData:ScriptableObject
{
    public List<TileData> tileDatas;
    public int levelNumber;

    public int LevelNumber => levelNumber;
    public List<TileData> TileDatas=>tileDatas;
    
}

[Serializable]
public class TileData
{
    public TileType tileType;
    public int checkPointAmout;
    public CollectableParentType ParentType;

    public TileType _tileType=>tileType;
    public int _checkPointAmount =>checkPointAmout;
    public CollectableParentType _parentType => ParentType;
    
}
