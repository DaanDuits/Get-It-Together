using Ignite.Movement.States.Factory;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

namespace Ignite.Movement.States
{
    public class PlayerLegState : PlayerMovementState, IRootState
    {
        private float _rotation;
        private float _hopDistance = 1.5f;
        private float _maxHeight = 2f;
        private bool _jump = false;

        public PlayerLegState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            InitializeSubState();
            _rotation = Context.Rotation.eulerAngles.y;
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
            _rotation += Context.MovementInput.x * Context.LegSpeed * Time.deltaTime;
            Context.Rotation = Quaternion.AngleAxis(_rotation, Vector3.up);

            if (Context.IsSpacePressed && !_jump)
            {
                _jump = true;
                Context.IsPossessionLocked = true;
                Context.StartCoroutine(PlotParabola());
            }
        }
        public IEnumerator PlotParabola()
        {
            float time = 0f;

            while (_jump)
            {
                if (time == 1f)
                    _jump = false;

                float y = Context.LegHopCurve.Evaluate(time) * _maxHeight;
                float x = _hopDistance / 1f;

                Vector3 forward = Context.CurrentLimb.transform.forward.normalized;
                Context.AppliedMovement = new Vector3(forward.x * x, 0, forward.z * x);

                Vector3 visualPos = new Vector3(Context.Position.x, Context.Position.y + y, Context.Position.z);
                Context.CurrentLimb.VisualObject.transform.position = visualPos;

                yield return null;


                time = Mathf.Clamp01(time + Time.deltaTime);
            }
            Context.AppliedMovement = Vector3.zero; 
            Context.IsPossessionLocked = false;
        }

        public override void ExitState()
        { }
        public override void CheckSwitchStates()
        {
            if (Context.CurrentLimb.Type == ELimbType.Head)
                SwitchState(Factory.HeadState());
            if (Context.CurrentLimb.Type == ELimbType.Arm)
                SwitchState(Factory.ArmState()); 
        }
        public void InitializeSubState()
        { }
    }
}
