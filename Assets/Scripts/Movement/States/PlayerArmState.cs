using Ignite.Movement;
using Ignite.Movement.States;
using Ignite.Movement.States.Factory;
using UnityEngine;

namespace Ignite
{
    public class PlayerArmState : PlayerMovementState, IRootState
    {
        private float _rotation;
        private float _maxDistance = 0.8f, _minDistance = 0.1f;

        public PlayerArmState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            InitializeSubState();
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            _rotation += Context.MovementInput.x * Context.LegRotationSpeed * 360 * Time.deltaTime;
            Context.CurrentLimb.transform.parent.rotation = Quaternion.Euler(0, _rotation, 0);

            float distance = (Context.Position - Context.CurrentLimb.VisualObject.transform.position).magnitude; 

            
        }
        public override void ExitState()
        { }
        public override void CheckSwitchStates()
        {
            if (Context.CurrentLimb.Type == ELimbType.Head)
                SwitchState(Factory.HeadState());
            if (Context.CurrentLimb.Type == ELimbType.Leg)
                SwitchState(Factory.LegState());
        }
        public void InitializeSubState()
        { }
    }
}
