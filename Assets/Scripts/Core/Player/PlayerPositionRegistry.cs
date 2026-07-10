namespace Core
{
    public static class PlayerPositionRegistry
    {
        public static IPlayerPositionProvider Active { get; private set; }

        public static void Register(IPlayerPositionProvider provider) => Active = provider;

        public static void Unregister(IPlayerPositionProvider provider)
        {
            if (Active == provider) Active = null;
        }
    }
}