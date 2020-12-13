using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;
public class MapGenerator : MonoBehaviour
{
    public Tilemap background;
    public Tilemap tilemapObs;
    public Tilemap tilemapDeco;

    public TileBase tileBG;
    public ObstacleInfo[] obs;
    int numberOfObj;
    //int xMin = -5, xMax=30, yMin=-15, yMax=25;

    public GameObject[] enemies;

    public Transform checkPoints;

    //public int numberOfEnemies = 8;
    //public int obstacleRate = 6;

    public Transform finishPoint;

    [Header("Level Configuration")]
    public LevelDictionary levelDictionary;

    LevelData level;

    bool isLevelExist = true;
    // Start is called before the first frame update
    void Awake()
    {
        numberOfObj = 0;
        level = levelDictionary.GetLevel(Setting.instance._settings.currentLevel);
        if (level == null)
        {
            isLevelExist = false;
            return;
        }
        //Random.InitState(System.DateTime.Now.Millisecond);
        GenerateMap();
        Invoke("Scan", 1);

        GenerateCheckpoints();
        GenerateEnemies();

    }
    private void Start()
    {
        if (!isLevelExist)
        {
            GameController.Instance.PlayerWin();
        }
        else
        {
            GameController.Instance.ShowMessage("Stage" + (Setting.instance._settings.currentLevel + 1), true);
        }
    }
    void GenerateMap()
    {
        background.ClearAllTiles();
        tilemapObs.ClearAllTiles();
        tilemapDeco.ClearAllTiles();
        // generate background
        BoxFill(level.xMin, level.yMin, level.xMax, level.yMax);

        // border
        //for (int row = yMin - 3; row < yMax - 3; row+=2)
        for (int row = level.yMax - 3; row >= level.yMin - 3; row -= 2)
        {
            AddObject(obs[Random.Range(0, 2)], new Vector3Int(level.xMin, row, 0));
            AddObject(obs[Random.Range(0, 2)], new Vector3Int(level.xMax, row, 0));
        }
        for (int x = level.xMin; x < level.xMax; x += 2)
        {
            AddObject(obs[Random.Range(0, 2)], new Vector3Int(x, level.yMin - 3, 0));
            AddObject(obs[Random.Range(0, 2)], new Vector3Int(x, level.yMax - 3, 0));
        }
        // spawn obstacle
        for (int row = level.yMax - 4; row >= level.yMin + 2; row--)
        {
            int numberOfObsOnRow = level.obstacleRate;
            for (int i = 0; i < numberOfObsOnRow; i++)
            {
                int col = Random.Range(level.xMin, level.xMax-2);
                Vector3Int pos = new Vector3Int(col, row, 0);
                if (Mathf.Abs(col) > 2 && Mathf.Abs(row) > 2 && tilemapObs.GetTile(pos)==null)
                    AddObject(obs[Random.Range(0, obs.Length)], pos);
            }
        }
        //background.CompressBounds();
        //tilemapObs.CompressBounds();
        //tilemapDeco.CompressBounds();
    }

    void BoxFill(int xMin, int yMin, int xMax, int yMax)
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        List<TileBase> tiles = new List<TileBase>();
        for (int x = xMin; x <= xMax; x++)
        {
            for (int y = yMin; y <= yMax; y++)
            {
                positions.Add(new Vector3Int(x, y, 0));
                tiles.Add(tileBG);
            }
        }
        background.SetTiles(positions.ToArray(), tiles.ToArray());
    }

    List<Vector3Int> GenerateFreeSlotArray()
    {
        List<Vector3Int> result = new List<Vector3Int>();
        BoundsInt bounds = tilemapObs.cellBounds;
        TileBase[] allCell = tilemapObs.GetTilesBlock(bounds);

        for (int x = 3; x < bounds.size.x - 3; x++)
        {
            for (int y = 3; y < bounds.size.y - 3; y++)
            {
                if (allCell[x * bounds.size.y + y] == null)
                {
                    if (!((Mathf.Abs(bounds.x + x) < 8) && (Mathf.Abs(bounds.y + y) < 8)))
                    {
                        result.Add(new Vector3Int(bounds.x + x, bounds.y + y, 0));
                    }
                }
            }

        }
        Debug.Log(result.Count);
        return result;
    }
    void GenerateCheckpoints()
    {
        List<Vector3Int> allowedPos = GenerateFreeSlotArray();
        int numberCp = 5;
        for (int i = 0; i < level.enemiesRate.Length; i++)
        {
            numberCp += level.enemiesRate[i];
        }
        for (int i = 0; i < numberCp; i++)
        {
            Vector3Int spawnPos = allowedPos[Random.Range(0, allowedPos.Count)];
            AddCheckpoint(spawnPos);
        }
        finishPoint.position = allowedPos[Random.Range(0, allowedPos.Count)]+Vector3.up;
    }
    void GenerateEnemies()
    {
        List<Vector3Int> allowedPos = GenerateFreeSlotArray();
        for (int i = 0; i < level.enemiesRate.Length; i++)
        {
            //int e_id = Random.Range(0, enemies.Length);
            //Debug.Log("spawn enemy " + e_id);
            Vector3Int spawnPos = allowedPos[Random.Range(0, allowedPos.Count)];
            AddEnemy(enemies[i], spawnPos);
        }
    }

    void AddCheckpoint(Vector3Int pos)
    {
        GameObject go = new GameObject("cp");
        //GameObject go = Instantiate<GameObject>(enemy, background.CellToWorld(pos), Quaternion.identity);
        Vector3 calibPos = background.CellToWorld(pos);
        calibPos.z = 0;
        go.transform.position = calibPos;
        go.transform.SetParent(checkPoints);
    }

    void AddEnemy(GameObject enemy, Vector3Int pos)
    {
        GameObject go = Instantiate<GameObject>(enemy, background.CellToWorld(pos), Quaternion.identity);
        Vector3 calibPos = go.transform.position;
        calibPos.z = 0;
        go.transform.position = calibPos;
        go.GetComponent<TargetChooser>().checkpoints = checkPoints;
    }
    void AddObject(ObstacleInfo obj, Vector3Int pos)
    {
        numberOfObj += 1;
        tilemapObs.SetTiles(obj.GetPositions(pos, numberOfObj), obj.GetTiles());
        tilemapDeco.SetTiles(obj.GetPositionsDecorate(pos, numberOfObj), obj.GetTilesDecorate());

    }
    void Scan()
    {
        AstarPath.active.Scan();
    }
}
