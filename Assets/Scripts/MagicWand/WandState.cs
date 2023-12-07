using MagicWand;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;



public class WandState : MonoBehaviour
{
    public enum PlayerState
    {
        Shoot,
        Magic
    }

    [SerializeField]
    private Renderer StoneWand;

    [Header("Magic Colors")]
    public Color Magic;
    public Color MagicGlow;
    public GameObject MagicLogo;
    [SerializeField] private ParticleSystem magicParticles;

    [Header("Shoot Colors")]
    public Color Shoot;
    public Color ShootGlow;
    public GameObject ShotLogo;
    public Image wandStateUI;
    [SerializeField] private ParticleSystem shootParticles;
    
    public PlayerState currentState = PlayerState.Magic;
    [SerializeField]
    private SwitchVCam switchVCam;
    private PlayerInput playerInput;
    private InputAction changeAction;
    private Animator anim;



    private void Start()
    {
        MagicState();
        playerInput = GetComponent<PlayerInput>();
        changeAction = playerInput.actions["Change"];
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        // Detecta si se presiona el bot�n para cambiar de estado.
        if (changeAction.triggered && !switchVCam.IsAiming)
        {
            anim.SetTrigger("change");
            // Cambia entre los estados
            if (currentState == PlayerState.Shoot)
            {
                MagicState();
            }
            else if (currentState == PlayerState.Magic)
            {
                ShootState();
            }
        }

        
    }

    private void ShootState()
    {
        currentState = PlayerState.Shoot;
        StoneWand.material.SetColor("_ColorBase", Shoot);
        StoneWand.material.SetColor("_ColorBrillo", ShootGlow);
        wandStateUI.color = Shoot;
        ShotLogo.SetActive(true);
        MagicLogo.SetActive(false);
        GetComponent<MagicController>().enabled = false;
        GetComponent<ShootWand>().enabled = true;
        shootParticles.Play();
        magicParticles.Stop();
        Debug.Log("Cambio al estado Shoot.");
    }

    private void MagicState()
    {
        currentState = PlayerState.Magic;
        StoneWand.material.SetColor("_ColorBase",Magic);
        StoneWand.material.SetColor("_ColorBrillo",MagicGlow);
        ShotLogo.SetActive(false);
        MagicLogo.SetActive(true);
        wandStateUI.color = Magic;
        GetComponent<MagicController>().enabled = true;
        GetComponent<ShootWand>().enabled = false;
        magicParticles.Play();
        shootParticles.Stop();
        Debug.Log("Cambi� al estado Magic.");
    }



    public PlayerState GetWandState() => currentState;
}
