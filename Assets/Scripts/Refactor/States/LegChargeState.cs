using UnityEngine;

namespace Ignite
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
        }
        public override void UpdateState()
        {
            if (!_context.IsCharging && _context.MoveInput.y != 0f)
            {
                _context.HopStrength = _timer / _context.ChargeDuration;
                _context.HopDirection = _context.MoveInput.y;
                SwitchState(_context.HopState);
            }
            else if (!_context.IsCharging && _context.MoveInput.y == 0f)
            {
                SwitchState(_context.IdleState);
            }

            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0f, _context.ChargeDuration);
        }
        public override void ExitState()
        { }
    }
}
