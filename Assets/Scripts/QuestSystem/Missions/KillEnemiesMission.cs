using UnityEngine;

[CreateAssetMenu(fileName = "KillEnemies", menuName = "ScriptableObjects/Missions/KillEnemies")]
public class KillEnemiesMission : Mission
{
    
    public int requiredKills;
    public int currentKills = 0;

    

    public override bool IsCompleted()
    {
        return currentKills >= requiredKills;
    }

    // Llama a este método cuando el jugador mate a un enemigo.
    public void RegisterKill()
    {
        currentKills++;
        // Puedes llamar a IsCompleted aquí para verificar si la misión se ha completado.
        if (IsCompleted())
        {

            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }
}
