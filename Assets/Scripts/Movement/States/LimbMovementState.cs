using Ignite.Movement.States.Controller;

namespace Ignite.Movement.States
{
    public abstract class LimbMovementState
    {
        private ILimbController _root;

        public LimbMovementState(ILimbController root)
        {
            _root = root;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        protected void SwitchState(LimbMovementState newState)
        {
            ExitState();

            _root.CurrentState = newState;
            newState.EnterState();
        }
    }
}
