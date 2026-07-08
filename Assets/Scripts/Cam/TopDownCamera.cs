using UnityEngine;

namespace CameraSystem
{
    public class TopDownCamera : CameraBase
    {
        [Header("Framing")]
        [Tooltip("Target Offset. Y high = more high, Z negative = littel angle behind the player.")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 12f, -6f);
        [SerializeField] private float followSpeed = 8f;

        protected override void Awake()
        {
            base.Awake();
            transform.rotation = Quaternion.LookRotation(-offset.normalized, Vector3.up);
        }

        protected override void UpdatePosition()
        {
            Vector3 desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
        }

        protected override void OnTargetSet()
        {
            transform.position = target.position + offset;
        }
    }
}