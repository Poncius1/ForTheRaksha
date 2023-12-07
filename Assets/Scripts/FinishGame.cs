using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;

    private void OnTriggerEnter(Collider other)
    {
        
        Cursor.lockState = CursorLockMode.None;
        finishPanel.SetActive(true);
        
    }


}
