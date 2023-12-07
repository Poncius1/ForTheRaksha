using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyBurstSpawnArea : MonoBehaviour, IInteractable
{
    [Header("Interact Config")]
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public int id; // ID del objeto Raksha
    public GameObject Chest;

    [Header("Spawn Area Config")]

    [SerializeField]
    private EnemySpawner EnemySpawner;
    [SerializeField]
    private KillEnemiesMission killEnemiesMission;
    [SerializeField]
    private List<Collider> spawnAreas = new List<Collider>();
    [SerializeField]
    private List<EnemyWaves> enemyWaveConfigs = new List<EnemyWaves>(); 
    [SerializeField] private AudioClip rakshaActivate;
    private Coroutine SpawnEnemiesCoroutine;

    [Header("Missions")]
    [SerializeField] private List<Mission> missions;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("Raksha Activado");
        
        if (SpawnEnemiesCoroutine == null)
        {
            SpawnEnemiesCoroutine = StartCoroutine(SpawnEnemies());
            SoundManager.Instance.PlayEffect(rakshaActivate);

            AddMission();
        }

        // Aquí obtienes la lista de misiones activas desde el MissionManager.
        List<Mission> activeMissions = MissionManager.Instance.activeMissions;

        // Buscas una misión de tipo InteractMission y con el mismo ID que el objeto Raksha.
        foreach (Mission mission in activeMissions)
        {
            if (mission is InteractMission && ((InteractMission)mission).id == id)
            {
                ((InteractMission)mission).MarkInteracted();
                break; // Finaliza el bucle si se encuentra una coincidencia.
            }
        }

        return true;
    }

    private void Update()
    {
        ShowChestIfMissionCompleted();
    }

    public void AddMission()
    {
        foreach (var mission in missions)
        {
            // Verifica si la misión ya está en la lista de misiones activas.
            if (!MissionManager.Instance.IsMissionActive(mission))
            {
                MissionManager.Instance.AddMission(mission);
                Debug.Log("Misión Agregada: " + mission.description); // Muestra la descripción en lugar del título.
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
        
        
    }


    public void ShowChestIfMissionCompleted()
    {
        MissionManager missionManager = MissionManager.Instance;

        if (missionManager.IsMissionCompleted(killEnemiesMission))
        {
            Destroy(gameObject);
            Chest.SetActive(true); 
        }
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