using UnityEngine;
using Core;

namespace UI
{
    public class SaveSlotButton : MonoBehaviour
    {
        [SerializeField] private int slotIndex;
        public void OnSlotPressed() => EventBus.Publish(new LoadGameRequestedEvent(slotIndex));
    }
}