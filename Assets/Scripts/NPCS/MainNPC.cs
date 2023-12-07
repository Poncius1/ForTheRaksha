using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private int id = 0;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private List<Mission> missions;
    
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }


    public bool Interact(Interactor interactor)
    {

        // Aquí obtienes la lista de misiones activas desde el MissionManager.
        List<Mission> activeMissions = MissionManager.Instance.activeMissions;

        // Buscas una misión de tipo InteractMission y con el mismo ID que el objeto Raksha.
        foreach (Mission mission in activeMissions)
        {
            if (mission is InteractMission && ((InteractMission)mission).id == id)
            {
                ((InteractMission)mission).MarkInteracted();
                break; // Finaliza el bucle si se encuentra una coincidencia.
            }
        }


        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                dialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        if (!dialogueUI.IsOpen) // Verifica si el diálogo ya está abierto
        {
            dialogueUI.ShowDialogue(dialogueObject);
            
            return true;
        }
        return false;
    }

    public void AddMission()
    {
        foreach (var mission in missions)
        {
            // Verifica si la misión ya está en la lista de misiones activas.
            if (!MissionManager.Instance.IsMissionActive(mission))
            {
                MissionManager.Instance.AddMission(mission);
                Debug.Log("Misión Agregada: " + mission.description); // Muestra la descripción en lugar del título.
            }
        }
    }

    
}
