using UnityEngine;
using Ignite.Input;
using Ignite.UI;

namespace Ignite
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int winCondition = 5;
        [SerializeField] private WinScreenUI winScreen;

        private int _winCounter;
        private InputMap _inputMap;

        public static GameManager Instance
        {  get; private set; }

        public InputMap InputMap
        { get => _inputMap; }

        public int WinCounter
        { get => _winCounter; }
        public int WinCondition
        { get => winCondition; }

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

        public void AddWin()
        {
            _winCounter++;

            if (_winCounter == winCondition)
            {
                winScreen.Enable();
            }
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
