using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
   

    public Transform Player;
    public int NumberOfEnemiesToSpawn = 5;
    public float SpawnDelay = 1f;
    public List<EnemyScriptableObject> Enemies = new List<EnemyScriptableObject>();
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
    public bool ContnuousSpawning;
    [SerializeField] private int Level = 0;
    private int EnemiesAlive = 0;
    private int SpawnedEnemies = 0;

    private NavMeshTriangulation Triangulation;
    private Dictionary<int, ObjectPool> EnemyObjectPools = new Dictionary<int, ObjectPool>();

    private void Awake()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            EnemyObjectPools.Add(i, ObjectPool.CreateInstance(Enemies[i].Prefab, NumberOfEnemiesToSpawn));
        }
    }

    private void Start()
    {
        Triangulation = NavMesh.CalculateTriangulation();

       
    }

    

    private IEnumerator SpawnEnemies()
    {
        Level++;
        SpawnedEnemies = 0;
        EnemiesAlive = 0;
        WaitForSeconds Wait = new WaitForSeconds(SpawnDelay);

        

        while (SpawnedEnemies < NumberOfEnemiesToSpawn)
        {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(SpawnedEnemies);
            }
            else if (EnemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }

            SpawnedEnemies++;

            yield return Wait;
        }

        if (ContnuousSpawning)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private void SpawnRoundRobinEnemy(int SpawnedEnemies)
    {
        int SpawnIndex = SpawnedEnemies % Enemies.Count;

        DoSpawnEnemy(SpawnIndex, ChooseRandomPositionOnNavMesh());
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, Enemies.Count), ChooseRandomPositionOnNavMesh());
    }

    private Vector3 ChooseRandomPositionOnNavMesh()
    {
        int VertexIndex = Random.Range(0, Triangulation.vertices.Length);
        return Triangulation.vertices[VertexIndex];
    }

    public void DoSpawnEnemy(int SpawnIndex, Vector3 SpawnPosition)
    {
        PoolableObject poolableObject = EnemyObjectPools[SpawnIndex].GetObject();

        if (poolableObject != null)
        {
            Enemy enemy = poolableObject.GetComponent<Enemy>();
            Enemies[SpawnIndex].SetupEnemy(enemy);


            NavMeshHit Hit;
            if (NavMesh.SamplePosition(SpawnPosition, out Hit, 2f, -1))
            {
                enemy.Agent.Warp(Hit.position);
                // enemy needs to get enabled and start chasing now.
                enemy.Movement.target = Player;
                enemy.Movement.Triangulation = Triangulation;
                enemy.Agent.enabled = true;
                enemy.Movement.Spawn();
                enemy.OnDie += HandleEnemyDeath;

                EnemiesAlive++;
            }
            else
            {
                Debug.LogError($"Unable to place NavMeshAgent on NavMesh. Tried to use {SpawnPosition}");
            }
        }
        else
        {
            Debug.LogError($"Unable to fetch enemy of type {SpawnIndex} from object pool. Out of objects?");
        }
    }

    private void HandleEnemyDeath(Enemy enemy)
    {
        EnemiesAlive--;
        if (EnemiesAlive == 0 && SpawnedEnemies == NumberOfEnemiesToSpawn)
        {
            StartCoroutine(SpawnEnemies());
        }
    }
    public enum SpawnMethod
    {
        RoundRobin,
        Random
        // Other spawn methods can be added here
    }
}