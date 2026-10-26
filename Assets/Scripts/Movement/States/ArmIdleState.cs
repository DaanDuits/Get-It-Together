using Ignite.Movement.States.Controller;
using UnityEngine;

namespace Ignite.Movement.States
{
    public class ArmIdleState : LimbMovementState
    {
        private ArmController _context;

        public ArmIdleState(ArmController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        { }
        public override void UpdateState()
        {
            _context.RotationY += _context.MoveInput.x * _context.TurnSpeed * 360f * Time.deltaTime;

            Vector3 pos = _context.Position;
            pos.y = 0f;
            Vector3 target = _context.MovePosition;
            target.y = 0f;

            float dist = (pos - target).magnitude;
            float input = _context.MoveInput.y;

            if ((dist < _context.ReachDistanceRange.x && input < 0f) || (dist > _context.ReachDistanceRange.y && input > 0f))
            {
                _context.AppliedMovement = Vector3.zero;
                return;
            }

            _context.AppliedMovement = new Vector3(_context.Forward.x * input, 0f, _context.Forward.z * input);

            if (_context.IsDragging)
                SwitchState(_context.DragState);
        }
        public override void ExitState()
        { }
    }
}
