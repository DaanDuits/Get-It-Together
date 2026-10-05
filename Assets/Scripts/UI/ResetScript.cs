using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ignite.UI
{
    public class ResetScript : MonoBehaviour
    {
        public void ResetScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}
