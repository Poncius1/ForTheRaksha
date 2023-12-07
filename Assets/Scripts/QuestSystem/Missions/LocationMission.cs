using UnityEngine;

[CreateAssetMenu(fileName = "LocationMission", menuName = "ScriptableObjects/Missions/Location")]
public class LocationMission : Mission
{
    public int Id_Location;

    private bool hasReachedLocation = false;

    public override bool IsCompleted()
    {
        
        return hasReachedLocation;
    }

    // Llama a este m�todo cuando el jugador entre en el trigger con un objeto que contiene el mismo Id_Location.
    public void MarkReachedLocation()
    {
        hasReachedLocation = true;
        // Puedes llamar a IsCompleted aqu� para verificar si la misi�n se ha completado.
        if (IsCompleted())
        {
            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }
}
