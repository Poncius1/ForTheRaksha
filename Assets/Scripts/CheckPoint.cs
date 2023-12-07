using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Vector3 spawnPosition;
    public GameObject logicCompleted;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LastCheckPointPos = spawnPosition;
            if (logicCompleted != null)
            {
                Destroy(logicCompleted);
            }
            
        }

    }
}
