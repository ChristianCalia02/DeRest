using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ButtonHoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float duration = 0.15f;

        private RectTransform rect;
        private Vector3 baseScale;
        private Coroutine activeAnim;

        private void Awake()
        {
            rect = (RectTransform)transform;
            baseScale = rect.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData) => AnimateTo(baseScale * hoverScale);
        public void OnPointerExit(PointerEventData eventData) => AnimateTo(baseScale);

        private void AnimateTo(Vector3 target)
        {
            if (activeAnim != null) StopCoroutine(activeAnim);
            activeAnim = StartCoroutine(ScaleRoutine(target));
        }

        private IEnumerator ScaleRoutine(Vector3 target)
        {
            Vector3 start = rect.localScale;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                rect.localScale = Vector3.Lerp(start, target, t / duration);
                yield return null;
            }
            rect.localScale = target;
        }
    }
}