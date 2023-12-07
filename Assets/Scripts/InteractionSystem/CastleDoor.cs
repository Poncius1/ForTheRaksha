using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CastleDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private string prompt;
    [SerializeField] private int Id;
    public string InteractionPrompt => prompt;

    [SerializeField] private LoadingScene loadingScene;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueObject dialogueObject;

    public bool Interact(Interactor interactor)
    {

        if (GameManager.Instance.VillageFree)
        {
            // Aquí obtienes la lista de misiones activas desde el MissionManager.
            List<Mission> activeMissions = MissionManager.Instance.activeMissions;

            // Buscas una misión de tipo InteractMission y con el mismo ID que el objeto Raksha.
            foreach (Mission mission in activeMissions)
            {
                if (mission is InteractMission && ((InteractMission)mission).id == Id)
                {
                    ((InteractMission)mission).MarkInteracted();
                    break; // Finaliza el bucle si se encuentra una coincidencia.
                }
            }

            loadingScene.LoadScene(2);
            return true;
        }
        else
        {
            if (!dialogueUI.IsOpen) // Verifica si el diálogo ya está abierto
            {
                dialogueUI.ShowDialogue(dialogueObject);

                
            }
        }
        return false;
    }
}
