using UnityEngine;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public class LegChargeState : PlayerMovementState
    {
        float _timer;
        public LegChargeState(PlayerMovement context, PlayerMovementStateFactory factory) : base(context, factory)
        { }

        public override void EnterState()
        {
            _timer = 0f;
        }
        public override void UpdateState()
        {
            if (!Context.IsSpacePressed && Context.MovementInput.y != 0)
            {
                float ratio = _timer / Context.LegChargeSpeed;
                float hopHeight = Mathf.Lerp(Context.LegHopHeightMinMax.x, Context.LegHopHeightMinMax.y, ratio);
                float hopDistance = Mathf.Lerp(Context.LegHopDistanceMinMax.x, Context.LegHopDistanceMinMax.y, ratio);

                Context.LegHopData = new LegHopData(hopDistance, hopHeight, Context.MovementInput.y);
                SwitchState(Factory.LegHopState());
            }
            else if (!Context.IsSpacePressed && Context.MovementInput.y == 0)
            {
                SwitchState(Factory.LegIdleState());
            }

            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, Context.LegChargeSpeed);
        }
        public override void ExitState()
        { }
    }
}
