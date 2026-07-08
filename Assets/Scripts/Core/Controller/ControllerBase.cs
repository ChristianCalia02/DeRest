using UnityEngine;

namespace Core
{
    public abstract class ControllerBase : MonoBehaviour
    {
        [SerializeField] private ControllableBase startControl;

        public ControllableBase CurrentControllable { get; private set; }

        protected virtual void Start()
        {
            if (startControl != null)
                Control(startControl);
        }

        public void Control(ControllableBase controllable)
        {
            if (CurrentControllable != null)
                Release();

            CurrentControllable = controllable;

            if (CurrentControllable != null)
                CurrentControllable.OnControlled(this);

            PostProcessControl(CurrentControllable);
        }

        public void Release()
        {
            var released = CurrentControllable;
            CurrentControllable = null;

            if (released != null)
                released.OnReleased(this);

            PostProcessRelease(released);
        }

        protected virtual void PostProcessControl(ControllableBase c) { }
        protected virtual void PostProcessRelease(ControllableBase c) { }
    }
}