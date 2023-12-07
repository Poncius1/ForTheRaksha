using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera _maincamera;
    public TextMeshProUGUI promptText;
    public GameObject promptPanel;
    // Start is called before the first frame update
    void Start()
    {
        _maincamera= Camera.main;
        promptPanel.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var rotation = _maincamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward,rotation * Vector3.up);
    }
    public bool IsDisplayed = false;
    
    public void SetUp(string PromptText)
    {
        promptText.text= PromptText;
        promptPanel.SetActive(true);
        IsDisplayed= true;
    }

    public void Close()
    {
        promptPanel.SetActive(false);
        IsDisplayed = false;
    }
}
