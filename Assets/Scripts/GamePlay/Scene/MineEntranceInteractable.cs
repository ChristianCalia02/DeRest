using UnityEngine;
using Core;

namespace GamePlay
{
    [RequireComponent(typeof(Collider))]
    public class MineEntranceInteractable : MonoBehaviour
    {
        private IInputReader inputReaderInRange;

        private void Reset() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            var reader = other.GetComponentInParent<IInputReader>();
            if (reader != null) inputReaderInRange = reader;
        }

        private void OnTriggerExit(Collider other)
        {
            var reader = other.GetComponentInParent<IInputReader>();
            if (reader == inputReaderInRange) inputReaderInRange = null;
        }

        private void Update()
        {
            if (inputReaderInRange != null && inputReaderInRange.InteractPressed)
                EventBus.Publish(new EnterMineRequestedEvent());
        }
    }
}