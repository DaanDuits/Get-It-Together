using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement.States
{
    public class LegHopState : LimbMovementState
    {
        private LegController _context;

        private float _timer;

        private float _height, _distance;

        private bool _hasStarted;
        private bool _hasPlayed;

        public LegHopState(LegController context) : base(context)
        {
            _context = context;
            _context.AnimatorBehaviour.OnHopFinished.AddListener(() => {
                SwitchState(_context.IdleState);
            });
            _context.AnimatorBehaviour.OnHopStarted.AddListener(() =>
            {
                _hasStarted = true;
            });
        }

        public override void EnterState()
        {
            _context.Animator.Play("HopBegin", 0);
            _height = Mathf.Lerp(_context.HopHeightRange.x, _context.HopHeightRange.y, _context.HopStrength);
            _distance = Mathf.Lerp(_context.HopDistanceRange.x, _context.HopDistanceRange.y, _context.HopStrength);

            _timer = 0f;
        }
        public override void UpdateState()
        {
            if (!_hasStarted)
                return;

            if (_timer == 1f)
            {
                _context.Animator.Play("Landing", 0);
                _context.AppliedMovement = Vector3.zero;
                PlayEffectOnce();
                return;
            }

            float y = 4f * (_height - 0f) * _timer * (1f - _timer) + 0f;
            float x = _distance / 1f * _context.HopDirection;

            _context.AppliedMovement = new Vector3(_context.Forward.x * x, 0, _context.Forward.z * x);
            _context.VisualPosition = new Vector3(0, y, 0);

            _timer = Mathf.Clamp01(_timer + Time.deltaTime);
        }
        public override void ExitState()
        {
            _hasPlayed = false;
            _hasStarted = false;
        }

        private void PlayEffectOnce()
        {
            if (_hasPlayed)
                return;

            _context.DustEffect.Play();
            _hasPlayed = true;
        }
    }
}
