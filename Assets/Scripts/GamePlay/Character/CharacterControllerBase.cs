using Core;

namespace GamePlay
{
    public abstract class CharacterControllerBase : ControllerBase
    {
        public Character CurrentCharacter { get; private set; }

        protected override void PostProcessControl(ControllableBase c)
        {
            base.PostProcessControl(c);
            CurrentCharacter = c as Character;
        }

        protected override void PostProcessRelease(ControllableBase c)
        {
            base.PostProcessRelease(c);
            CurrentCharacter = null;
        }
    }
}