namespace Core
{
    public readonly struct LoadGameRequestedEvent : IGameEvent { }

    public readonly struct ReturnToMainMenuRequestedEvent : IGameEvent { }

    public readonly struct SceneTransitionCompletedEvent : IGameEvent
    {
        public readonly string SceneName;
        public SceneTransitionCompletedEvent(string sceneName) => SceneName = sceneName;
    }
}