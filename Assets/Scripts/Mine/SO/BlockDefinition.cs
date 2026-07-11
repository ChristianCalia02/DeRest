using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Core;

namespace Mine
{
    [CreateAssetMenu(menuName = "DeRest/Mine/Block Definition")]
    public class BlockDefinition : ScriptableObject
    {
        public string id;
        public TileBase tile;
        public int maxHP = 10;
        public ToolType requiredTool = ToolType.Pickaxe;
        public List<ResourceDrop> drops = new();
    }

    [System.Serializable]
    public class ResourceDrop
    {
        public string resourceId;
        public int minAmount = 1;
        public int maxAmount = 1;
        [Range(0f, 1f)] public float dropChance = 1f;
    }
}