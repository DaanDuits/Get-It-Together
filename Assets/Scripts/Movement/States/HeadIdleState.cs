using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement.States
{
    public class HeadIdleState : LimbMovementState
    {
        private HeadController _context;
        public HeadIdleState(HeadController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        { }
        public override void UpdateState()
        {
            if (_context.MoveInput != Vector2.zero)
                SwitchState(_context.RollState);

            _context.PanRotation = Quaternion.Euler(-_context.PanAngle(_context.MouseInput.y, _context.PanStart.x), _context.PanAngle(_context.MouseInput.x, _context.PanStart.y), 0f);
        }
        public override void ExitState()
        { }
    }
}
