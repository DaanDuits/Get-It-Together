using System.Collections;
using UnityEngine;

namespace Ignite.Animation
{
    public class WinAnimation : MonoBehaviour
    {
        [SerializeField] private new Transform camera;
        [SerializeField] private Transform start, end;

        private void Awake()
        {
            if (camera == null)
                camera = Camera.main.transform;
        }

        public void Play()
        {
            StartCoroutine(Transition());
        }

        private IEnumerator Transition()
        {
            float timer = 0f;
            while(timer != 1f)
            {
                camera.rotation = Quaternion.Slerp(start.rotation, end.rotation, timer);
                camera.position = Vector3.Lerp(start.position, end.position, timer);

                timer += Time.deltaTime;
                timer = Mathf.Clamp01(timer);

                yield return null;
            }
        }

    }
}
