namespace Ignite.Movement.States.Controller
{
    public interface ILimbController
    {
        public LimbMovementState CurrentState
        { get; set; }

        public void EnterLimb();
        public void UpdateLimb();
        public void ExitLimb();

        public void Lock();
    }
}
