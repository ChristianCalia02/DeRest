using UnityEngine;
using Core;

namespace Mine
{
    public class MinerPlayerController : ControllerBase
    {
        public MinerCharacter CurrentMiner { get; private set; }

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

            inputReader.OnJump += HandleJump;
            base.Start();
        }

        private void OnDestroy()
        {
            if (inputReader != null)
                inputReader.OnJump -= HandleJump;
        }

        private void Update()
        {
            if (CurrentMiner == null) return;
            CurrentMiner.Move(inputReader.Move.x);
        }

        private void HandleJump() => CurrentMiner?.RequestJump();

        protected override void PostProcessControl(ControllableBase c)
        {
            base.PostProcessControl(c);
            CurrentMiner = c as MinerCharacter;
            if (c != null)
                EventBus.Publish(new PlayerControlledEvent(c.transform, GameplayMode.Mine));
        }

        protected override void PostProcessRelease(ControllableBase c)
        {
            base.PostProcessRelease(c);
            CurrentMiner = null;
        }
    }
}