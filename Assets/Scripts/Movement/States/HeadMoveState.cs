using UnityEngine;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public class HeadMoveState : PlayerMovementState
    {
        public HeadMoveState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            context.CameraPosition = context.CamStartPos;
            context.CameraRotation = Quaternion.identity;
        }

        public override void EnterState()
        { }
        public override void UpdateState()
        {
            if (!Context.IsMoving)
                Context.Rotation = Quaternion.AngleAxis(Context.CameraRotation.eulerAngles.y, Vector3.up);
            
            if (!IsCurrentSuperState(Context.CurrentState))
                return;

            Context.AppliedMovementX = Context.MovementInput.x * Context.RollSpeed;
            Context.AppliedMovementZ = Context.MovementInput.y * Context.RollSpeed;
            Context.AppliedMovement = Context.Rotation * Context.AppliedMovement;

            Vector3 w = Vector3.Cross(Context.AppliedMovement, Vector3.up) / Context.Radius;
            if (!Context.IsMoving)
                return;
            float theta = -(w.magnitude * Time.deltaTime);
            Vector3 a = w.normalized;
            Quaternion dQ = Quaternion.AngleAxis(theta * Mathf.Rad2Deg, a);
            Context.CameraRotation = dQ * Context.CameraRotation;
            Context.CameraPosition = Context.Position + Context.CameraRotation * new Vector3(0, 0, Context.Radius);
        }
        public override void ExitState()
        {
            Context.AppliedMovement = Vector3.zero;
        }
        public override void CheckSwitchStates()
        { }
    }
}
