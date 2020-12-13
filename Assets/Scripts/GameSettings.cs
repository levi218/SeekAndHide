using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    public int currentLevel;
    public bool isSoundOn;

    public float minCompleteTime = -1;
    public float currentTime;
}
