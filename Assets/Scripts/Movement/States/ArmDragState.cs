using Ignite.Movement.States.Controller;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Ignite.Movement.States
{
    public class ArmDragState : LimbMovementState
    {
        private ArmController _context;

        private bool DragFinished
        {
            get
            {
                Vector3 pos = _context.Position;
                pos.y = 0f;
                Vector3 target = _context.ShoulderPosition;
                target.y = 0f;

                float dist = (pos - target).magnitude;

                return dist <= _context.ReachDistanceRange.x;
            }
        }

        public ArmDragState(ArmController context) : base(context)
        {
            _context = context;
        }

        public override void EnterState()
        {
            if (!DragFinished)
                _context.DragSound.Play();
            _context.AppliedMovement = Vector3.zero;
        }
        public override void UpdateState()
        {
            if (DragFinished || !_context.IsDragging || _context.MoveInput.x != 0f)
            {
                SwitchState(_context.IdleState);
                return;
            }
            _context.ShoulderPosition += _context.Forward * Time.deltaTime * _context.DragSpeed;
        }
        public override void ExitState()
        {
            _context.DragSound.Pause();
        }
    }
}
