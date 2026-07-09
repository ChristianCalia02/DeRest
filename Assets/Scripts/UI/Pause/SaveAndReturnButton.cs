using UnityEngine;
using Core;

namespace UI
{
    public class SaveAndReturnButton : MonoBehaviour
    {
        public void OnSaveAndReturnPressed()
        {
            EventBus.Publish(new SaveGameEvent());

            EventBus.Publish(new PauseStateChangedEvent(false)); //hide pause panel

            EventBus.Publish(new ReturnToMainMenuRequestedEvent());
        }
    }
}