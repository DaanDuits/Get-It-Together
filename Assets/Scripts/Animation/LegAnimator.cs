using UnityEngine;
using UnityEngine.Events;

namespace Ignite
{
    public class LegAnimator : StateMachineBehaviour
    {
        private UnityEvent _onHopFinished = new UnityEvent();
        private UnityEvent _onHopStarted = new UnityEvent();

        public UnityEvent OnHopFinished
        { get => _onHopFinished; }
        public UnityEvent OnHopStarted
        { get => _onHopStarted; }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("HopBegin"))
            {
                _onHopStarted.Invoke();
            }
            if (stateInfo.IsName("Landing"))
            {
                Debug.Log("Landing");
                _onHopFinished.Invoke();
            }
        }
    }
}
