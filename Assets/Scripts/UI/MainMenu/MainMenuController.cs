using UnityEngine;
using Core;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void OnPlayButtonPressed() => EventBus.Publish(new LoadGameRequestedEvent());
        public void OnQuitButtonPressed() => Application.Quit();
    }
}