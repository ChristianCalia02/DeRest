using Core;

namespace UI
{
    public class SettingsPanel : UIPanelBase
    {
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

        private void OnOpenRequested(OpenSettingsRequestedEvent e) => Show();
        private void OnCloseRequested(CloseSettingsRequestedEvent e) => Hide();
    }
}