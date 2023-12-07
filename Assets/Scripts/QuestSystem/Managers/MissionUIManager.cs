using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MissionUIManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private List<Mission> activeMissions = new List<Mission>();
    private int currentMissionIndex = 0;

    private void Update()
    {
        // Obtén la lista de misiones activas desde el MissionManager.
        activeMissions = MissionManager.Instance.activeMissions;

        // Verifica si hay misiones activas y si el índice actual está dentro de los límites.
        if (activeMissions.Count > 0 && currentMissionIndex < activeMissions.Count)
        {
            Mission currentMission = activeMissions[currentMissionIndex];
            titleText.text = currentMission.missionName;
            descriptionText.text = currentMission.description;

            // Comprueba si la misión actual se ha completado y aplica el Strikethrough en ese caso.
            if (currentMission.isCompleted)
            {
                descriptionText.fontStyle = FontStyles.Strikethrough;
            }
            else
            {
                descriptionText.fontStyle = FontStyles.Normal;
            }
        }
        else
        {
            // Todas las misiones se han completado o no hay ninguna. Muestra un mensaje de finalización.
            titleText.text = "Mission Completed";
            descriptionText.text = string.Empty;
        }
    }
}
