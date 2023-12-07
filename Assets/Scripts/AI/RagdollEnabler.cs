using UnityEngine;
using UnityEngine.AI;

public class RagdollEnabler : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private Transform RagdollRoot;
    [SerializeField]
    private NavMeshAgent Agent;
    [SerializeField]
    private bool StartRagdoll = false;
    private Rigidbody[] Rigidbodies;
    private CharacterJoint[] Joints;

    private void Awake()
    {
        Rigidbodies = RagdollRoot.GetComponentsInChildren<Rigidbody>();
        Joints = RagdollRoot.GetComponentsInChildren<CharacterJoint>();
    }

    private void Start()
    {
        Vector3 shootingDirection = transform.forward; // Cambia la dirección si es necesario
        float shootingForce = 10f;

        if (StartRagdoll)
        {
            EnableRagdoll(shootingDirection, shootingForce);
        }
        else
        {
            EnableAnimator();
        }
    }

    public void EnableRagdoll(Vector3 forceDirection, float forceMagnitude)
    {
        Animator.enabled = false;
        Agent.enabled = false;
        
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = true;
        }
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;

            // Aplicar fuerza a los Rigidbody
            Rigidbodies[0].AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
        }

        
    }

    public void DisableAllRigidbodies()
    {
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

        }
    }

    public void EnableAnimator()
    {
        Animator.enabled = true;
        Agent.enabled = true;
        foreach (CharacterJoint joint in Joints)
        {
            joint.enableCollision = false;
        }
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            //rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }
}