namespace Core
{
    public readonly struct EnterMineRequestedEvent : IGameEvent { }
    public readonly struct ExitMineRequestedEvent : IGameEvent { }

    public readonly struct GameplayModeChangedEvent : IGameEvent
    {
        public readonly GameplayMode Mode;
        public GameplayModeChangedEvent(GameplayMode mode) => Mode = mode;
    }
}