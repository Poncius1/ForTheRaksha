using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask InteractionMask;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator anim;
    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    private IInteractable interactable;

    private void Start()
    {
        starterAssetsInputs= GetComponent<StarterAssetsInputs>();
        anim= GetComponent<Animator>();
        
    }
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, InteractionMask);

        if (numFound > 0)
        {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!interactionPromptUI.IsDisplayed) interactionPromptUI.SetUp(interactable.InteractionPrompt);

                if ( starterAssetsInputs.interact)
                {
                    anim.SetBool("interact", true);
                    interactable.Interact(this);
                }
               
                
               
            }
            
        }
        else
        {
            anim.SetBool("interact", false);
            if (interactable != null) interactable = null;
            if (interactionPromptUI.IsDisplayed) interactionPromptUI.Close();
        
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(interactionPoint.position, interactionPointRadius);   
    }


}
