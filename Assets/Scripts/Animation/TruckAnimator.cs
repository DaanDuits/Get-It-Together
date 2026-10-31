using UnityEngine;
using UnityEngine.Events;

namespace Ignite
{
    public class TruckAnimator : StateMachineBehaviour
    {

        private UnityEvent _onHit = new UnityEvent();
        public UnityEvent OnHit
        { get => _onHit; }
        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("TruckAnimation"))
                _onHit.Invoke();
        }
    }
}
