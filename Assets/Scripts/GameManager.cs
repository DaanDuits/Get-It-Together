using UnityEngine;
using Ignite.Input;
using Ignite.UI;

namespace Ignite
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int limbCount = 4;
        [SerializeField] private WinScreenUI winScreen;

        private InputMap _inputMap;

        public static GameManager Instance
        {  get; private set; }

        public InputMap InputMap
        { get => _inputMap; }
        public int LimbCount
        { get => limbCount; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
                return;
            }

            Instance = this;

            _inputMap = new InputMap();
        }

        public void Win()
        {
            winScreen.Enable();
        }

        private void OnEnable()
        {
            _inputMap.Player.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Player.Disable();
        }
    }
}
