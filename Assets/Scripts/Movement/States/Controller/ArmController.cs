using System.Net.Http.Headers;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    public class ArmController : MonoBehaviour, ILimbController
    {
        [SerializeField] private float turnSpeed = 0.2f;
        [SerializeField] private float dragSpeed = 0.4f;
        [SerializeField] private float skinWidth = 0.05f;
        [SerializeField] private float handRadius = 0.1f;
        [SerializeField] private Vector2 reachDistanceRange = new Vector2(0.2f, 0.8f);
        [SerializeField] private LayerMask wallMask;
        [SerializeField] private Transform pivot, shoulder;
        [SerializeField] private LimbType type;

        private Vector3 _appliedMovement;

        private ArmIdleState _idleState;
        private ArmDragState _dragState;

        private bool _positionLocked;

        public LimbMovementState CurrentState
        { get; set; }
        public LimbType Type
        { get => type; }

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
        { 
            get => transform.position; 
            set => transform.position = value;
        }
        public Vector3 Forward
        { get => pivot.forward; }
        public Quaternion Rotation
        { 
            get => pivot.rotation;
            set => pivot.rotation = value; 
        }
        public Vector3 Pivot
        {
            get => pivot.position;
            set => pivot.position = value; 
        }
        public Vector3 ShoulderPosition
        { 
            get => shoulder.position; 
            set => shoulder.position = value;
        }
        public float Offset
        {
            get
            {
                return (Position - ShoulderPosition).magnitude;
            }
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
            RotationY = Rotation.eulerAngles.y;

            _idleState = new ArmIdleState(this);
            _dragState = new ArmDragState(this);
        }

        private void FixedUpdate()
        {
            float radius = (ShoulderPosition - Position).magnitude;
            if (Physics.Raycast(ShoulderPosition, transform.forward, radius + skinWidth, wallMask))
                IsDragging = false;
            else
                IsDragging = GameManager.Instance.InputMap.Player.Charge.ReadValue<float>() > 0f;

            if (!Physics.CheckSphere(ShoulderPosition, radius + skinWidth, wallMask))
                return;

            _positionLocked = true;

            Vector3 predictedPos = Position + _appliedMovement * Time.deltaTime;
            if (Physics.CheckSphere(predictedPos, handRadius, wallMask))
                return;

            _positionLocked = false;
        }

        public void EnterLimb()
        {
            CurrentState = _idleState;
            CurrentState.EnterState();
        }
        public void UpdateLimb()
        {
            CurrentState.UpdateState();

            MoveInput = GameManager.Instance.InputMap.Player.Move.ReadValue<Vector2>();

            Rotation = Quaternion.AngleAxis(RotationY, Vector3.up);
            shoulder.rotation = Rotation;

            if (!_positionLocked)
                transform.position += _appliedMovement * Time.deltaTime;
        }
        public void ExitLimb()
        {
        }

        public void Destroy()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
