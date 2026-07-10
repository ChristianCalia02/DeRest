using UnityEngine;

namespace Core
{
    public class PauseController : MonoBehaviour
    {
        private IInputReader inputReader;
        private bool isPaused;
        private bool isSettingsOpen;

        private void Start()
        {
            inputReader = GetComponent<IInputReader>() ?? GetComponentInChildren<IInputReader>();
            if (inputReader == null)
            {
                Debug.LogError($"{name}: no IInputReader founded.", this);
                enabled = false;
                return;
            }
            inputReader.OnPause += TogglePause;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<ResumeGameRequestedEvent>(OnResumeRequested);
            EventBus.Subscribe<OpenSettingsRequestedEvent>(OnSettingsOpened);
            EventBus.Subscribe<CloseSettingsRequestedEvent>(OnSettingsClosed);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<ResumeGameRequestedEvent>(OnResumeRequested);
            EventBus.Unsubscribe<OpenSettingsRequestedEvent>(OnSettingsOpened);
            EventBus.Unsubscribe<CloseSettingsRequestedEvent>(OnSettingsClosed);
        }

        private void OnDestroy()
        {
            if (inputReader != null)
                inputReader.OnPause -= TogglePause;
        }

        private void OnSettingsOpened(OpenSettingsRequestedEvent e) => isSettingsOpen = true;
        private void OnSettingsClosed(CloseSettingsRequestedEvent e) => isSettingsOpen = false;

        private void TogglePause()
        {
            if (isSettingsOpen)
            {
                EventBus.Publish(new CloseSettingsRequestedEvent());
                return;
            }

            SetPaused(!isPaused);
        }

        private void OnResumeRequested(ResumeGameRequestedEvent e) => SetPaused(false);

        private void SetPaused(bool paused)
        {
            isPaused = paused;
            Time.timeScale = isPaused ? 0f : 1f;
            EventBus.Publish(new PauseStateChangedEvent(isPaused));
        }
    }
}