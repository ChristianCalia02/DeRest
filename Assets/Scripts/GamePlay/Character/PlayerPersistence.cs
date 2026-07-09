using UnityEngine;
using Core;

namespace GamePlay
{
    [RequireComponent(typeof(Character))]
    public class PlayerPersistence : MonoBehaviour, IPlayerPositionProvider
    {
        public Vector3 Position => transform.position;
        public float Yaw => transform.eulerAngles.y;

        private void OnEnable()
        {
            PlayerPositionRegistry.Register(this);
            ApplySavedPosition();
        }

        private void OnDisable() => PlayerPositionRegistry.Unregister(this);

        private void ApplySavedPosition()
        {
            if (SaveSystem.ActiveGameData == null) return;

            var transformData = SaveSystem.ActiveGameData.GetData<PlayerTransformData>();

            var controller = GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;

            transform.position = transformData.Position;
            transform.rotation = Quaternion.Euler(0f, transformData.Yaw, 0f);

            if (controller != null) controller.enabled = true;
        }
    }
}