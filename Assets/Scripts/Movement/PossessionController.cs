using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement
{
    public class PossessionController : MonoBehaviour
    {
        [SerializeField] private float possessionDistance = 5f;
        [SerializeField] private LayerMask possessionLayer;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject startLimb;

        private ILimbController _startLimb;

        private ILimbController _currentLimb;

        private bool _isLocked;

        private void Start()
        {
            _startLimb = startLimb.GetComponent<ILimbController>();
            _currentLimb = _startLimb;
            _currentLimb.EnterLimb();

            if (camera == null)
                camera = Camera.main;
        }

        private void Update()
        {
            if (_isLocked) return;
            _currentLimb.UpdateLimb();
        }

        private void FixedUpdate()
        {
            if (_isLocked) return;

            Ray ray = camera.ScreenPointToRay(GameManager.Instance.InputMap.Player.MousePos.ReadValue<Vector2>());

            if (GameManager.Instance.InputMap.Player.MousePressed.ReadValue<float>() == 0f)
                return;

            _currentLimb.ExitLimb();
            if (Physics.Raycast(ray, out RaycastHit hit, possessionDistance, possessionLayer))
            {
                IPossessable possessable = hit.collider.gameObject.GetComponent<IPossessable>();
                _currentLimb = possessable.Controller;
            }
            else
            {
                _currentLimb = _startLimb;
            }
            _currentLimb.EnterLimb();
        }

        public void Reset()
        {
            _currentLimb.ExitLimb();
            _currentLimb = _startLimb;
            _currentLimb.EnterLimb();
        }

        public void Lock(bool locked)
        {
            _isLocked = locked;
        }
    }
}
