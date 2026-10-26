using UnityEngine;

namespace Ignite
{
    public class LegHopState : LimbMovementState
    {
        private LegController _context;

        private float _timer;

        private float _height, _distance;

        public LegHopState(LegController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        {
            _height = Mathf.Lerp(_context.HopHeightRange.x, _context.HopHeightRange.y, _context.HopStrength);
            _distance = Mathf.Lerp(_context.HopDistanceRange.x, _context.HopDistanceRange.y, _context.HopStrength);
        }
        public override void UpdateState()
        {
            if (_timer == 1f)
            {
                SwitchState(_context.IdleState);
                return;
            }

            float y = 4f * (_height - 0f) * _timer * (1f - _timer) + 0f;
            float x = _distance / 1f * _context.HopDirection;

            _context.AppliedMovement = new Vector3(_context.Forward.x * x, 0, _context.Forward.z * x);
            _context.VisualPosition = new Vector3(0, y, 0);

            _timer = Mathf.Clamp01(_timer + Time.deltaTime);
        }
        public override void ExitState()
        { }
    }
}
