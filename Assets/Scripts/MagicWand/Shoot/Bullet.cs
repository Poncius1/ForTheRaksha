using UnityEngine;

public class Bullet : AutoDestroyPoolableObject
{
    public float lifetime = 5f;
    public float speed = 20f;
    private Vector3 direction;
    [SerializeField]private ParticleSystem impact;

    
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized; // Normaliza la dirección
    }

    public void Update()
    {
        // Mueve el proyectil hacia adelante en la dirección especificada.
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        // Mueve el proyectil hacia adelante en la dirección especificada.
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public override void OnDisable()
    {
        base.OnDisable();
        direction= Vector3.zero;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

        }

        gameObject.SetActive(false);
    }
}
