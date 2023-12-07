using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : PoolableObject
{
    public float AutoDestroyTime = 5f;
    public float MoveSpeed = 1f;
    public int Damage = 1;
    public Rigidbody Rigidbody;
    protected Transform Target;


    private const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void OnEnable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;

        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            Debug.Log("Collided with: " + other.name); // Agregar esta línea para depurar
            damageable.TakeDamage(Damage);
        }

        Disable();
    }
    public virtual void Spawn(Vector3 Forward, int Damage, Transform Target)
    {
        this.Damage = Damage;
        this.Target = Target;
        Rigidbody.AddForce(Forward * MoveSpeed, ForceMode.VelocityChange);
    }

    protected virtual void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}