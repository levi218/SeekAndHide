using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInformation", menuName = "Enemy Information", order = 51)]
[Serializable]
public class EnemyInformation : ScriptableObject
{
    public string enemyName;
    public string description;
    public Sprite sprite;
    public Color color;
    public bool encoutered;
}
