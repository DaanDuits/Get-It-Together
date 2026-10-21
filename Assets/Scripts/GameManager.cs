using UnityEngine;

namespace Ignite
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {  get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance);
                return;
            }

            Instance = this;
        }
    }
}
