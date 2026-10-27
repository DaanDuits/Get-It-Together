using UnityEngine;

namespace Ignite.Movement.States.Controller
{
    public class TorsoController : MonoBehaviour
    {
        [SerializeField] private LayerMask limbLayer;

        private bool _hasEntered;
        private ILimbController _current;

        private void Start()
        {
            GameManager.Instance.InputMap.Player.Submit.started += context => {
                if (!_hasEntered)
                    return;

                GameManager.Instance.AddWin();

                _hasEntered = false;
                _current.Lock();
            };
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Head") && GameManager.Instance.WinCounter < GameManager.Instance.WinCondition - 1)
                return;
            if ((limbLayer & (1 << other.gameObject.layer)) == 0)
                return;

            _current = other.GetComponent<ILimbController>();
            _hasEntered = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if ((limbLayer & (1 << other.gameObject.layer)) == 0)
                return;
            _hasEntered = false;
        }
    }
}
