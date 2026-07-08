using Core;
using UnityEngine;

namespace UI
{
    public class PauseMenuPanel : UIPanelBase
    {
        private void OnEnable() => EventBus.Subscribe<PauseStateChangedEvent>(OnPauseChanged);
        private void OnDisable() => EventBus.Unsubscribe<PauseStateChangedEvent>(OnPauseChanged);

        private void OnPauseChanged(PauseStateChangedEvent e)
        {
            if (e.IsPaused) Show(); else Hide();
        }
    }
}