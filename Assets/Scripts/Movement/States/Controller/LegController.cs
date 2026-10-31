using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class LegController : MonoBehaviour, ILimbController
    {
        [SerializeField] private float turnSpeed = 0.4f;
        [SerializeField] private float chargeDuration = 1f;
        [SerializeField] private Vector2 hopHeightRange = new Vector2(0.2f, 0.8f);
        [SerializeField] private Vector2 hopDistanceRange = new Vector2(0.5f, 1.5f);
        [SerializeField] private ParticleSystem dustEffect;
        [SerializeField] private Transform visualTransform;

        private CharacterController _controller;
        private Vector3 _appliedMovement;

        private LegIdleState _idleState;
        private LegChargeState _chargeState;
        private LegHopState _hopState;

        private bool _isActive;

        public LimbMovementState CurrentState
        { get; set; }

        public float TurnSpeed
        { get => turnSpeed; }
        public float ChargeDuration
        { get => chargeDuration; }
        public Vector2 HopHeightRange
        { get => hopHeightRange; }
        public Vector2 HopDistanceRange
        { get => hopDistanceRange; }
        public ParticleSystem DustEffect
        { get => dustEffect; }
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
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        public Vector3 Forward
        { get => transform.forward; }
        public Vector3 VisualPosition
        { set => visualTransform.localPosition = value; }
        public float RotationY 
        { get; set; }
        public bool IsCharging
        { get; private set; }
        public float HopStrength
        { get; set; }
        public float HopDirection
        { get; set; }

        public LegIdleState IdleState
        {  get => _idleState; }
        public LegChargeState ChargeState
        { get => _chargeState; }
        public LegHopState HopState
        { get => _hopState; }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();

            RotationY = transform.rotation.eulerAngles.y;

            _idleState = new LegIdleState(this);
            _chargeState = new LegChargeState(this);
            _hopState = new LegHopState(this);
        }

        private void Update()
        {
            if (_appliedMovement != Vector3.zero && !_isActive)
            {
                UpdateLimb();
            }
        }

        public void EnterLimb()
        {
            CurrentState = _idleState;
            CurrentState.EnterState();

            _isActive = true;
        }
        public void UpdateLimb()
        {
            if (!enabled)
                return;
            CurrentState.UpdateState();

            IsCharging = GameManager.Instance.InputMap.Player.Charge.ReadValue<float>() == 1f;
            MoveInput = GameManager.Instance.InputMap.Player.Move.ReadValue<Vector2>();

            _controller.Move(_appliedMovement * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(RotationY, Vector3.up);
        }
        public void ExitLimb()
        { 
            _isActive = false;
        }

        public void Lock()
        {
            enabled = false;
        }
    }
}
