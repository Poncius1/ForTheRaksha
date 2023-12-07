using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PrimaryButton : MonoBehaviour
{
    [SerializeField] 
    private Button button;



    // Start is called before the first frame update
    void Start()
    {
        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
