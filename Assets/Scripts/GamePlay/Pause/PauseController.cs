using UnityEngine;
using Core;

namespace GamePlay 
{ 
    public class PauseController : MonoBehaviour
    {
        private IInputReader inputReader;
        private bool isPaused;

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

        private void OnDestroy()
        {
            if (inputReader != null)
                inputReader.OnPause -= TogglePause;
        }

        private void TogglePause()
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            EventBus.Publish(new PauseStateChangedEvent(isPaused));
        }
    }
}