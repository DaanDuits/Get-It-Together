using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Ignite.UI
{
    public class MainMenuUI : UIBehaviour
    {
        [SerializeField] private Animator animator;

        private void Awake()
        {
            animator.GetBehaviour<TruckAnimator>().OnHit.AddListener(BlackOut);

            Root.Q<Button>("play-button").clicked += () => {
                Root.Q<VisualElement>("menu-holder").AddToClassList("fade-out");
                Root.Q<VisualElement>("menu-holder").RegisterCallback<TransitionEndEvent>(OnFadeOutEnd);
            };
            Root.Q<Button>("quit-button").clicked += () => {
                Application.Quit();
            };
        }

        private void OnFadeOutEnd(TransitionEndEvent evt)
        {
            animator.Play("TruckAnimation", 0);
        }
        private void OnBlackOutEnd(TransitionEndEvent evt)
        {
            SceneManager.LoadScene(1);
        }

        private void BlackOut()
        {
            Root.AddToClassList("black-out");
            Root.RegisterCallback<TransitionEndEvent>(OnBlackOutEnd);
        }
    }
}
