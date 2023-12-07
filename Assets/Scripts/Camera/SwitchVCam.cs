
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;
using MagicWand;
using StarterAssets;

public class SwitchVCam : MonoBehaviour
{

    [Header("Settings Parameters")]
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject Croshair;
    public float aimSensivility = 0.5f;
    public float normalSensivility = 1.5f;


    [Header("Settings Aim Cameras")]
    [SerializeField]
    private GameObject PlayerCamera;
    [SerializeField]
    private GameObject MagicCamera;
    [SerializeField]
    private GameObject ShootCamera;
    [SerializeField]
    public bool IsAiming;

    private WandState wandState;
   
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        MagicCamera.SetActive(false);
        ShootCamera.SetActive(false);
        Croshair.SetActive(false);
        wandState = Player.GetComponent<WandState>();
        starterAssetsInputs= Player.GetComponent<StarterAssetsInputs>();
        thirdPersonController = Player.GetComponent<ThirdPersonController>();
        animator = Player.GetComponent<Animator>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            StartAim();
        }
        else
        {
            CancelAim();
        }
    }


    private void StartAim()
    {
        IsAiming= true;

        if (wandState.GetWandState() == WandState.PlayerState.Magic)
        {
            MagicState();
        }
        else
        {
            ShootState();
            
        }
        
       

    }
    private void CancelAim()
    {
        IsAiming= false;
        DefaultState();
        
    }


    private void DefaultState()
    {
        Croshair.SetActive(false);
        MagicCamera.SetActive(false);
        ShootCamera.SetActive(false);
        PlayerCamera.SetActive(true);
        thirdPersonController.SetSensitivity(normalSensivility);
        OffLayer();
    }
    private void MagicState()
    {
        MagicCamera.SetActive(true);
        ShootCamera.SetActive(false);
        PlayerCamera.SetActive(false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
    }
    private void ShootState()
    {
        Croshair.SetActive(true);
        MagicCamera.SetActive(false);
        ShootCamera.SetActive(true);
        PlayerCamera.SetActive(false);
        thirdPersonController.SetSensitivity(aimSensivility);
        thirdPersonController.SetRotationOnMove(false);
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
    }

    private void OffLayer()
    {
        
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
        thirdPersonController.SetRotationOnMove(true);
    }
   

}
