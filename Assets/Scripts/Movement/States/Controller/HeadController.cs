using UnityEngine;
using UnityEngine.InputSystem;

namespace Ignite.Movement.States.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class HeadController : MonoBehaviour, ILimbController
    {
        [SerializeField] private float radius;
        [SerializeField] private float rollSpeed;
        [SerializeField] private float panAmount = 10f;
        [SerializeField] private Vector2 panStart = new Vector2(0.3f, 0.3f);
        [SerializeField] private new Camera camera;

        private float _panMax = 0.5f;
        private CharacterController _controller;

        private HeadIdleState _idleState;
        private HeadRollState _rollState;

        private Vector3 _appliedMovement;

        private Vector2 _mousePos, _mouseInput;

        public LimbMovementState CurrentState 
        { get; set; }

        public float Radius
        { get => radius; }
        public float RollSpeed
        { get => rollSpeed; }
        public Vector2 PanStart
        { get => panStart; }
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
        public Vector2 MouseInput
        {
            get
            {
                _mouseInput = camera.ScreenToViewportPoint(_mousePos);
                _mouseInput -= new Vector2(_panMax, _panMax);
                return _mouseInput;
            } 
        }
        public Vector3 Position
        { get => transform.position; }
        public Quaternion Rotation
        { 
            get => transform.rotation; 
            set => transform.rotation = value; 
        }

        public Vector3 CameraPosition
        { get; set; }
        public Quaternion CameraRotation
        { get; set; }
        public Quaternion PanRotation
        { get; set; }

        public HeadIdleState IdleState
        { get => _idleState; }
        public HeadRollState RollState
        { get => _rollState; }

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main;

            CameraPosition = transform.position + transform.forward * radius;
            CameraRotation = transform.rotation;

            _idleState = new HeadIdleState(this);
            _rollState = new HeadRollState(this);

            CurrentState = _idleState;

            _controller = GetComponent<CharacterController>();
        }

        public void EnterLimb()
        { }
        public void UpdateLimb()
        {
            if (!enabled)
                return;

            CurrentState.UpdateState();

            _controller.Move(_appliedMovement * Time.deltaTime);

            _mousePos = GameManager.Instance.InputMap.Player.MousePos.ReadValue<Vector2>();
            camera.transform.position = CameraPosition;
            camera.transform.rotation = CameraRotation * PanRotation;

            MoveInput = GameManager.Instance.InputMap.Player.Move.ReadValue<Vector2>();
        }
        public void ExitLimb()
        { }

        public float PanAngle(float mousePos, float panExtend)
        {
            if (mousePos >= panExtend)
            {
                float t = (mousePos - panExtend) / (panExtend - _panMax);
                return Mathf.Lerp(0f, panAmount, -t);;
            }
            if (mousePos <= -panExtend)
            {
                float t = (mousePos - -panExtend) / (-panExtend - -_panMax);
                return Mathf.Lerp(0f, -panAmount, -t);
            }

            return 0f;
        }

        public void Lock()
        {
            enabled = false;
        }
    }
}
