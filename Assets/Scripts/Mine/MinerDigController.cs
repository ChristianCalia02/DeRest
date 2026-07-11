using UnityEngine;
using Core;

namespace Mine
{
    public class MinerDigController : MonoBehaviour
    {
        [SerializeField] private MineWorldController worldController;
        [SerializeField] private Camera minerCamera;
        [SerializeField] private int digDamage = 5;
        [SerializeField] private float digCooldown = 0.15f;
        [SerializeField] private float maxDigDistance = 5f;

        private IInputReader inputReader;
        private float cooldownTimer;

        private void Start() =>
            inputReader = GetComponent<IInputReader>() ?? GetComponentInChildren<IInputReader>();

        private void Update()
        {
            if (inputReader == null) return;

            cooldownTimer -= Time.deltaTime;
            if (!inputReader.DigHeld || cooldownTimer > 0f) return;

            float distanceFromCamera = -minerCamera.transform.position.z; 
            Vector3 mouseScreen = new Vector3(inputReader.MousePosition.x, inputReader.MousePosition.y, distanceFromCamera);
            Vector3 worldPos = minerCamera.ScreenToWorldPoint(mouseScreen);

            if (Vector2.Distance(transform.position, worldPos) > maxDigDistance) return;

            var cell = new Vector2Int(Mathf.FloorToInt(worldPos.x), Mathf.FloorToInt(worldPos.y));
            worldController.DamageBlock(cell, digDamage);
            cooldownTimer = digCooldown;
        }
    }
}