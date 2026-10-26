using UnityEngine;

namespace Ignite
{
    public interface ILimbController
    {
        public LimbMovementState CurrentState
        { get; set; }

        public void EnterLimb();
        public void UpdateLimb();
        public void ExitLimb();
    }
}
