using UnityEngine;

namespace Mine
{
    public class MineWorldGenerator : MonoBehaviour
    {
        [SerializeField] private BlockDefinition dirtBlock;
        [SerializeField] private BlockDefinition stoneBlock;
        [Range(0f, 1f)][SerializeField] private float stoneChance = 0.25f;
        [SerializeField] private int width = 40;
        [SerializeField] private int depth = 30;

        public MineWorldData Generate()
        {
            var world = new MineWorldData();

            for (int x = -width / 2; x < width / 2; x++)
            {
                for (int y = 0; y > -depth; y--)
                {
                    var definition = Random.value < stoneChance ? stoneBlock : dirtBlock;
                    world.SetBlock(new Vector2Int(x, y), definition);
                }
            }

            return world;
        }
    }
}