using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDictionary", menuName = "Level Dictionary", order = 51)]
[Serializable]
public class LevelDictionary : ScriptableObject
{
    public LevelData[] datas;

    public LevelData GetLevel(int level)
    {
        if (level >= 0 && level < datas.Length)
            return datas[level];
        return null;
    }
}
