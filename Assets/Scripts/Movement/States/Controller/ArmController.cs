using System.Net.Http.Headers;
using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class ArmController : MonoBehaviour, ILimbController
    {
        [SerializeField] private float turnSpeed = 0.2f;
        [SerializeField] private float dragSpeed = 0.4f;
        [SerializeField] private Vector2 reachDistanceRange = new Vector2(0.2f, 0.8f);

        private CharacterController _controller;

        private Vector3 _appliedMovement;

        private ArmIdleState _idleState;
        private ArmDragState _dragState;

        public LimbMovementState CurrentState
        { get; set; }

        public float TurnSpeed
        { get => turnSpeed; }
        public float DragSpeed
        { get => dragSpeed; }
        public Vector2 ReachDistanceRange
        { get => reachDistanceRange; }
        public float AppliedMovementX
        {
            get => _appliedMovement.x;
            set => _appliedMovement.x = value;
        }
        public float AppliedMovementZ
        {
            get => _appliedMovement.z;
            set => _appliedMovement.z = value;
        }
        public Vector3 AppliedMovement
        {
            get => _appliedMovement;
            set => _appliedMovement = value;
        }
        public Vector2 MoveInput
        { get; private set; }
        public Vector3 Position
        { get => transform.position; }
        public Vector3 Forward
        { get => transform.forward; }
        public Quaternion Rotation
        { 
            get => transform.parent.rotation;
            set => transform.parent.rotation = value; 
        }
        public Vector3 MovePosition
        { 
            get => transform.parent.position; 
            set => transform.parent.position = value;
        }
        public float RotationY
        { get; set; }
        public bool IsDragging
        { get; private set; }

        public ArmIdleState IdleState
        { get => _idleState; }
        public ArmDragState DragState
        { get => _dragState; }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();

            RotationY = Rotation.eulerAngles.y;

            _idleState = new ArmIdleState(this);
            _dragState = new ArmDragState(this);
        }

        public void EnterLimb()
        {
            CurrentState = _idleState;
            CurrentState.EnterState();
        }
        public void UpdateLimb()
        {
            if (!enabled)
                return;
            CurrentState.UpdateState();

            MoveInput = GameManager.Instance.InputMap.Player.Move.ReadValue<Vector2>();
            IsDragging = GameManager.Instance.InputMap.Player.Charge.ReadValue<float>() > 0f;
            Rotation = Quaternion.AngleAxis(RotationY, Vector3.up);

            if (MoveInput.x == 0)
                _controller.Move(_appliedMovement * Time.deltaTime);
        }
        public void ExitLimb()
        { }

        public void Lock()
        {
            enabled = false;
        }
    }
}
