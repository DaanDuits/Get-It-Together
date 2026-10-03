using Ignite.Movement.States.Factory;
using UnityEngine;

namespace Ignite.Movement.States
{
    public class PlayerLegState : PlayerMovementState, IRootState
    {
        public PlayerLegState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        {
            IsRootState = true;
        }

        public override void EnterState()
        {
            InitializeSubState();
            Context.MovementSpeed = Context.LegSpeed;
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
        }
        public void InitializeSubState()
        {
        }
    }
}
