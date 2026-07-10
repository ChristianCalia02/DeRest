using UnityEngine;

namespace Core
{
    public interface IPlayerPositionProvider
    {
        Vector3 Position { get; }
        float Yaw { get; }
    }
}