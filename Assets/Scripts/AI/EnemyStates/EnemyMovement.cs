using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public EnemyLineOfSightChecker LineOfSightChecker;
    public NavMeshTriangulation Triangulation;
    public float UpdateSpeed = 0.1f;
    private Animator anim;
    private NavMeshAgent Agent;
    private const string IsWalking = "run";

    public EnemyState Defaultstate;

    private EnemyState _state;
    public EnemyState State
    {
        get
        {
            return _state;
        }
        set 
        {
            OnStateChange?.Invoke(_state, value);
            _state = value;
        }
    }

    public delegate void StateChangeEvent(EnemyState oldState, EnemyState newState);
    public StateChangeEvent OnStateChange;
    public float IdleLocationRadius = 4f;
    public float IdleMovespeedMultiplier = 0.5f;
    [SerializeField] private int WaypointIndex = 0;
    private Coroutine FollowCoroutine;
    public Vector3[] Waypoints = new Vector3[4];
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        LineOfSightChecker.OnGainSight += HandleGainSight;
        LineOfSightChecker.OnLosesSight += HandleLosesSight;


        OnStateChange += HandleStateChange;
    }

    private void HandleGainSight(ThirdPersonController player)
    {
        State = EnemyState.Chase;
    }
    private void HandleLosesSight(ThirdPersonController player)
    {
        State = Defaultstate;
    }

    private void OnDisable()
    {
        _state = Defaultstate;
    }
    private void Update()
    {
        anim.SetBool(IsWalking, Agent.velocity.magnitude > 0.1f);
    }

    public void Spawn()
    {
        for (int i = 0; i < Waypoints.Length; i++)
        {
            NavMeshHit Hit;
            if (NavMesh.SamplePosition(Triangulation.vertices[Random.Range(0,Triangulation.vertices.Length)],out Hit,2f,Agent.areaMask))
            {
                Waypoints[i] = Hit.position;
            }
            else
            {
                Debug.LogError("Unable to find Position for near Triangulation vertex!");
            }
        }
        OnStateChange?.Invoke(EnemyState.Spawn, Defaultstate);
    }   
    


    private void HandleStateChange(EnemyState oldState, EnemyState newState)
    {
        if (oldState != newState) 
        {
            if (FollowCoroutine != null)
            {
                StopCoroutine(FollowCoroutine);
            }

            if (oldState == EnemyState.Idle)
            {
                Agent.speed /= IdleMovespeedMultiplier;
            }

            switch (newState)
            {
               
                case EnemyState.Idle:
                    FollowCoroutine = StartCoroutine(DoIdleMotion());
                    break;
                case EnemyState.Patrol:
                    FollowCoroutine = StartCoroutine(DoPatrolMotion());
                    break;
                case EnemyState.Chase:
                    FollowCoroutine = StartCoroutine(FollowTarget());
                    break; 
                default:
                    break;
            }
        }
    }

    private IEnumerator DoIdleMotion()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        Agent.speed *= IdleMovespeedMultiplier;

        while (true)
        {
            if (!Agent.enabled || !Agent.isOnNavMesh)
            {
                yield return Wait;
            }
            else if(Agent.remainingDistance <= Agent.stoppingDistance)
            {
                Vector2 point = Random.insideUnitCircle * IdleLocationRadius;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(Agent.transform.position + new Vector3(point.x, 0,point.y), out hit, 2f,Agent.areaMask))
                {
                    Agent.SetDestination(hit.position);
                }
            }

            yield return Wait;

        }
    }

    private IEnumerator DoPatrolMotion()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        yield return new WaitUntil(() => Agent.enabled && Agent.isOnNavMesh);
        Agent.SetDestination(Waypoints[WaypointIndex]);
        while (true) 
        {
            if (Agent.isOnNavMesh && Agent.enabled && Agent.remainingDistance <= Agent.stoppingDistance)
            {
                WaypointIndex++;

                if (WaypointIndex >= Waypoints.Length)
                {
                    WaypointIndex = 0;
                }
                Agent.SetDestination(Waypoints[WaypointIndex]);
            }
            yield return Wait;
        }
    }




    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        while (enabled)
        {
           
            Agent.SetDestination(target.transform.position);

            yield return Wait;
        }
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < Waypoints.Length; i++)
        {
            Gizmos.DrawWireSphere(Waypoints[i], 0.35f);
            if (i + 1 < Waypoints.Length)
            {
                Gizmos.DrawLine(Waypoints[i], Waypoints[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(Waypoints[i], Waypoints[0]);
            }
        }
    }
}
