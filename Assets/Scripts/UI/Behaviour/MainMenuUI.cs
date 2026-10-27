using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ignite.UI
{
    public class MainMenuUI : UIBehaviour
    {
        private void Awake()
        {
            Root.Q<Button>("play-button").clicked += () => {
                SceneManager.LoadScene(1);
            };
            Root.Q<Button>("quit-button").clicked += () => {
                Application.Quit();
            };
        }
    }
}
