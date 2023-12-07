using UnityEngine;

[CreateAssetMenu(fileName = "InteractMission", menuName = "ScriptableObjects/Missions/Interact")]
public class InteractMission : Mission
{
    public int id;
    private bool hasInteracted = false;

    

    public override bool IsCompleted()
    {
        return hasInteracted;
    }

    // Llama a este método cuando el jugador interactúe con el objeto objetivo.
    public void MarkInteracted()
    {
        hasInteracted = true;
        // Puedes llamar a IsCompleted aquí para verificar si la misión se ha completado.
        if (IsCompleted())
        {
            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }
}
