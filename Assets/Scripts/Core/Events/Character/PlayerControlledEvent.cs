using UnityEngine;

namespace Core
{
    public readonly struct PlayerControlledEvent : IGameEvent
    {
        public readonly Transform PlayerTransform;
        public readonly GameplayMode Mode;

        public PlayerControlledEvent(Transform playerTransform, GameplayMode mode)
        {
            PlayerTransform = playerTransform;
            Mode = mode;
        }
    }
}