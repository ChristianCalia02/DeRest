using UnityEngine;

namespace UI
{
    public class SlowRotate : MonoBehaviour
    {
        [SerializeField] private float degreesPerSecond = 6f;

        private void Update() =>
            transform.Rotate(0f, 0f, degreesPerSecond * Time.unscaledDeltaTime);
    }
}