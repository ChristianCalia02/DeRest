using UnityEngine;

namespace Core
{
    public abstract class ControllableBase : MonoBehaviour
    {
        public ControllerBase CurrentController { get; private set; }

        public void OnControlled(ControllerBase controller)
        {
            CurrentController = controller;
            PostProcessControl(controller);
        }

        public void OnReleased(ControllerBase controller)
        {
            CurrentController = null;
            PostProcessRelease(controller);
        }

        protected virtual void PostProcessControl(ControllerBase controller) { }
        protected virtual void PostProcessRelease(ControllerBase controller) { }
    }
}