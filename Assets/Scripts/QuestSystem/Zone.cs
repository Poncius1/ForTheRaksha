using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private int _id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtiene todas las misiones activas desde el MissionManager.
            var activeMissions = MissionManager.Instance.activeMissions;

            // Itera a través de las misiones activas.
            foreach (var mission in activeMissions)
            {
                if (mission is LocationMission)
                {
                    // Verifica si la misión actual es de tipo LocationMission.
                    LocationMission locationMission = mission as LocationMission;

                    if (_id == locationMission.Id_Location)
                    {
                        // Completa la LocationMission actual.
                        locationMission.MarkReachedLocation();
                        Debug.Log("Llegaste a la Zona");
                    }
                }
            }
        }
    }
}
