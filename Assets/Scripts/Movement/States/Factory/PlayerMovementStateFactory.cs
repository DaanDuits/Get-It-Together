using System.Collections.Generic;

namespace Ignite.Movement.States.Factory
{
    public enum EPlayerMovementStates
    {
        PlayerHeadState,
        HeadMoveState,
        PlayerLegState,
        PlayerArmState
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
                { EPlayerMovementStates.PlayerHeadState, new PlayerHeadState(context, this) },
                { EPlayerMovementStates.HeadMoveState, new HeadMoveState(context, this) },
                { EPlayerMovementStates.PlayerLegState, new PlayerLegState(context, this) },
                { EPlayerMovementStates.PlayerArmState, new PlayerArmState(context, this) }
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
        public PlayerMovementState LegState()
        {
            return _states[EPlayerMovementStates.PlayerLegState];
        }
        public PlayerMovementState ArmState()
        {
            return _states[EPlayerMovementStates.PlayerArmState];
        }
    }
}
