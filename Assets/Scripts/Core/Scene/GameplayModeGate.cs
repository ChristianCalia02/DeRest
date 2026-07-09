using UnityEngine;

namespace Core
{
    public class GameplayModeGate : MonoBehaviour
    {
        [SerializeField] private GameplayMode activeInMode;
        [SerializeField] private GameObject target;

        private void OnEnable() => EventBus.Subscribe<GameplayModeChangedEvent>(OnModeChanged);
        private void OnDisable() => EventBus.Unsubscribe<GameplayModeChangedEvent>(OnModeChanged);

        private void OnModeChanged(GameplayModeChangedEvent e) =>
            target.SetActive(e.Mode == activeInMode);
    }
}