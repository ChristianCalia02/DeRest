using UnityEngine;
using Core;

namespace UI
{
    public class SettingsButton : MonoBehaviour
    {
        public void OnSettingsPressed() => EventBus.Publish(new OpenSettingsRequestedEvent());
    }
}