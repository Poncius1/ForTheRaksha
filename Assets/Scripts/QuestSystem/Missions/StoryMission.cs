using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StoryMission", menuName = "ScriptableObjects/Missions/StoryMission")]
public class StoryMission : Mission
{
    [TextArea] public string title;
    public List<Mission> missions = new List<Mission>();
    [SerializeField] private int currentMissionIndex;

    public override bool IsCompleted()
    {
        if (currentMissionIndex >= missions.Count)
        {
            
            return true; // Todas las misiones se han completado en orden.
        }

        // Verifica si la misión actual está completada.
        if (missions[currentMissionIndex].isCompleted == true)
        {
            currentMissionIndex++; // Avanza al siguiente paso.
        }

        return false;
    }
}
