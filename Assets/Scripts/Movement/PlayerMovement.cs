using System.Collections.Generic;
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
        [SerializeField] private float armSpeed = 1f;
        [Header("Possession Parameters")]
        [SerializeField] private LimbController startLimb;
        [SerializeField] private float possessionDistance = 10f;
        [SerializeField] private LayerMask possessionLayer;
        [Header("Camera Parameters")]
        [SerializeField] private Vector3 camStartPos;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject winScreen;

        private LimbController _currentLimb;
        private InputMap _inputMap;

        private PlayerMovementStateFactory _stateFactory;
        private PlayerMovementState _currentState;

        private Vector2 _movementInput;
        private Vector2 _mousePos;

        private Vector3 _appliedMovement;
        private float _movementSpeed;

        private Vector3 _cameraPosition;
        private Quaternion _rotation;

        private float _radius = 0.3f;

        private bool _mousePressed = false;

        private int _winCounter = 0;

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
        public float MovementSpeed
        { set => _movementSpeed = value; }
        public Vector3 CamStartPos
        { get => camStartPos; }
        public Vector2 MovementInput
        { get => _movementInput; }
        public Vector3 AppliedMovement
        { get => _appliedMovement; }
        public Vector3 Position
        { get => _currentLimb.transform.position; }
        public Vector3 CameraPosition
        { set => _cameraPosition = value; }
        public Quaternion Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }
        public float Radius
        { get => _radius; }

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
            _appliedMovement.x = _movementInput.x * _movementSpeed;
            _appliedMovement.z = _movementInput.y * _movementSpeed;

            _currentLimb.Controller.Move(_appliedMovement * Time.deltaTime);

            camera.transform.position = _cameraPosition;
            camera.transform.rotation = _rotation;

            _currentState.UpdateStates();
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
        }

        private void OnTriggerExit(Collider other)
        {
            if ((LayerMask.GetMask("Limb") & (1 << other.gameObject.layer)) > 0)
                _winCounter--;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
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
    }
}
