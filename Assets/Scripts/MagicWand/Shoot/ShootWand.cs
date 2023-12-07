using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootWand : MonoBehaviour
{
    [SerializeField] private Bullet BulletPrefab;
    [SerializeField] private ObjectPool BulletPool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AudioClip _shoot;




    private Vector3 mouseWorldPosition;
    private Animator anim;
    private StarterAssetsInputs starterAssetsInputs;
    

    private float nextFireTime;

    private void Awake()
    {
        BulletPool = ObjectPool.CreateInstance(BulletPrefab,100);
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        
        if (starterAssetsInputs.aim)
        {
            
            Aiming();
        }

        if (starterAssetsInputs.shoot && Time.time >= nextFireTime)
        {
             Shoot(); 
             nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        FireBullet(firePoint.forward);
    }


   

    private void Aiming()
    {
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit hit,999f,layerMask))
        {
            mouseWorldPosition  = hit.point;
            

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            // Calcular la dirección hacia la que apunta el jugador
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            // Smoothly rotar el jugador hacia el punto de mira
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            if (starterAssetsInputs.shoot && Time.time >= nextFireTime)
            {
                Vector3 direction = (hit.point - firePoint.position).normalized;
                FireBullet(direction);
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    private void FireBullet(Vector3 direction)
    {
        anim.SetTrigger("Shoot");
        SoundManager.Instance.PlayEffect(_shoot);
        // Obtén un proyectil de la piscina de objetos
        Bullet bullet = (Bullet)BulletPool.GetObject();

        if (bullet != null)
        {
            // Configura la posición y rotación del proyectil
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.identity;

            // Establece la dirección del proyectil
            bullet.SetDirection(direction);

            // Activa el proyectil (esto llamará a OnEnable en el PoolableObject)
            bullet.gameObject.SetActive(true);
        }
    }





}
