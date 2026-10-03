using UnityEngine;

namespace Ignite.Movement
{
    public enum ELimbType
    {
        Head,
        Arm,
        Leg
    }

    [RequireComponent(typeof(CharacterController))]
    public class LimbController : MonoBehaviour
    {
        [SerializeField] private ELimbType type;
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public ELimbType Type
        { get => type; }
        public CharacterController Controller
        { get => _characterController; }
    }
}
