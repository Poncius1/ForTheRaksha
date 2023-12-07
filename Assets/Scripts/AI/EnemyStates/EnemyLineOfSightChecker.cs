
using StarterAssets;
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class EnemyLineOfSightChecker : MonoBehaviour
{
    public SphereCollider _collider;
    public float FieldOfView = 90f;
    public LayerMask LineOfSightLayer;


    public delegate void GainSightEvent(ThirdPersonController player);
    public GainSightEvent OnGainSight;
    public delegate void LoseSightEvent(ThirdPersonController player);
    public LoseSightEvent OnLosesSight;

    private Coroutine CheckForLineOfSightCoroutine;
    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ThirdPersonController player;
        if (other.TryGetComponent<ThirdPersonController>(out player))
        {
            if (!CheckLineOfSight(player))
            {
                CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(player));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ThirdPersonController player;
        if (other.TryGetComponent<ThirdPersonController>(out player))
        {
           OnLosesSight?.Invoke(player);
            if (CheckForLineOfSightCoroutine != null)
            {
                StopCoroutine(CheckForLineOfSightCoroutine);
            }
        }
    }


    private bool CheckLineOfSight(ThirdPersonController player)
    {
        Vector3 Direction = (player.transform.position - transform.position).normalized;
        if (Vector3.Dot(transform.forward,Direction) >= Mathf.Cos(FieldOfView))
        {
            RaycastHit Hit;

            if (Physics.Raycast(transform.position, Direction, out Hit, _collider.radius,LineOfSightLayer))
            {
                if (Hit.transform.GetComponent<ThirdPersonController>() != null)
                {
                    OnGainSight?.Invoke(player);
                    return true;
                }
            }
        }

        return false;
    }


    private IEnumerator CheckForLineOfSight(ThirdPersonController player)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.1f);

        while (!CheckLineOfSight(player)) 
        {
            yield return Wait;
        }
    }
}

