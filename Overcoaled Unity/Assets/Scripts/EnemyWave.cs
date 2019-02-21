using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public int enterDistance;
    public int amount;
    [HideInInspector] public bool activated = false;
    [HideInInspector] public GameObject enemyBody;
}
