using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SaveSlotCardHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CanvasGroup highlightBorder;
        [SerializeField] private float fadeDuration = 0.15f;

        private Coroutine activeAnim;

        private void Awake() => highlightBorder.alpha = 0f;

        public void OnPointerEnter(PointerEventData eventData) => FadeTo(1f);
        public void OnPointerExit(PointerEventData eventData) => FadeTo(0f);

        private void FadeTo(float target)
        {
            if (activeAnim != null) StopCoroutine(activeAnim);
            activeAnim = StartCoroutine(FadeRoutine(target));
        }

        private IEnumerator FadeRoutine(float target)
        {
            float start = highlightBorder.alpha;
            float t = 0f;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                highlightBorder.alpha = Mathf.Lerp(start, target, t / fadeDuration);
                yield return null;
            }
            highlightBorder.alpha = target;
        }
    }
}