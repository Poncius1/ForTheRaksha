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

    // Llama a este m�todo cuando el jugador interact�e con el objeto objetivo.
    public void MarkInteracted()
    {
        hasInteracted = true;
        // Puedes llamar a IsCompleted aqu� para verificar si la misi�n se ha completado.
        if (IsCompleted())
        {
            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }
}
