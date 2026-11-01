using Ignite.Movement.States.Controller;
using UnityEngine;

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
        }
        public override void ExitState()
        { }
    }
}
