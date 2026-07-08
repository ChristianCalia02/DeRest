namespace Core
{
    public readonly struct PauseStateChangedEvent : IGameEvent
    {
        public readonly bool IsPaused;
        public PauseStateChangedEvent(bool isPaused) => IsPaused = isPaused;
    }
}