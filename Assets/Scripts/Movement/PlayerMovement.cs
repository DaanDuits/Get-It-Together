using UnityEngine;
using UnityEngine.InputSystem;
using Ignite.Input;
using Ignite.Movement.States;
using Ignite.Movement.States.Factory;

namespace Ignite.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        [SerializeField] private Vector3 camStartPos;
        [SerializeField] private float rollSpeed;

        private CharacterController _controller;
        private InputMap _inputMap;

        private PlayerMovementStateFactory _stateFactory;
        private PlayerMovementState _currentState;

        private Vector2 _movementInput;

        private Vector3 _appliedMovement;

        private Vector3 _cameraPosition;
        private Quaternion _rotation;

        private float _radius = 0.5f;

        public PlayerMovementState CurrentState
        {
            get => _currentState;
            set => _currentState = value; 
        }
        public Vector3 CamStartPos
        { get => camStartPos; }
        public Vector3 AppliedMovement
        {
            get => _appliedMovement;
        }
        public Vector3 Position
        {
            get => transform.position;
        }
        public Vector3 CameraPosition
        {
            get => _cameraPosition;
            set => _cameraPosition = value;
        }
        public Quaternion Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }
        public float Radius
        {
            get => _radius;
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _stateFactory = new PlayerMovementStateFactory(this);

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
            _appliedMovement.x = _movementInput.x * rollSpeed;
            _appliedMovement.z = _movementInput.y * rollSpeed;

            _appliedMovement = Quaternion.Euler(0, transform.eulerAngles.y, 0) * _appliedMovement; 

            _controller.Move(_appliedMovement * Time.deltaTime);

            Camera.main.transform.position = _cameraPosition;
            Camera.main.transform.rotation = _rotation;

            _currentState.UpdateStates();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }
    }
}
