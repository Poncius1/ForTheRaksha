using UnityEngine;

[CreateAssetMenu(fileName = "PlaceObjectMission", menuName = "ScriptableObjects/Missions/PlaceObject")]
public class PlaceObjectMission : Mission
{
    public GameObject objectToPlace;
    public bool isObjectPlaced = false;

    public void RegisterObjectPlacement()
    {
        isObjectPlaced = true;
        // Puedes llamar a IsCompleted aqu� para verificar si la misi�n se ha completado.
        if (IsCompleted())
        {
            MissionManager.Instance.CompleteMission(this);
            MarkCompleted();
        }
    }

    public override bool IsCompleted()
    {
        return isObjectPlaced;
    }
}
