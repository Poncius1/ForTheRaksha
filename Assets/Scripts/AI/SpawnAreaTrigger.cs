using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class SpawnAreaTrigger : MonoBehaviour
{

    [Header("Spawn Area Config")]
    [SerializeField]
    private EnemySpawner EnemySpawner;
    [SerializeField]
    private List<Collider> spawnAreas = new List<Collider>();
    [SerializeField]
    private List<EnemyWaves> enemyWaveConfigs = new List<EnemyWaves>(); 
    private Coroutine SpawnEnemiesCoroutine;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (SpawnEnemiesCoroutine == null)
            {
                SpawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
            }
        }
        
    }
    
    
    private Vector3 GetRandomPositionInBounds()
    {
        Collider chosenCollider = spawnAreas[Random.Range(0, spawnAreas.Count)];
        Bounds bounds = chosenCollider.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            bounds.min.y,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (EnemyWaves waveConfig in enemyWaveConfigs)
        {
            for (int i = 0; i < waveConfig.numberOfEnemiesToSpawn; i++)
            {
                foreach (EnemyScriptableObject enemy in waveConfig.enemies)
                {
                    EnemySpawner.DoSpawnEnemy(
                        EnemySpawner.Enemies.FindIndex((e) => e.Equals(enemy)),
                        GetRandomPositionInBounds()
                    );
                    yield return new WaitForSeconds(waveConfig.timeBetweenSpawns);
                }
            }
        }

        Destroy(this);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        if (spawnAreas != null && spawnAreas.Count > 0)
        {
            foreach (Collider spawnArea in spawnAreas)
            {
                if (spawnArea != null)
                {
                    Gizmos.DrawWireCube(spawnArea.bounds.center, spawnArea.bounds.size);
                }
            }
        }
        else
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
            }
        }
    }
}