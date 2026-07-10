using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class MenuListItemHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private RectTransform label;
        [SerializeField] private float hoverOffset = 12f;
        [SerializeField] private float duration = 0.15f;

        private Vector2 baseAnchoredPosition;
        private Coroutine activeAnim;

        private void Awake() => baseAnchoredPosition = label.anchoredPosition;

        public void OnPointerEnter(PointerEventData eventData) =>
            AnimateTo(baseAnchoredPosition + Vector2.right * hoverOffset);

        public void OnPointerExit(PointerEventData eventData) =>
            AnimateTo(baseAnchoredPosition);

        private void AnimateTo(Vector2 target)
        {
            if (activeAnim != null) StopCoroutine(activeAnim);
            activeAnim = StartCoroutine(MoveRoutine(target));
        }

        private IEnumerator MoveRoutine(Vector2 target)
        {
            Vector2 start = label.anchoredPosition;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                label.anchoredPosition = Vector2.Lerp(start, target, t / duration);
                yield return null;
            }
            label.anchoredPosition = target;
        }
    }
}