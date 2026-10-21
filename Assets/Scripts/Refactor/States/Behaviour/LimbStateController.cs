using UnityEngine;

namespace Ignite
{
    public abstract class LimbStateController : MonoBehaviour
    {
        public LimbMovementState CurrentState
        { get; set; }
    }
}
