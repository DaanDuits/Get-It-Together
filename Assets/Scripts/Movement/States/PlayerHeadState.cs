using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public class PlayerHeadState : PlayerMovementState, IRootState
    {
        public PlayerHeadState(PlayerMovement context, PlayerMovementStateFactory factory)
            : base(context, factory)
        { }

        public override void EnterState()
        {
            InitializeSubState();
        }
        public override void UpdateState()
        { 
            CheckSwitchStates();
        }
        public override void ExitState()
        { }
        public override void CheckSwitchStates()
        { }
        public void InitializeSubState()
        {
            SetSubState(Factory.HeadMoveState());
        }
    }
}
