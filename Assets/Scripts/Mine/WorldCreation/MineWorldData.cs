using System.Collections.Generic;
using UnityEngine;

namespace Mine
{
    public class BlockInstance
    {
        public BlockDefinition Definition;
        public int CurrentHP;

        public BlockInstance(BlockDefinition definition)
        {
            Definition = definition;
            CurrentHP = definition.maxHP;
        }
    }

    public class MineWorldData
    {
        private readonly Dictionary<Vector2Int, BlockInstance> blocks = new();

        public bool TryGetBlock(Vector2Int cell, out BlockInstance block) =>
            blocks.TryGetValue(cell, out block);

        public void SetBlock(Vector2Int cell, BlockDefinition definition) =>
            blocks[cell] = new BlockInstance(definition);

        public void RemoveBlock(Vector2Int cell) => blocks.Remove(cell);

        public IEnumerable<KeyValuePair<Vector2Int, BlockInstance>> AllBlocks => blocks;
    }
}