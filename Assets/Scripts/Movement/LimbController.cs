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
        [SerializeField] private GameObject visualObject;

        private void Awake()
        {
            Controller = GetComponent<CharacterController>();
        }

        public ELimbType Type
        { get => type; }
        public CharacterController Controller
        { get; private set; }
        public GameObject VisualObject
        { get => visualObject; }
    }
}
