using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement.States
{
    public class LegIdleState : LimbMovementState
    {
        private LegController _context;

        public LegIdleState(LegController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        {
            _context.Animator.Play("Idle", 0);
        }
        public override void UpdateState()
        {
            _context.RotationY += _context.MoveInput.x * _context.TurnSpeed * 360f * Time.deltaTime;

            if (_context.IsCharging)
                SwitchState(_context.ChargeState);
        }
        public override void ExitState()
        { }
    }
}
