using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ignite.UI
{
    public class PauseScreenUI : UIBehaviour
    {
        private void Awake()
        {
            Hide();
        }

        public void Show()
        {
            Document.enabled = true;

            Root.Q<Button>("continue-button").clicked += () => {
                GameManager.Instance.SwitchPause();
            };
            Root.Q<Button>("main-menu-button").clicked += () => {
                SceneManager.LoadScene(0);
            };
        }

        public void Hide()
        {
            Document.enabled = false;
        }
    }
}
