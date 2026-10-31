using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement.States
{
    public class LegChargeState : LimbMovementState
    {
        private LegController _context;
        private float _timer;

        public LegChargeState(LegController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        {
            _timer = 0f;

            _context.Animator.speed = 0f;
        }
        public override void UpdateState()
        {
            if (!_context.IsCharging && _context.MoveInput.y != 0f)
            {
                _context.HopStrength = _timer / _context.ChargeDuration;
                _context.HopDirection = _context.MoveInput.y;
                SwitchState(_context.HopState);
                return;
            }
            else if (!_context.IsCharging && _context.MoveInput.y == 0f)
            {
                SwitchState(_context.IdleState);
                return;
            }

            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, _context.ChargeDuration);

            _context.Animator.Play("Charge", 0, _timer / _context.ChargeDuration);
        }
        public override void ExitState()
        {
            _context.Animator.speed = 1f;
        }
    }
}
