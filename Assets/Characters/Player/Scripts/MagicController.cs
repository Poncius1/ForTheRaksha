using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;


namespace MagicWand
{
    public class MagicController : MonoBehaviour
    {

        [Header("Settings")]
        [SerializeField] private float _range = 10f;
        [SerializeField] private float  _radius = 10f;
        [SerializeField] private float _throwForce = 10f;
        [SerializeField] private int _dragTime = 5;

        [SerializeField] private Transform _pivot;
        [SerializeField] private Transform _dragpivot;
        [SerializeField] private ParticleSystem _particleTrail;
        [SerializeField] private AudioClip _dragSound;
        private Animator _animator;
        private ThirdPersonController thirdPersonController;
        private StarterAssetsInputs starterAssetsInputs;

        public float Range => _range;
        public float Radius => _radius;
        public Transform Obj => ActualOutline;

        public ParticleSystem trailParticle => _particleTrail;
        public float ThrowForce => _throwForce;
        public Ray PivotRay => new(_pivot.position, _pivot.forward);

        public Animator anim => _animator;
        public StarterAssetsInputs assetsInputs => starterAssetsInputs;
        public AudioClip dragSound => _dragSound;



        public Transform DragPoint => _dragpivot;

        public Vector3 PivotPosition => _pivot.position;
        public Vector3 PivotDirection => _pivot.forward;

        public  Transform ActualOutline { get; set; }
        public Magicbody CurrentMagicbody { get; set; }

        public MagicState Idle { get; private set; }
        public MagicState Ready { get; private set; }
        public MagicState Drag { get; private set; }
        public MagicState Throw { get; private set; }
        public MagicState DryShot { get; private set; }
            
        private void Awake()
        {
            
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            _animator = GetComponent<Animator>();



        Idle = new MagicStateIdle(this);
            Drag = new MagicStateDrag(this);
            Throw = new MagicStateThrow(this);
            Ready = new MagicStateReady(this);
            DryShot = new MagicStateDryShot(this);

            SetState(Idle);
        }

        public MagicState _currentState;

        public void SetState(MagicState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
            
        }

        private void Update() => _currentState?.Update();
        private void FixedUpdate() => _currentState?.FixedUpdate();


        public IEnumerator TimeOnDrag()
        {
            yield return new WaitForSeconds(_dragTime);
            _particleTrail.Stop();
            SetState(Idle);
            Debug.Log("Tiempo en Drag Acabado");
            

        }
        
        
    }
}