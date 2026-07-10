using UnityEngine;
using UnityEngine.InputSystem;
using Core;

namespace GameInput
{
    public class BackKeyListener : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
                EventBus.Publish(new BackRequestedEvent());
        }
    }
}