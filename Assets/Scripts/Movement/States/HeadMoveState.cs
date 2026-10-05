using UnityEngine;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public class HeadMoveState : PlayerMovementState
    {
        private bool _exit;

        public HeadMoveState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            context.CameraPosition = context.CamStartPos;
            context.Rotation = Quaternion.identity;
        }

        public override void EnterState()
        {
            Context.MovementSpeed = Context.RollSpeed;
            _exit = false;
        }
        public override void UpdateState()
        {
            if (_exit)
                return;
            Vector3 w = Vector3.Cross(Context.AppliedMovement, Vector3.up) / Context.Radius;
            if (Context.AppliedMovement == Vector3.zero)
                return;
            float theta = -(w.magnitude * Time.deltaTime);
            Vector3 a = w.normalized;
            Quaternion dQ = Quaternion.AngleAxis(theta * Mathf.Rad2Deg, a);
            Context.Rotation = dQ * Context.Rotation;
            Context.CameraPosition = Context.Position + Context.Rotation * new Vector3(0, 0, Context.Radius);
        }
        public override void ExitState()
        { 
            _exit = true;
        }
        public override void CheckSwitchStates()
        { }
    }
}
