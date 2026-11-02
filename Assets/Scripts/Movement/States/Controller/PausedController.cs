using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    public class PauseController : MonoBehaviour, ILimbController, IPossessable
    {
        public LimbMovementState CurrentState 
        { get; set; }

        public LimbType Type
        { get; }

        public ILimbController Controller
        { get => this; }

        public void Destroy()
        { }

        public void EnterLimb()
        { }

        public void ExitLimb()
        { }

        public void UpdateLimb()
        { }
    }
}
