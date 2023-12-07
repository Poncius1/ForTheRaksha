using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Wave Config", menuName = "ScriptableObjects/Waves/Wave Config")]
public class EnemyWaves : ScriptableObject
{
    public List<EnemyScriptableObject> enemies = new List<EnemyScriptableObject>();
    public float timeBetweenSpawns = 1.0f;
    public int numberOfEnemiesToSpawn = 5;
}
