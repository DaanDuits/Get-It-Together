using UnityEngine;
using Ignite.Input;

namespace Ignite
{
    public class InputListener : MonoBehaviour
    {
        protected InputMap InputMap
        {  get; private set; }

        private void OnEnable()
        {
            InputMap = new InputMap();
            InputMap.Player.Enable();
        }

        private void OnDisable()
        {
            InputMap.Player.Disable();
        }
    }
}
