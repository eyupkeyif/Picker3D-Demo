using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    private Picker picker;
    private PoolManager poolManager;
    private int levelIndex;
    private Vector3 pickerStartPosition;
    private TileBase lastTile;

    private void Awake() {

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        }
        else
        {
            levelIndex=1;
            PlayerPrefs.SetInt("CurrentLevel",levelIndex);
        }   
            
            LoadAllAssets();
            Initialize();

            for (int i = 0; i < 2; i++)
            {
                GenerateLevel(levelIndex + i);
            }
            
            
    }

    public void Initialize()
    {
        poolManager = FindObjectOfType<PoolManager>();
        picker = FindObjectOfType<Picker>();
        pickerStartPosition = picker.transform.position;
        EventManager.UIEvents.SuccessButtonEvent+=SuccessEventHandler;
        EventManager.UIEvents.FailButtonEvent+=FailEventHandler;
    }

    public void SuccessEventHandler()
    {
        pickerStartPosition = new Vector3(0,picker.transform.position.y,picker.transform.position.z);
        levelIndex++;
        PlayerPrefs.SetInt("CurrentLevel",levelIndex);
        Timer.Instance.TimerWait(2, () => {
            poolManager.DeactivateLevel(levelIndex-1);
            GenerateLevel(levelIndex+1);
        });
    }
    public void FailEventHandler()
    {
        picker.transform.position = pickerStartPosition;
        picker.SetStartingTile(pickerStartPosition);
        
    }

    public void GenerateLevel(int level)
    {

        var levelData = AssetManager.Instance.LoadLevel(level);

        if (levelData==null)
        {
            levelData = AssetManager.Instance.LoadLevel(Random.Range(1,AssetManager.Instance.LoadAllLevels().Count));
        }

        for (int i = 0; i < levelData.tileDatas.Count; i++)
        {
            TileType tileType = levelData.tileDatas[i].tileType;

            switch (tileType)
            {
                case TileType.NormalTile:
                NormalTile normalTile = (NormalTile)poolManager.GetAvailableTile(levelData.tileDatas[i].tileType,level);
                if (lastTile!=null)
                {
                    normalTile.transform.position = lastTile.endPosition.position + normalTile.endPosition.position;
                }

                CollectableParent collectableParent = poolManager.GetCollectableParent(levelData.tileDatas[i].ParentType,level);
                collectableParent.transform.position = normalTile.transform.position;

                lastTile=normalTile;
                break;

                case TileType.CheckPointTile:
                CheckPointTile checkPointTile = (CheckPointTile)poolManager.GetAvailableTile(levelData.tileDatas[i].tileType,level);
                if (lastTile!=null)
                {
                    checkPointTile.transform.position = lastTile.endPosition.position + checkPointTile.endPosition.position;
                }
                
                checkPointTile.Setup(levelData.tileDatas[i].checkPointAmout);
                lastTile = checkPointTile;
                break;

                case TileType.FinalTile:
                FinalTile finalTile = (FinalTile)poolManager.GetAvailableTile(levelData.tileDatas[i].tileType,level);
                if (lastTile!=null)
                {
                    finalTile.transform.position = lastTile.endPosition.position + finalTile.endPosition.position;
                }

                lastTile=finalTile;
                break;

                default:
                break;
            }
        }


    }

    public void LoadAllAssets()
    {
        AssetManager.Instance.LoadTilePrefabs();
    }


    
}
