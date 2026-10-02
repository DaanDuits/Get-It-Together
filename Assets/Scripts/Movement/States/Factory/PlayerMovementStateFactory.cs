using System.Collections.Generic;

namespace Ignite.Movement.States.Factory
{
    public enum EPlayerMovementStates
    {
        PlayerHeadState,
        HeadMoveState
    }

    public class PlayerMovementStateFactory
    {
        private PlayerMovement _context;
        private Dictionary<EPlayerMovementStates, PlayerMovementState> _states;

        public PlayerMovementStateFactory(PlayerMovement context)
        {
            _context = context;

            _states = new Dictionary<EPlayerMovementStates, PlayerMovementState>
            {
                {EPlayerMovementStates.PlayerHeadState, new PlayerHeadState(context, this) },
                {EPlayerMovementStates.HeadMoveState, new HeadMoveState(context, this) }
            };
        }

        public PlayerMovementState HeadState()
        {
            return _states[EPlayerMovementStates.PlayerHeadState];
        }
        public PlayerMovementState HeadMoveState()
        {
            return _states[EPlayerMovementStates.HeadMoveState];
        }
    }
}
