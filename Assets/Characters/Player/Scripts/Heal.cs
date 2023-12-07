using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Heal : MonoBehaviour
{
    [Header("Heal Hability")]
    public Image habilityImage;
    [SerializeField]private float CooldownTime = 10;
    [SerializeField]private int GainLifes = 8;
    [SerializeField] private ParticleSystem healParticle;
    [SerializeField] private AudioClip healEffect;


    bool isCooldown = false;
    
    private PlayerInput playerInput;
    private InputAction healAction;
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        healAction = playerInput.actions["Power"];
        habilityImage.fillAmount = 0;
    }

    private void Update() {
        HealHability();
    }
    private void HealHability()
    {
        if(healAction.triggered && isCooldown == false)
        {
            anim.SetTrigger("heal");
            healParticle.Play();
            SoundManager.Instance.PlayEffect(healEffect);
            GameManager.Instance.Addife(GainLifes);
            isCooldown = true;
            habilityImage.fillAmount = 1;
        }


        if(isCooldown)
        {
            habilityImage.fillAmount -= 1/ CooldownTime * Time.deltaTime;

            if(habilityImage.fillAmount <= 0)
            {
                habilityImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }
}
