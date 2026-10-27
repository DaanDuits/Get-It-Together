using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ignite.UI
{
    public class WinScreenUI : UIBehaviour
    {
        private void Awake()
        {
            Document.enabled = false;
        }

        public void Enable()
        {
            Document.enabled = true;

            Root.Q<Button>("main-menu-button").clicked += () =>
            {
                Debug.Log("Test");
                SceneManager.LoadScene(0);
            };
        }
    }
}
