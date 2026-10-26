using UnityEngine;

namespace Ignite
{
    public class PossessionController : MonoBehaviour
    {
        private ILimbController _currentLimb;

        private void Update()
        {
            _currentLimb.UpdateLimb();
        }
    }
}
