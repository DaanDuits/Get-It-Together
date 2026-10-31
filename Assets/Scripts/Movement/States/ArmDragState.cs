using Ignite.Movement.States.Controller;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Ignite.Movement.States
{
    public class ArmDragState : LimbMovementState
    {
        private ArmController _context;

        public ArmDragState(ArmController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        { }
        public override void UpdateState()
        {
            Vector3 pos = _context.Position;
            pos.y = 0f;
            Vector3 target = _context.ShoulderPosition;
            target.y = 0f;

            float dist = (pos - target).magnitude;

            if (dist <= _context.ReachDistanceRange.x || !_context.IsDragging || _context.MoveInput.x != 0f)
            {
                SwitchState(_context.IdleState);
                return;
            }
            _context.ShoulderPosition += _context.Forward * Time.deltaTime * _context.DragSpeed;
        }
        public override void ExitState()
        { }
    }
}
