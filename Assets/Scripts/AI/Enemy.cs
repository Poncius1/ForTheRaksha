using MagicWand;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PoolableObject, IDamageable
{
     public AttackRadius AttackRadius;
    public Animator Animator;
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public int BulletDmg = 1;
    public int Health = 100;

    [SerializeField] private RagdollEnabler ragdollEnabler;
    [SerializeField] private float FadeOutDelay = 10f;

    Vector3 shootingDirection;
    public float shootingForce = 10f;

    public delegate void DeathEvent(Enemy enemy);
    public DeathEvent OnDie;

    public delegate void TakeDamageEvent();
    public TakeDamageEvent OnTakeDamage;

    [Header("Sounds")]
    [SerializeField]private AudioClip damageClip;
    [SerializeField] private AudioClip attackClip;


    private Coroutine LookCoroutine;
    private const string ATTACK_TRIGGER = "Attack";

    private void Awake()
    {
        AttackRadius.OnAttack += OnAttack;
        shootingDirection = transform.forward;
    }

    private void Start()
    {
        if (ragdollEnabler != null)
        {
            ragdollEnabler.EnableAnimator();
        }
    }

    private void OnAttack(IDamageable Target)
    {
        Animator.SetTrigger(ATTACK_TRIGGER);
        SoundManager.Instance.PlayEffect(attackClip);

        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }

    private IEnumerator LookAt(Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);

            time += Time.deltaTime * 2;
            yield return null;
        }

        transform.rotation = lookRotation;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        Agent.enabled = false;
        OnDie = null;
    }

    public void TakeDamage(int Damage)
    {
        Debug.Log("Heal" + Health);
        Health -= Damage;
        OnTakeDamage?.Invoke();

        if (Health <= 0 && ragdollEnabler != null)
        {
            KillEnemiesMission killMission = MissionManager.Instance.activeMissions.Find(mission => mission is KillEnemiesMission) as KillEnemiesMission;

            // Si se encuentra una misión válida, llama a RegisterKill() para registrar la kill.
            if (killMission != null)
            {
                killMission.RegisterKill();
            }

            OnDie?.Invoke(this);
            ragdollEnabler.EnableRagdoll(shootingDirection, shootingForce);

            StartCoroutine(FadeOut());
            
        }
    }


    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(FadeOutDelay);

        if (ragdollEnabler != null)
        {
            ragdollEnabler.DisableAllRigidbodies();
        }

        float time = 0;
        while (time < 1)
        {
            transform.position += Vector3.down * Time.deltaTime;
            time += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
        ragdollEnabler.EnableAnimator();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            SoundManager.Instance.PlayEffect(damageClip);
            TakeDamage(BulletDmg);
        }
        if(collision.collider.CompareTag("Lanzable"))
        {
           
            var dmg = collision.collider.GetComponent<Magicbody>().Damage;
            if (dmg < 0)
            {
                SoundManager.Instance.PlayEffect(damageClip);
                TakeDamage(dmg);
            }
            
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}