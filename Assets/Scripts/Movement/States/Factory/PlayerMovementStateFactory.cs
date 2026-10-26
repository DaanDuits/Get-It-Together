using System.Collections.Generic;

namespace Ignite.Movement.States.Factory
{
    public enum EPlayerMovementStates
    {
        PlayerLegState,
        LegIdleState,
        LegChargeState,
        LegHopState,
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
                { EPlayerMovementStates.PlayerLegState, new PlayerLegState(context, this) },
                { EPlayerMovementStates.LegIdleState, new LegIdleState(context, this) },
                { EPlayerMovementStates.LegChargeState, new LegChargeState(context, this) },
                { EPlayerMovementStates.LegHopState, new LegHopState(context, this) },
                { EPlayerMovementStates.PlayerArmState, new PlayerArmState(context, this) }
            };
        }

        public PlayerMovementState LegState()
        {
            return _states[EPlayerMovementStates.PlayerLegState];
        }
        public PlayerMovementState LegIdleState()
        {
            return _states[EPlayerMovementStates.LegIdleState];
        }
        public PlayerMovementState LegChargeState()
        {
            return _states[EPlayerMovementStates.LegChargeState];
        }
        public PlayerMovementState LegHopState()
        {
            return _states[EPlayerMovementStates.LegHopState];
        }
        public PlayerMovementState ArmState()
        {
            return _states[EPlayerMovementStates.PlayerArmState];
        }
    }
}
