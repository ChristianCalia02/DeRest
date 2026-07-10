using UnityEngine;
using Core;

namespace UI
{
    public class ContinueButton : MonoBehaviour
    {
        public void OnContinuePressed() => EventBus.Publish(new ResumeGameRequestedEvent());
    }
}