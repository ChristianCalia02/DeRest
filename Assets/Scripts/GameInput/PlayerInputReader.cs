using Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameInput
{
    public class PlayerInputReader : MonoBehaviour, IInputReader
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference interact;
        [SerializeField] private InputActionReference pause;
        [SerializeField] private InputActionReference debug;
        [SerializeField] private InputActionReference jump;
        [SerializeField] private InputActionReference dig;

        public Vector2 Move { get; private set; }
        public Vector2 MousePosition { get; private set; }
        public bool InteractPressed => interact.action.WasPressedThisFrame();

        public event Action OnInteract;
        public event Action OnPause;
        public event Action OnDebug;
        public event Action OnJump;
        public bool DigHeld => dig.action.IsPressed();

        private void OnEnable()
        {
            move.action.Enable();
            interact.action.Enable();
            pause.action.Enable();
            debug.action.Enable();
            jump.action.Enable();
            dig.action.Enable();

            interact.action.performed += HandleInteract;
            pause.action.performed += HandlePause;
            debug.action.performed += HandleDebug;
            jump.action.performed += HandleJump;
        }

        private void OnDisable()
        {
            interact.action.performed -= HandleInteract;
            pause.action.performed -= HandlePause;
            debug.action.performed -= HandleDebug;
            jump.action.performed -= HandleJump;
        }

        private void Update()
        {
            Move = move.action.ReadValue<Vector2>();

            if (Mouse.current != null)
                MousePosition = Mouse.current.position.ReadValue();
        }

        private void HandleInteract(InputAction.CallbackContext ctx) => OnInteract?.Invoke();
        private void HandlePause(InputAction.CallbackContext ctx) => OnPause?.Invoke();
        private void HandleDebug(InputAction.CallbackContext ctx) => OnDebug?.Invoke();
        private void HandleJump(InputAction.CallbackContext ctx) => OnJump?.Invoke();
    }
}