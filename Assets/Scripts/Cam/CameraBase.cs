using UnityEngine;
using Core;

namespace CameraSystem
{
    public abstract class CameraBase : MonoBehaviour, ICameraController
    {
        [SerializeField] private GameplayMode ownerMode;

        protected Transform target;
        public Camera Cam { get; private set; }

        protected virtual void Awake() => Cam = GetComponent<Camera>();

        protected virtual void OnEnable() => EventBus.Subscribe<PlayerControlledEvent>(OnPlayerControlled);
        protected virtual void OnDisable() => EventBus.Unsubscribe<PlayerControlledEvent>(OnPlayerControlled);

        public void SetTarget(Transform t)
        {
            target = t;
            OnTargetSet();
        }

        protected virtual void LateUpdate()
        {
            if (target == null) return;
            UpdatePosition();
        }

        private void OnPlayerControlled(PlayerControlledEvent e)
        {
            if (e.Mode == ownerMode)
                SetTarget(e.PlayerTransform);
        }

        protected abstract void UpdatePosition();
        protected virtual void OnTargetSet() { }
    }
}