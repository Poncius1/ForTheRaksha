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

    // Llama a este m�todo cuando el jugador mate a un enemigo.
    public void RegisterKill()
    {
        currentKills++;
        // Puedes llamar a IsCompleted aqu� para verificar si la misi�n se ha completado.
        if (IsCompleted())
        {

            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }
}
