
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MagicWand
{
    public abstract class MagicState  
    {
        protected MagicController _controller;
        protected MagicState(MagicController controller) => _controller = controller;

        public virtual void Enter() 
        {

            
        }
        public virtual void Update() 
        {

        }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }



    public abstract class MagicStateIdleBase : MagicState
    {
        protected MagicStateIdleBase(MagicController controller) : base(controller) { }

        protected bool TryGetBody(out Magicbody gravibody)
        {
            gravibody = null;
            

            if (Physics.SphereCast(_controller.PivotRay, _controller.Radius ,out RaycastHit hit, _controller.Range))
            {

                
                Debug.DrawRay(_controller.PivotPosition, _controller.PivotDirection *_controller.Range,Color.red);
                
                if (hit.collider.TryGetComponent(out gravibody))
                {
                    _controller.ActualOutline = hit.transform;
                    _controller.ActualOutline.GetComponent<Outline>().enabled = true;
                    return true;
                    
                }
                
            }
            if (_controller.ActualOutline != null)
            {
                _controller.ActualOutline.GetComponent<Outline>().enabled = false;
                _controller.ActualOutline = null;
            }


            return false;
        }

        
        
    }




    public sealed class MagicStateIdle : MagicStateIdleBase
    {
        public MagicStateIdle(MagicController controller) : base(controller) { }

        public override void Enter()
        {
            base.Enter();

            
            _controller.CurrentMagicbody = null;
            

        }

        public override void Update()
        {
            base.Update();

            if (TryGetBody(out Magicbody body))
            {
                _controller.CurrentMagicbody = body;
                
                _controller.SetState(_controller.Ready);
            }
            
            
            else
            {
                
                if (_controller.assetsInputs.aim || _controller.assetsInputs.shoot)
                {
                    _controller.SetState(_controller.DryShot);
                }
            }
            
        }
    }

    public sealed class MagicStateReady : MagicStateIdleBase
    {
        public MagicStateReady(MagicController controller) : base(controller) { }

        public override void Enter()
        {
            base.Enter();
            

        }

        public override void Update()
        {
            base.Update();

            if (!TryGetBody(out Magicbody body)) _controller.SetState(_controller.Idle);
            else if (body != _controller.CurrentMagicbody) _controller.CurrentMagicbody = body;

            else
            {
                if (_controller.assetsInputs.aim && !_controller.assetsInputs.shoot)
                {
                    
                    _controller.SetState(_controller.Drag);
                    
                }
                else if (_controller.assetsInputs.shoot && !_controller.assetsInputs.aim) _controller.SetState(_controller.Throw);
            } 
            

        }

        public override void Exit()
        {
            base.Exit();

            
        }
    }

    public sealed class MagicStateDrag : MagicState
    {
        public MagicStateDrag(MagicController controller) : base(controller) { }

        public override void Enter()
        {
            base.Enter();

            _controller.CurrentMagicbody.Locked = true;
            _controller.CurrentMagicbody.SetDamage(_controller.CurrentMagicbody.ObjectDamage);
            _controller.trailParticle.Play();
            SoundManager.Instance.PlayEffect(_controller.dragSound);

        }

        public override void Update()
        {
            base.Update();

            

            if (!_controller.assetsInputs.aim && !_controller.assetsInputs.shoot)
            {

                _controller.SetState(_controller.Idle);
               

            }
            
            
            if (_controller.assetsInputs.aim && _controller.assetsInputs.shoot)
            {

                _controller.anim.SetTrigger("Throw");
                _controller.SetState(_controller.Throw);
            }
            

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_controller.CurrentMagicbody != null)
            {
                _controller.CurrentMagicbody.Move(_controller.DragPoint);
                
               
            }
            
        }

        public override void Exit()
        {
            base.Exit();

            
            _controller.CurrentMagicbody.SetDamage(_controller.CurrentMagicbody.InitialDamage);
            _controller.CurrentMagicbody.Locked = false;
            _controller.ActualOutline.GetComponent<Outline>().enabled = false;
            _controller.trailParticle.Stop();
        }
        
    }
    
    
    public sealed class MagicStateThrow : MagicState
    {
        public MagicStateThrow(MagicController controller) : base(controller) { }

        public override void Enter()
        {
            base.Enter();

            _controller.CurrentMagicbody.AddForce(_controller.PivotDirection * _controller.ThrowForce, ForceMode.Impulse);
            _controller.CurrentMagicbody.SetDamage(_controller.CurrentMagicbody.ObjectDamage);
            _controller.ActualOutline.GetComponent<Outline>().enabled = false;
            _controller.SetState(_controller.Idle);
        }

        public override void Exit()
        {
            base.Exit();
            
            
            
        }
    }

    public sealed class MagicStateDryShot : MagicState
    {
        public MagicStateDryShot(MagicController controller) : base(controller) { }

        public override void Enter()
        {
            base.Enter();

            

            _controller.SetState(_controller.Idle);
            
        }
    }
}