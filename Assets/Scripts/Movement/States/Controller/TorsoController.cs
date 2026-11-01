using System.Collections.Generic;
using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    [RequireComponent(typeof(PossessionController))]
    public class TorsoController : MonoBehaviour
    {
        [SerializeField] private LayerMask limbLayer;
        [SerializeField] private LimbSettings limbs;
        [SerializeField] private HeadCheck headCheck;

        [System.Serializable]
        private struct LimbSettings
        {
            public Renderer LeftArm, RightArm;
            public Renderer LeftLeg, RightLeg;
        }

        private Dictionary<LimbType, Renderer> _renderers;
        private List<bool> _enabled = new List<bool>();

        private bool _hasEntered;
        private ILimbController _current;

        private PossessionController _controller;

        private void Start()
        {
            _controller = GetComponent<PossessionController>();

            _renderers = new Dictionary<LimbType, Renderer>() {
                { LimbType.LeftArm, limbs.LeftArm }, { LimbType.RightArm, limbs.RightArm },
                { LimbType.LeftLeg, limbs.LeftLeg }, { LimbType.RightLeg, limbs.RightLeg }
            };

            headCheck.OnHeadAttached.AddListener(OnHeadAttached);
            headCheck.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Head"))
                return;
            if ((limbLayer & (1 << other.gameObject.layer)) == 0)
                return;

            IPossessable possessable = other.gameObject.GetComponent<IPossessable>();
            _current = possessable.Controller;
            _controller.Reset();
            Render(_current.Type);
            _current.Destroy();

            if (_enabled.Count == GameManager.Instance.LimbCount)
                headCheck.gameObject.SetActive(true);
        }

        private void OnHeadAttached()
        {
            GameManager.Instance.Win();
        }

        private void Render(LimbType type)
        {
            _renderers[type].enabled = true;
            _enabled.Add(true);
        }
    }
}
