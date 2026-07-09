using Core;
using UnityEngine;

namespace GamePlay
{
    public class CharacterPlayerController : CharacterControllerBase
    {
        private IInputReader inputReader;

        protected override void Start()
        {
            inputReader = GetComponent<IInputReader>() ?? GetComponentInChildren<IInputReader>();
            if (inputReader == null)
            {
                Debug.LogError($"{name}: no IInputReader founded.", this);
                enabled = false;
                return;
            }

            base.Start();
        }

        private void Update()
        {
            if (CurrentCharacter == null) return;

            Vector2 move = inputReader.Move;
            Vector3 worldMove = new Vector3(move.x, 0f, move.y);
            CurrentCharacter.Move(worldMove);
        }

        protected override void PostProcessControl(ControllableBase c)
        {
            base.PostProcessControl(c);
            if (c != null)
                EventBus.Publish(new PlayerControlledEvent(c.transform, GameplayMode.Surface));
        }
    }
}