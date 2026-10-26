using UnityEngine;

namespace Ignite
{
    public class HeadRollState : LimbMovementState
    {
        private HeadController _context;
        public HeadRollState(HeadController context) : base(context)
        { 
            _context = context;
        }

        public override void EnterState()
        { }
        public override void UpdateState()
        {
            _context.AppliedMovementX = _context.MoveInput.x;
            _context.AppliedMovementZ = _context.MoveInput.y;
            _context.AppliedMovement = _context.Rotation * _context.AppliedMovement;

            if (!(_context.MoveInput.x != 0f || _context.MoveInput.y != 0f))
            {
                _context.Rotation = Quaternion.AngleAxis(_context.CameraRotation.eulerAngles.y, Vector3.up);
                return;
            }
            Vector3 w = Vector3.Cross(_context.AppliedMovement, Vector3.up) / _context.Radius;
            float theta = -(w.magnitude * Time.deltaTime) * Mathf.Rad2Deg;
            Quaternion dQ = Quaternion.AngleAxis(theta, w.normalized);
            _context.CameraRotation = dQ * _context.CameraRotation;
            _context.CameraPosition = _context.Position + _context.CameraRotation * new Vector3(0, 0, _context.Radius);
        }
        public override void ExitState()
        { }
    }
}
