using UnityEngine;
using Core;

namespace Mine
{
    [RequireComponent(typeof(Collider2D))]
    public class MineExitInteractable : MonoBehaviour
    {
        private IInputReader inputReaderInRange;

        private void Reset() => GetComponent<Collider2D>().isTrigger = true;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var reader = other.GetComponentInParent<IInputReader>();
            if (reader != null) inputReaderInRange = reader;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var reader = other.GetComponentInParent<IInputReader>();
            if (reader == inputReaderInRange) inputReaderInRange = null;
        }

        private void Update()
        {
            if (inputReaderInRange != null && inputReaderInRange.InteractPressed)
                EventBus.Publish(new ExitMineRequestedEvent());
        }
    }
}