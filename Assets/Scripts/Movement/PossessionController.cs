using UnityEngine;
using Ignite.Movement.States.Controller;

namespace Ignite.Movement
{
    public class PossessionController : InputListener
    {
        [SerializeField] private float possessionDistance = 5f;
        [SerializeField] private LayerMask possessionLayer;
        [SerializeField] private new Camera camera;
        [SerializeField] private GameObject startLimb;

        private ILimbController _startLimb;

        private ILimbController _currentLimb;

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
            _currentLimb.UpdateLimb();
        }

        private void FixedUpdate()
        {
            Ray ray = camera.ScreenPointToRay(InputMap.Player.MousePos.ReadValue<Vector2>());

            if (InputMap.Player.MousePressed.ReadValue<float>() == 0f)
                return;

            _currentLimb.ExitLimb();
            if (Physics.Raycast(ray, out RaycastHit hit, possessionDistance, possessionLayer))
                _currentLimb = hit.collider.gameObject.GetComponent<ILimbController>();
            else
                _currentLimb = _startLimb;
            _currentLimb.EnterLimb();
        }
    }
}
