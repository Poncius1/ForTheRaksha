using System.Collections;
using UnityEngine;

namespace MagicWand
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]

    public class Magicbody : MonoBehaviour
    {

         private int _damage = 0;
        [SerializeField] private int _objectDamage = 2;
        [SerializeField] private int _life = 4;
         private int _initialDamage = 0;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public Vector3 Position => transform.position;
        public Vector3 Velocity => _rigidbody.velocity;

        public int Damage => _damage;
        public int Life => _life;

        public int InitialDamage => _initialDamage;
        public int ObjectDamage => _objectDamage;
        public void AddForce(Vector3 force, ForceMode forceMode) => _rigidbody.AddForce(force * _rigidbody.mass, forceMode);

        private bool _locked;

        public bool Locked
        {
            get => _locked;
            
            set => _rigidbody.useGravity = !(_locked = value);
        }

        
        private void Update()
        {
            if (_life <= 0 )
            {

                Destroy(this);
      
            }
        }

        

        public void Move(Transform targetTransform)
        {
            if (_rigidbody != null)
            {
                Vector3 direction = targetTransform.position - _rigidbody.position;
                _rigidbody.velocity = direction.normalized * direction.sqrMagnitude * 10f;
                if (_rigidbody.velocity.magnitude > 100f)
                {
                    _rigidbody.velocity = _rigidbody.velocity.normalized * 100f;
                }
            }
           
        }

        public void SetDamage(int newDamage)
        {
            _damage = newDamage;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                _life = _life - Damage;
            }
        }

        

    }
}