using System.Collections;
using UnityEngine;
using Core;

namespace UI
{
    public class MainMenuTransitionController : MonoBehaviour
    {
        [Header("Main buttons")]
        [SerializeField] private CanvasGroup mainButtonsGroup;
        [SerializeField] private RectTransform playButton;

        [Header("Save slots")]
        [SerializeField] private CanvasGroup saveSlotsGroup;
        [SerializeField] private RectTransform saveSlotsPanel;

        [Header("Timing")]
        [SerializeField] private float expandDuration = 0.35f;
        [SerializeField] private float revealDuration = 0.25f;
        [SerializeField] private float expandTargetScale = 6f;
        [SerializeField] private AnimationCurve expandCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private Vector3 playButtonOriginalScale;
        private int playButtonOriginalSiblingIndex;
        private bool isSettingsOpen;
        private bool isInSlotsView;

        private void Awake()
        {
            playButtonOriginalScale = playButton.localScale;
            playButtonOriginalSiblingIndex = playButton.GetSiblingIndex();

            saveSlotsPanel.localScale = Vector3.zero;
            SetGroupVisible(saveSlotsGroup, false);
        }

        private void OnEnable()
        {
            EventBus.Subscribe<BackRequestedEvent>(OnBackRequested);
            EventBus.Subscribe<OpenSettingsRequestedEvent>(OnSettingsOpened);
            EventBus.Subscribe<CloseSettingsRequestedEvent>(OnSettingsClosed);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<BackRequestedEvent>(OnBackRequested);
            EventBus.Unsubscribe<OpenSettingsRequestedEvent>(OnSettingsOpened);
            EventBus.Unsubscribe<CloseSettingsRequestedEvent>(OnSettingsClosed);
        }

        private void OnSettingsOpened(OpenSettingsRequestedEvent e) => isSettingsOpen = true;
        private void OnSettingsClosed(CloseSettingsRequestedEvent e) => isSettingsOpen = false;

        private void OnBackRequested(BackRequestedEvent e)
        {
            if (isSettingsOpen)
            {
                EventBus.Publish(new CloseSettingsRequestedEvent());
                return;
            }

            if (isInSlotsView)
                OnBackPressed();
        }

        public void OnPlayPressed()
        {
            foreach (var card in saveSlotsPanel.GetComponentsInChildren<SaveSlotCard>(true))
                card.Refresh();

            StartCoroutine(EnterPlayButtonRoutine());
        }

        public void OnBackPressed() => StartCoroutine(ExitToMainMenuRoutine());

        private IEnumerator EnterPlayButtonRoutine()
        {
            SetGroupVisible(mainButtonsGroup, false);
            yield return Fade(mainButtonsGroup, 1f, 0f, expandDuration * 0.6f);

            playButton.SetAsLastSibling();
            yield return Scale(playButton, playButtonOriginalScale,
                                playButtonOriginalScale * expandTargetScale, expandDuration, expandCurve);

            saveSlotsPanel.localScale = Vector3.one * 0.8f;
            SetGroupVisible(saveSlotsGroup, true);
            yield return Fade(saveSlotsGroup, 0f, 1f, revealDuration);
            yield return Scale(saveSlotsPanel, Vector3.one * 0.8f, Vector3.one, revealDuration, null);

            isInSlotsView = true;
        }

        private IEnumerator ExitToMainMenuRoutine()
        {
            SetGroupVisible(saveSlotsGroup, false);
            yield return Fade(saveSlotsGroup, 1f, 0f, revealDuration);
            saveSlotsPanel.localScale = Vector3.zero;

            playButton.localScale = playButtonOriginalScale;
            playButton.SetSiblingIndex(playButtonOriginalSiblingIndex);

            SetGroupVisible(mainButtonsGroup, true);
            yield return Fade(mainButtonsGroup, 0f, 1f, expandDuration * 0.6f);

            isInSlotsView = false;
        }

        private static void SetGroupVisible(CanvasGroup group, bool visible)
        {
            group.interactable = visible;
            group.blocksRaycasts = visible;
        }

        private static IEnumerator Fade(CanvasGroup group, float from, float to, float duration)
        {
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                group.alpha = Mathf.Lerp(from, to, t / duration);
                yield return null;
            }
            group.alpha = to;
        }

        private static IEnumerator Scale(RectTransform rect, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
        {
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float e = curve?.Evaluate(t / duration) ?? (t / duration);
                rect.localScale = Vector3.Lerp(from, to, e);
                yield return null;
            }
            rect.localScale = to;
        }
    }
}