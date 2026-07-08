using UnityEngine;

namespace Core
{
    public readonly struct PlayerControlledEvent : IGameEvent
    {
        public readonly Transform PlayerTransform;
        public PlayerControlledEvent(Transform playerTransform) => PlayerTransform = playerTransform;
    }
}
