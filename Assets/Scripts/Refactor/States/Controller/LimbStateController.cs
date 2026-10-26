using System;
using UnityEngine;
using Ignite.Input;

namespace Ignite
{
    public abstract class LimbStateController : MonoBehaviour
    {
        private Vector3 _appliedMovement;

        public LimbMovementState CurrentState
        { get; set; }

        protected InputMap Input
        {  get; private set; }

        public Vector2 MoveInput
        { get; private set; }
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

        private void OnEnable()
        {
            Input = new InputMap();
            Input.Player.Enable();
        }
        private void OnDisable()
        {
            Input.Player.Disable();
        }

        public LimbMovementState GetStateByType<T>()
        {
            return GetStateByType(typeof(T));
        }
        public virtual LimbMovementState GetStateByType(Type t)
        {
            throw new NotImplementedException(GetType().Name + "has not implemented virtual method GetStateByType!");
        }
    }
}
