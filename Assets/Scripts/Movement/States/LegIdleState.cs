using Ignite.Movement.States.Factory;
using UnityEngine;

namespace Ignite.Movement.States
{
    public class LegIdleState : PlayerMovementState
    {
        private float _rotation; 

        public LegIdleState(PlayerMovement context, PlayerMovementStateFactory factory) : base(context, factory)
        { }

        public override void EnterState()
        {
            _rotation = Context.Rotation.eulerAngles.y;
        }
        public override void UpdateState()
        {
            _rotation += Context.MovementInput.x * Context.LegSpeed * Time.deltaTime;
            Context.Rotation = Quaternion.AngleAxis(_rotation, Vector3.up);

            if (Context.IsSpacePressed)
            {
                Context.IsPossessionLocked = true;
                SwitchState(Factory.LegHopState());
            }
        }
        public override void ExitState()
        { }
    }
}
