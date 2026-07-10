using UnityEngine;
using Core;

namespace UI
{
    public class SettingsBackButton : MonoBehaviour
    {
        public void OnBackPressed() => EventBus.Publish(new CloseSettingsRequestedEvent());
    }
}