using Ignite.Movement.States.Factory;
using UnityEngine;

namespace Ignite.Movement.States
{
    public class LegHopState : PlayerMovementState
    {
        float _timer;
        private float _hopDistance = 1.5f;
        private float _maxHeight = 2f;

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

            float y = Context.LegHopCurve.Evaluate(_timer) * _maxHeight;
            float x = _hopDistance / 1f;

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
