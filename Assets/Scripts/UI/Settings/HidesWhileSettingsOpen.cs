using UnityEngine;
using Core;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class HidesWhileSettingsOpen : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private bool wasVisibleBeforeSettings;

        private void Awake() => canvasGroup = GetComponent<CanvasGroup>();

        private void OnEnable()
        {
            EventBus.Subscribe<OpenSettingsRequestedEvent>(OnOpenRequested);
            EventBus.Subscribe<CloseSettingsRequestedEvent>(OnCloseRequested);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<OpenSettingsRequestedEvent>(OnOpenRequested);
            EventBus.Unsubscribe<CloseSettingsRequestedEvent>(OnCloseRequested);
        }

        private void OnOpenRequested(OpenSettingsRequestedEvent e)
        {
            wasVisibleBeforeSettings = canvasGroup.alpha > 0.5f;
            if (wasVisibleBeforeSettings)
                SetVisible(false);
        }

        private void OnCloseRequested(CloseSettingsRequestedEvent e)
        {
            if (wasVisibleBeforeSettings)
                SetVisible(true);
        }

        private void SetVisible(bool visible)
        {
            canvasGroup.alpha = visible ? 1f : 0f;
            canvasGroup.interactable = visible;
            canvasGroup.blocksRaycasts = visible;
        }
    }
}