using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class HeadController : MonoBehaviour, ILimbController
    {
        [SerializeField] private float radius;
        [SerializeField] private float rollSpeed;
        [SerializeField] private new Transform camera;

        private CharacterController _controller;

        private Vector3 _appliedMovement;

        public LimbMovementState CurrentState 
        { get; set; }

        public float Radius
        { get => radius; }
        public float RollSpeed
        { get => rollSpeed; }
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

        public Vector3 CameraPosition
        { get; set; }
        public Quaternion CameraRotation
        { get; set; }

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main.transform;

            camera.position = transform.position + transform.forward * radius;

            CurrentState = new HeadRollState(this);

            CameraPosition = camera.position;
            CameraRotation = camera.rotation;

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

            camera.position = CameraPosition;
            camera.rotation = CameraRotation;

            MoveInput = GameManager.Instance.InputMap.Player.Move.ReadValue<Vector2>();
        }
        public void ExitLimb()
        { }

        public void Lock()
        {
            enabled = false;
        }
    }
}
