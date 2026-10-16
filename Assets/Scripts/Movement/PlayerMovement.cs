using UnityEngine;
using UnityEngine.InputSystem;
using Ignite.Input;
using Ignite.Movement.States;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement
{
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [Header("Movement Parameters")]
        [SerializeField] private float rollSpeed = 1.5f;
        [SerializeField] private float legSpeed = 2f;
        [SerializeField] private AnimationCurve legHopCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f) });
        [SerializeField] private float armSpeed = 1f;
        [Header("Possession Parameters")]
        [SerializeField] private LimbController startLimb;
        [SerializeField] private float possessionDistance = 10f;
        [SerializeField] private LayerMask possessionLayer;
        [Header("Camera Parameters")]
        [SerializeField] private Vector3 camStartPos;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private Material completeMaterial, incompleteMaterial;

        private LimbController _currentLimb;
        private InputMap _inputMap;

        private PlayerMovementStateFactory _stateFactory;
        private PlayerMovementState _currentState;

        private Vector2 _movementInput;
        private bool _isMoving = false;
        private Vector2 _mousePos;

        private Vector3 _appliedMovement;

        private Vector3 _cameraPosition;
        private Quaternion _cameraRotation;

        private float _radius = 0.3f;

        private bool _mousePressed = false;
        private bool _spacePressed = false;

        private int _winCounter = 0;

        public AnimationCurve LegHopCurve
        { get => legHopCurve; }
        public LimbController CurrentLimb
        { get => _currentLimb; }
        public PlayerMovementState CurrentState
        {
            get => _currentState;
            set => _currentState = value; 
        }
        public float RollSpeed
        { get => rollSpeed; }
        public float LegSpeed
        { get => legSpeed; }
        public float ArmSpeed
        { get => armSpeed; }
        public Vector3 CamStartPos
        { get => camStartPos; }
        public Vector2 MovementInput
        { get => _movementInput; }
        public bool IsMoving
        { get => _isMoving; }
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
            get => _currentLimb.transform.position; 
            set => _currentLimb.transform.position = value;
        }
        public Quaternion Rotation
        { 
            get => _currentLimb.transform.rotation; 
            set => _currentLimb.transform.rotation = value;
        }
        public Vector3 CameraPosition
        { set => _cameraPosition = value; }
        public Quaternion CameraRotation
        {
            get => _cameraRotation;
            set => _cameraRotation = value;
        }
        public float Radius
        { get => _radius; }
        public bool SpacePressed
        { get => _spacePressed; }

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;

            _currentLimb = startLimb;
            _stateFactory = new PlayerMovementStateFactory(this);

            _cameraPosition = _currentLimb.transform.position + camStartPos;

            _currentState = _stateFactory.HeadState();
            _currentState.EnterState();
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
            _currentState.UpdateStates();
            
            _currentLimb.Controller.Move(_appliedMovement * Time.deltaTime);

            camera.transform.position = _cameraPosition;
            camera.transform.rotation = _cameraRotation;
        }

        private void FixedUpdate()
        {
            Ray ray = camera.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit, possessionDistance, possessionLayer) && _mousePressed)
                _currentLimb = hit.collider.gameObject.GetComponent<LimbController>();
            else if (_mousePressed)
                _currentLimb = startLimb;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((LayerMask.GetMask("Limb") & (1 << other.gameObject.layer)) > 0)
                _winCounter++;

            if (_winCounter == 5)
                winScreen.SetActive(true);

            if (_currentLimb.GetComponentInChildren<SkinnedMeshRenderer>() == null)
                return;
            _currentLimb.GetComponentInChildren<SkinnedMeshRenderer>().material = completeMaterial;
        }

        private void OnTriggerExit(Collider other)
        {
            if ((LayerMask.GetMask("Limb") & (1 << other.gameObject.layer)) > 0)
                _winCounter--;

            if (_currentLimb.GetComponentInChildren<SkinnedMeshRenderer>() == null)
                return;
            _currentLimb.GetComponentInChildren<SkinnedMeshRenderer>().material = incompleteMaterial;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
            _isMoving = MovementInput.x != 0f || MovementInput.y != 0f;
        }
        public void OnMousePos(InputAction.CallbackContext context)
        {
            _mousePos = context.ReadValue<Vector2>();
        }

        public void OnMousePressed(InputAction.CallbackContext context)
        {
            if (context.started)
                _mousePressed = true;
            if (context.canceled)
                _mousePressed = false;
        }

        public void OnSpacePressed(InputAction.CallbackContext context)
        {
            if (context.started)
                _spacePressed = true;
            if (context.canceled)
                _spacePressed = false;
        }
    }
}
