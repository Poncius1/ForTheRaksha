using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause_Map : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private PlayerInput playerInput;
    private InputAction pauseAction;

    void Start()
    {
        
        playerInput= player.GetComponent<PlayerInput>();
        pauseAction = playerInput.actions["Pause"];
       
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseAction.triggered && !GameManager.Instance.IsGameOver)
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.Paused);
        }
    }
}
