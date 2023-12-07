using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeKill : MonoBehaviour
{
    public  Enemy enemy;
    private int dmg = 4;
    [SerializeField] private ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        
        explosion.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explosion.Play();
            enemy.TakeDamage(dmg);
            GameManager.Instance.LoseLife(2);
        }
        
        
    }
}
