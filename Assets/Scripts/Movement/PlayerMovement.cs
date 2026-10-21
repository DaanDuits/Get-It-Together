using UnityEngine;
using UnityEngine.InputSystem;
using Ignite.Input;
using Ignite.Movement.States;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement
{
    public struct LegHopData
    {
        public float distance;
        public float height;
        public float direction;

        public LegHopData(float distance, float height, float direction)
        {
            this.distance = distance;
            this.height = height;
            this.direction = direction;
        }
    }

    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [Header("Movement Parameters")]
        [SerializeField] private float rollSpeed = 1.5f;
        [Space]
        [SerializeField] private float legRotationSpeed = 0.8f;
        [SerializeField] private float legChargeSpeed = 2f;
        [SerializeField] private Vector2 legHopHeightMinMax = new Vector2(0.2f, 0.8f);
        [SerializeField] private Vector2 legHopDistanceMinMax = new Vector2(0.5f, 1.5f);
        [SerializeField] private AnimationCurve legHopCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f) });
        [Space]
        [SerializeField] private float armSpeed = 1f;
        [Header("Possession Parameters")]
        [SerializeField] private LimbController startLimb;
        [SerializeField] private float possessionDistance = 10f;
        [SerializeField] private LayerMask possessionLayer;
        [Header("Camera Parameters")]
        [SerializeField] private Vector3 camStartPos;
        [SerializeField] private float _radius = 0.3f;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private Material completeMaterial, incompleteMaterial;

        private InputMap _inputMap;

        private PlayerMovementStateFactory _stateFactory;

        private Vector3 _appliedMovement;

        private Vector2 _mousePos;
        private bool _isMousePressed = false;

        private int _winCounter = 0;

        public AnimationCurve LegHopCurve
        { get => legHopCurve; }
        public LimbController CurrentLimb
        { get; private set; }
        public PlayerMovementState CurrentState
        { get; set; }
        public float RollSpeed
        { get => rollSpeed; }
        public float LegRotationSpeed
        { get => legRotationSpeed; }
        public float LegChargeSpeed
        { get => legChargeSpeed; }
        public Vector2 LegHopHeightMinMax
        {  get => legHopHeightMinMax; }
        public Vector2 LegHopDistanceMinMax
        { get => legHopDistanceMinMax; }
        public LegHopData LegHopData
        { get; set; }
        public float ArmSpeed
        { get => armSpeed; }
        public Vector3 CamStartPos
        { get => camStartPos; }
        public Vector2 MovementInput
        { get; private set; }
        public bool IsMoving
        { get; private set; }
        public Vector3 AppliedMovement
        {
            get => _appliedMovement;
            set => _appliedMovement = value;
        }
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
        public Vector3 Position
        { 
            get => CurrentLimb.transform.position; 
            set => CurrentLimb.transform.position = value;
        }
        public Quaternion Rotation
        { 
            get => CurrentLimb.transform.rotation; 
            set => CurrentLimb.transform.rotation = value;
        }
        public Vector3 CameraPosition
        { private get; set; }
        public Quaternion CameraRotation
        { get; set; }
        public float Radius
        { get => _radius; }
        public bool IsSpacePressed
        { get; private set; }
        public bool IsPossessionLocked
        { get; set; }

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;

            CurrentLimb = startLimb;
            _stateFactory = new PlayerMovementStateFactory(this);

            CameraPosition = CurrentLimb.transform.position + camStartPos;

            CurrentState = _stateFactory.HeadState();
            CurrentState.EnterState();
        }

        private void OnEnable()
        {
            _inputMap = new InputMap();
            _inputMap.Player.Enable();
            _inputMap.Player.AddCallbacks(this);
        }

        private void OnDisable()
        {
            _inputMap.Player.Disable();
            _inputMap.Player.RemoveCallbacks(this);
        }

        private void Update()
        {
            CurrentState.UpdateStates();
            
            CurrentLimb.Controller.Move(_appliedMovement * Time.deltaTime);

            camera.transform.position = CameraPosition;
            camera.transform.rotation = CameraRotation;
        }

        private void FixedUpdate()
        {
            Ray ray = camera.ScreenPointToRay(_mousePos);

            if (!_isMousePressed || IsPossessionLocked)
                return;

            if (Physics.Raycast(ray, out RaycastHit hit, possessionDistance, possessionLayer))
                CurrentLimb = hit.collider.gameObject.GetComponent<LimbController>();
            else
                CurrentLimb = startLimb;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((LayerMask.GetMask("Limb") & (1 << other.gameObject.layer)) > 0)
                _winCounter++;

            if (_winCounter == 5)
                winScreen.SetActive(true);

            if (CurrentLimb.GetComponentInChildren<SkinnedMeshRenderer>() == null)
                return;
            CurrentLimb.GetComponentInChildren<SkinnedMeshRenderer>().material = completeMaterial;
        }

        private void OnTriggerExit(Collider other)
        {
            if ((LayerMask.GetMask("Limb") & (1 << other.gameObject.layer)) > 0)
                _winCounter--;

            if (CurrentLimb.GetComponentInChildren<SkinnedMeshRenderer>() == null)
                return;
            CurrentLimb.GetComponentInChildren<SkinnedMeshRenderer>().material = incompleteMaterial;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = context.ReadValue<Vector2>();
            IsMoving = MovementInput.x != 0f || MovementInput.y != 0f;
        }
        public void OnMousePos(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }

        public void OnMousePressed(InputAction.CallbackContext context)
        {
            if (context.started)
                _isMousePressed = true;
            if (context.canceled)
                _isMousePressed = false;
        }

        public void OnSpacePressed(InputAction.CallbackContext context)
        {
            if (context.started)
                IsSpacePressed = true;
            if (context.canceled)
                IsSpacePressed = false;
        }
    }
}
