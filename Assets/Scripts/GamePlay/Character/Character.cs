using Core;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : ControllableBase
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float rotationSpeed = 12f;

        private CharacterController controller;
        private Vector3 moveBuffer;

        private void Awake() => controller = GetComponent<CharacterController>();

        private void Update()
        {
            if (!controller.enabled || moveBuffer.sqrMagnitude < 0.0001f)
            {
                moveBuffer = Vector3.zero;
                return;
            }

            controller.Move(moveBuffer * speed * Time.deltaTime);

            Quaternion targetRot = Quaternion.LookRotation(moveBuffer, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

            moveBuffer = Vector3.zero;
        }

        public void Move(Vector3 direction) => moveBuffer = Vector3.ClampMagnitude(direction, 1f);
    }
}