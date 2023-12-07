using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private int Id = 0;
    [SerializeField] private string prompt;
    [SerializeField] private UIManager UImanager;
    [SerializeField] private CollectableUI collectableUI;
    [SerializeField] private AudioClip openChest;
    
    
    public string InteractionPrompt => prompt;
    private Animator anim;
    private bool isOpen = false;

    public bool Interact(Interactor interactor)
    {
        if (!isOpen) // Verifica si el cofre no está abierto
        {
            Debug.Log("Open Chest");
            anim.SetTrigger("Open");
            SoundManager.Instance.PlayEffect(openChest);
            if (UImanager != null)
            {
                UImanager.ShowCollectablePanel();
                collectableUI.UpdateCollectableUI(Id);
                GameManager.Instance.AddScore(1);
            }
            isOpen = true; // Marca el cofre como abierto
            Destroy(gameObject);
        }

        List<Mission> activeMissions = MissionManager.Instance.activeMissions;

        // Buscas una misión de tipo InteractMission y con el mismo ID que el objeto Cofre.
        foreach (Mission mission in activeMissions)
        {
            if (mission is InteractMission && ((InteractMission)mission).id == Id)
            {
                ((InteractMission)mission).MarkInteracted();
                break; // Finaliza el bucle si se encuentra una coincidencia.
            }
        }



        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();


    }
    // Update is called once per frame
    
}
