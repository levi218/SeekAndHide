using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
#if (UNITY_EDITOR) 
[System.Serializable]
[CustomEditor(typeof(ObstacleInfo))]
public class ObstacleEditor : Editor
{

    ObstacleInfo comp;
    static bool showTileEditor = false;
    static bool showColliableEditor = false;
    public void OnEnable()
    {
        comp = (ObstacleInfo)target;
        if (comp.tiles == null)
        {
            comp.tiles = new TileBase[comp.width*comp.height];
            comp.isCollidable = new bool[comp.width * comp.height];
        }
    }

    public override void OnInspectorGUI()
    {

        //MAP DEFAULT INFORMATION
        comp.name = EditorGUILayout.TextField("Name", comp.name);

        //WIDTH - HEIGHT
        comp.width = EditorGUILayout.IntField("Map Sprite Width", comp.width);
        comp.height = EditorGUILayout.IntField("Map Sprite Height", comp.height);

        if (comp.width*comp.height != comp.tiles.Length)
        {
            comp.tiles = new TileBase[comp.width * comp.height];
        }
        if(comp.isCollidable==null || comp.isCollidable.Length!= comp.width * comp.height)
        {
            comp.isCollidable = new bool[comp.width * comp.height];

        }
        showTileEditor = EditorGUILayout.Foldout(showTileEditor, "Tile Editor");

        if (showTileEditor)
        {
            for (int i = 0; i < comp.height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < comp.width; j++)
                {
                    comp.tiles[i* comp.width +j ] = (TileBase)EditorGUILayout.ObjectField(comp.tiles[i * comp.width + j], typeof(TileBase), false);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        showColliableEditor = EditorGUILayout.Foldout(showColliableEditor, "Collidable Editor");

        if (showColliableEditor)
        {
            for (int i = 0; i < comp.height; i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < comp.width; j++)
                {
                    comp.isCollidable[i * comp.width + j] = EditorGUILayout.Toggle(comp.isCollidable[i * comp.width + j]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorUtility.SetDirty(comp);
    }
    

}
#endif