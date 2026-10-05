using Ignite.Movement;
using Ignite.Movement.States;
using Ignite.Movement.States.Factory;
using UnityEngine;

namespace Ignite
{
    public class PlayerArmState : PlayerMovementState, IRootState
    {
        public PlayerArmState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            InitializeSubState();
            Context.MovementSpeed = Context.ArmSpeed;
        }
        public override void UpdateState()
        {
            CheckSwitchStates();
        }
        public override void ExitState()
        { }
        public override void CheckSwitchStates()
        {
            if (Context.CurrentLimb.Type == ELimbType.Head)
                SwitchState(Factory.HeadState());
            if (Context.CurrentLimb.Type == ELimbType.Leg)
                SwitchState(Factory.LegState());
        }
        public void InitializeSubState()
        { }
    }
}
