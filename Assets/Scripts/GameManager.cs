using UnityEngine;
using Ignite.Input;
using Ignite.UI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Ignite.Movement.States.Controller;
using Ignite.Movement;
using Ignite.Animation;

namespace Ignite
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private int limbCount = 4;
        [SerializeField] private WinScreenUI winScreen;
        [SerializeField] private PauseScreenUI pauseScreenUI;
        [SerializeField] private HeadController headController;
        [SerializeField] private PossessionController possessionController;
        [SerializeField] private WinAnimation winAnimation;
        [SerializeField] private Volume globalVolume;
        [SerializeField] private Transform vfxHolder;

        private InputMap _inputMap;

        private DepthOfField _dof;

        private bool _pauseState = true;

        public static GameManager Instance
        {  get; private set; }

        public InputMap InputMap
        { get => _inputMap; }
        public int LimbCount
        { get => limbCount; }
        public Transform VFXHolder
        {  get => vfxHolder; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
                return;
            }

            Instance = this;

            _inputMap = new InputMap();

            _inputMap.Player.Pause.started += OnPause;
            globalVolume.profile.TryGet(out _dof);
        }

        public void Win()
        {
            winScreen.Enable();
            winAnimation.Play();

            headController.LockPan(true);
            possessionController.Lock(true);
        }

        private void OnEnable()
        {
            _inputMap.Player.Enable();
        }

        private void OnDisable()
        {
            _inputMap.Player.Disable();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            SwitchPause();
        }
        public void SwitchPause()
        {
            if (_pauseState)
                Pause();
            else
                Resume();

            _pauseState = !_pauseState;
        }

        private void Pause()
        {
            pauseScreenUI.Show();

            headController.LockPan(true);
            possessionController.Lock(true);
            _dof.active = true;
        }

        private void Resume()
        {
            pauseScreenUI.Hide();

            headController.LockPan(false);
            possessionController.Lock(false);
            _dof.active = false;
        }
    }
}
