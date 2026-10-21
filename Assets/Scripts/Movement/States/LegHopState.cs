using UnityEngine;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public class LegHopState : PlayerMovementState
    {
        float _timer;

        public LegHopState(PlayerMovement context, PlayerMovementStateFactory factory) : base(context, factory)
        { }

        public override void EnterState()
        {
            _timer = 0f;
        }
        public override void UpdateState()
        {
            if (_timer == 1f)
            {
                SwitchState(Factory.LegIdleState());
                return;
            }

            float y = Context.LegHopCurve.Evaluate(_timer) * Context.LegHopData.height;
            float x = Context.LegHopData.distance / 1f * Context.LegHopData.direction;

            Vector3 forward = Context.CurrentLimb.transform.forward.normalized;
            Context.AppliedMovement = new Vector3(forward.x * x, 0, forward.z * x);

            Vector3 visualPos = new Vector3(Context.Position.x, Context.Position.y + y, Context.Position.z);
            Context.CurrentLimb.VisualObject.transform.position = visualPos;

            _timer = Mathf.Clamp01(_timer + Time.deltaTime);
        }
        public override void ExitState()
        {
            Context.AppliedMovement = Vector3.zero;
            Context.IsPossessionLocked = false;
        }
        public override void CheckSwitchStates()
        { }
    }
}
