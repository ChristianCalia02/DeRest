using UnityEngine;

namespace CameraSystem
{
    public class MinerCamera : CameraBase
    {
        [Header("Framing")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 1f, -10f);
        [SerializeField] private float followSpeed = 10f;

        protected override void Awake()
        {
            base.Awake();
            if (Cam != null) Cam.orthographic = true;
        }

        protected override void UpdatePosition()
        {
            Vector3 desired = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desired, followSpeed * Time.deltaTime);
        }

        protected override void OnTargetSet() => transform.position = target.position + offset;
    }
}