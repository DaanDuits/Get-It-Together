using UnityEngine;
using UnityEngine.Events;

namespace Ignite.Movement
{
    [RequireComponent(typeof(BoxCollider))]
    public class HeadCheck : MonoBehaviour
    {
        private UnityEvent _onHeadAttached = new UnityEvent();

        public UnityEvent OnHeadAttached
        { get => _onHeadAttached; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Head"))
                _onHeadAttached.Invoke();
        }
    }
}
