using Ignite.Movement.States.Factory;

namespace Ignite.Movement.States
{
    public abstract class PlayerMovementState 
    {
        private bool _isRootState = false;
        private PlayerMovement _context;
        private PlayerMovementStateFactory _factory;

        private PlayerMovementState _currentSuperState;
        private PlayerMovementState _currentSubState;

        protected bool IsRootState 
        { set => _isRootState = value; }
        protected PlayerMovement Context
        { get => _context; }
        protected PlayerMovementStateFactory Factory
        { get => _factory; }

        public PlayerMovementState(PlayerMovement context, PlayerMovementStateFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState(); 
        public abstract void CheckSwitchStates();

        public void UpdateStates()
        {
            UpdateState();
            _currentSubState?.UpdateState();
        }
        public void ExitStates()
        {
            ExitState();
            _currentSubState?.ExitState();
        }
        protected void SwitchState(PlayerMovementState newState)
        {
            ExitStates();

            newState.EnterState();

            if (_isRootState)
                _context.CurrentState = newState;

            _currentSuperState?.SetSubState(newState);
        }
        protected void SetSuperState(PlayerMovementState newSuperState)
        {
            _currentSuperState = newSuperState;
        }
        protected void SetSubState(PlayerMovementState newSubState)
        {
            _currentSubState = newSubState;
            _currentSubState.SetSuperState(this);
        }
    }
}
