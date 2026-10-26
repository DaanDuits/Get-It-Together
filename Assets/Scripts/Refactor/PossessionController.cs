using Ignite.Movement;
using UnityEngine;

namespace Ignite
{
    public class PossessionController : InputListener
    {
        [SerializeField] private float possessionDistance = 5f;
        [SerializeField] private LayerMask possessionLayer;
        [SerializeField] private new Camera camera;
        [SerializeField] private ILimbController startLimb;

        private ILimbController _currentLimb;

        private void Start()
        {
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

            if (Physics.Raycast(ray, out RaycastHit hit, possessionDistance, possessionLayer))
                _currentLimb = hit.collider.gameObject.GetComponent<ILimbController>();
            else
                _currentLimb = startLimb;
        }
    }
}
