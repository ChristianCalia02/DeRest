using System;
using UnityEngine;

namespace Core
{
    public interface IInputReader
    {
        Vector2 Move { get; }
        Vector2 MousePosition { get; }
        bool InteractPressed { get; }

        event Action OnInteract;
        event Action OnPause;
        event Action OnDebug;
        event Action OnJump;
    }
}