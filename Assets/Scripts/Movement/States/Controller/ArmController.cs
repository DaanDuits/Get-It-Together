using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    public class ArmController : InputListener, ILimbController
    {
        public LimbMovementState CurrentState 
        { get; set; }

        public void EnterLimb()
        { }
        public void ExitLimb()
        { }
        public void UpdateLimb()
        { }
    }
}
