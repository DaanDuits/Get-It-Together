using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ignite.UI
{
    public class StartScreenUI : UIBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(WaitForLoad());
            Root.Q("background").RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
        }

        private IEnumerator WaitForLoad()
        {
            yield return new WaitForSeconds(1);


            Root.Q("background").AddToClassList("fade-out");
        }

        private void OnFadeOutEnd(TransitionEndEvent evt)
        {
            gameObject.SetActive(false);
        }
    }
}
