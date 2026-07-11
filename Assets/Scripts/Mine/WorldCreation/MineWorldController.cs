using UnityEngine;

namespace Mine
{
    public class MineWorldController : MonoBehaviour
    {
        [SerializeField] private MineWorldGenerator generator;
        [SerializeField] private MineTilemapRenderer tilemapRenderer;

        public MineWorldData World { get; private set; }

        private void Start()
        {
            World = generator.Generate();
            tilemapRenderer.DrawAll(World);
        }

        public void DamageBlock(Vector2Int cell, int damage)
        {
            if (!World.TryGetBlock(cell, out var block)) return;

            block.CurrentHP -= damage;
            if (block.CurrentHP <= 0)
            {
                World.RemoveBlock(cell);
                tilemapRenderer.ClearCell(cell);
                // TODO: Event for drop resources when the inventory is done
            }
        }
    }
}