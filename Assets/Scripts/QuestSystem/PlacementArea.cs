using UnityEngine;

public class PlacementArea : MonoBehaviour
{
    [SerializeField]private ParticleSystem _activate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlaceObject"))
        {
            Debug.Log("Object entered the trigger.");

            PlaceObjectMission placeObjectMission = FindPlaceObjectMission();

            if (placeObjectMission != null)
            {
                placeObjectMission.RegisterObjectPlacement();

                GameObject placedObject = other.gameObject;
                Rigidbody rb = placedObject.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.isKinematic = true;
                    
                    placedObject.transform.parent = transform;
                    placedObject.transform.localPosition = Vector3.zero;
                    placedObject.transform.localRotation = Quaternion.identity;
                    _activate.Stop();
                    Debug.Log("Object placement successful.");
                }
                else
                {
                    Debug.LogWarning("Object does not have a Rigidbody.");
                }
            }
            else
            {
                Debug.LogWarning("PlaceObjectMission not found.");
            }
        }
    }


    private PlaceObjectMission FindPlaceObjectMission()
    {
        // Accede al MissionManager para buscar la misión PlaceObjectMission.
        MissionManager missionManager = MissionManager.Instance;

        // Itera a través de las misiones activas y busca una de tipo PlaceObjectMission.
        foreach (Mission mission in missionManager.activeMissions)
        {
            if (mission is PlaceObjectMission)
            {
                return mission as PlaceObjectMission;
            }
        }

        return null;
    }
}
