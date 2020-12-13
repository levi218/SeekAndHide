using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle", order = 51)]
[Serializable]
public class ObstacleInfo : ScriptableObject
{
    public string obsName;

    public int width;
    public int height;
    public TileBase[] tiles;
    public bool[] isCollidable;

    public Vector3Int[] GetPositions(Vector3Int basePos, int z)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        //Vector3Int[] result = new Vector3Int[width*height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if(isCollidable[i * width + j])
                result.Add(new Vector3Int(basePos.x + j, basePos.y + height - i, z));
                //result[i * width + j] = 
            }
        }
        return result.ToArray() ;
    }

    public TileBase[] GetTiles()
    {
        List<TileBase> result = new List<TileBase>();
        //TileBase[] tilesCollidable = (TileBase[])tiles.Clone();
        for (int i = 0; i < tiles.Length; i++) if (isCollidable[i]) result.Add(tiles[i]);
        return result.ToArray();
    }

    public Vector3Int[] GetPositionsDecorate(Vector3Int basePos, int z)
    {
        List<Vector3Int> result = new List<Vector3Int>();
        //Vector3Int[] result = new Vector3Int[width*height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (!isCollidable[i * width + j])
                    result.Add(new Vector3Int(basePos.x + j, basePos.y + height - i, z));
                //result[i * width + j] = 
            }
        }
        return result.ToArray();
    }

    public TileBase[] GetTilesDecorate()
    {
        List<TileBase> result = new List<TileBase>();
        //TileBase[] tilesCollidable = (TileBase[])tiles.Clone();
        for (int i = 0; i < tiles.Length; i++) if (!isCollidable[i]) result.Add(tiles[i]);
        return result.ToArray();
    }
}
