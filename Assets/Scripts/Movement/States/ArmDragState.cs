using Ignite.Movement.States.Controller;
using UnityEngine;

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
            Vector3 target = _context.MovePosition;
            target.y = 0f;

            float dist = (pos - target).magnitude;

            Debug.Log(dist <= _context.ReachDistanceRange.x || !_context.IsDragging);
            if (dist <= _context.ReachDistanceRange.x || !_context.IsDragging || _context.MoveInput.x != 0f)
            {
                SwitchState(_context.IdleState);
                return;
            }
            _context.MovePosition += _context.Forward * Time.deltaTime * _context.DragSpeed;
        }
        public override void ExitState()
        { }
    }
}
