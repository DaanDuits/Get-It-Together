namespace Ignite.Movement.States.Controller
{
    public enum LimbType
    {
        Head,
        LeftArm, RightArm,
        LeftLeg, RightLeg
    }

    public interface ILimbController
    {
        public LimbMovementState CurrentState
        { get; set; }
        public LimbType Type 
        { get; }

        public void EnterLimb();
        public void UpdateLimb();
        public void ExitLimb();

        public void Destroy();
    }
}
