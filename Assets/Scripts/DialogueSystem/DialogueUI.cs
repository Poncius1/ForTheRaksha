
using UnityEngine;
using TMPro;
using System.Collections;
using StarterAssets;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] TMP_Text textLabel;
    [SerializeField] private DialogueObject testDialogue;
    private TypewriterEffect typewriterEffect;
    [SerializeField]private StarterAssetsInputs starterAssetsInputs;
    public bool IsOpen { get; private set; }
    private ResponseHandler responseHandler;


    private void Start()
    {
       responseHandler= GetComponent<ResponseHandler>();
       typewriterEffect= GetComponent<TypewriterEffect>();
       CloseDialogueBox();
    }


    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThoroughDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator StepThoroughDialogue(DialogueObject dialogueObject)
    {

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            
            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return null;
            yield return new WaitUntil(() => starterAssetsInputs.skip);
        }
        if (dialogueObject.HasResponses)
        {
            Debug.Log("La variable: " + dialogueObject.HasResponses);
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
        
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);
        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (starterAssetsInputs.skip)
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
       dialogueBox.SetActive(false);
       textLabel.text= string.Empty;
       IsOpen= false;
    }

}
