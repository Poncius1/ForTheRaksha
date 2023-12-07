using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private DialogueObject dialogueNotCompleted;
    
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        
        CompletedMission();
        return true;
    }

    private void CompletedMission()
    {
        MissionManager missionManager = MissionManager.Instance;
        foreach (Mission mission in missionManager.activeMissions)
        {
            if (!mission.IsCompleted())
            {
                dialogueUI.ShowDialogue(dialogueNotCompleted);
                return;
            }
        }

        // Si llegamos aquí, todas las misiones activas están completas.
        foreach (Mission mission in missionManager.activeMissions)
        {
            missionManager.CompleteMission(mission);
        }
        dialogueUI.ShowDialogue(dialogueObject);
        Debug.Log("Todas las misiones activas están completas y se han agregado a las misiones completadas.");
    }


}
