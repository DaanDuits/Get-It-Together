using UnityEngine;
using UnityEngine.InputSystem;
using Ignite.Input;
using System.Runtime.CompilerServices;

namespace Ignite.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, InputMap.IPlayerActions
    {
        private CharacterController _controller;
        private InputMap _inputMap;

        private Vector2 _movementInput;

        private Vector3 _appliedMovement;

        private Vector3 _camPos;
        private Vector3 _angularVelocity;

        private float _phi, _theta;
        private const float Radius = 0.5f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            _inputMap = new InputMap();
            _inputMap.Player.Enable();
            _inputMap.Player.AddCallbacks(this);
        }

        private void Update()
        {
            _appliedMovement.x = _movementInput.x * 3.0f;
            _appliedMovement.z = _movementInput.y * 3.0f;

            _appliedMovement = Quaternion.Euler(0, transform.eulerAngles.y, 0) * _appliedMovement; 

            _controller.Move(_appliedMovement * Time.deltaTime);

            _angularVelocity.x = _appliedMovement.x / Radius;
            _angularVelocity.y = _appliedMovement.z / Radius;
            _angularVelocity.z = 0.0f;

            float sinPhi = Mathf.Sin(_phi);
            if (Mathf.Abs(sinPhi) < 0.001f)
                sinPhi = 0.001f * Mathf.Sign(sinPhi);
            float cotPhi = Mathf.Cos(_phi) / sinPhi;

            float dPhi = _angularVelocity.x * Mathf.Sin(_theta) - _angularVelocity.y * Mathf.Cos(_theta);
            float dTheta = _angularVelocity.z - cotPhi * (_angularVelocity.x * Mathf.Cos(_theta) + _angularVelocity.y * Mathf.Sin(_theta));

            _phi += dPhi * Time.deltaTime;
            _theta += dTheta * Time.deltaTime; 
            
            _theta = Mathf.Repeat(_theta, Mathf.PI * 2f);
            _phi = Mathf.Clamp(_phi, 0.001f, Mathf.PI - 0.001f);

            _camPos.x = Mathf.Sin(_phi) * Mathf.Cos(_theta) * Radius;
            _camPos.z = Mathf.Sin(_phi) * Mathf.Sin(_theta) * Radius;
            _camPos.y = Mathf.Cos(_phi) * Radius;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _movementInput = context.ReadValue<Vector2>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_camPos + transform.position, 0.1f);
        }
    }
}
