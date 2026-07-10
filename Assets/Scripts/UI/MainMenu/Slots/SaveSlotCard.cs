using UnityEngine;
using TMPro;
using Core;

namespace UI
{
    public class SaveSlotCard : MonoBehaviour
    {
        [SerializeField] private int slotIndex;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text subtitleText;
        [SerializeField] private GameObject emptyStateRoot;
        [SerializeField] private GameObject filledStateRoot;

        private void OnEnable() => Refresh();

        public void Refresh()
        {
            bool exists = SaveSystem.SlotExists(slotIndex);

            emptyStateRoot.SetActive(!exists);
            filledStateRoot.SetActive(exists);

            if (!exists)
            {
                titleText.text = "New game";
                return;
            }

            titleText.text = $"Slot {slotIndex + 1}";

            var lastPlayed = SaveSystem.GetSlotLastWriteTime(slotIndex);
            subtitleText.text = lastPlayed.HasValue
                ? $"Last session: {lastPlayed.Value:dd/MM/yyyy HH:mm}"
                : string.Empty;
        }
    }
}