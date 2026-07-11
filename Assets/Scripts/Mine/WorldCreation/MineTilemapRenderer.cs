using UnityEngine;
using UnityEngine.Tilemaps;

namespace Mine
{
    [RequireComponent(typeof(Tilemap))]
    public class MineTilemapRenderer : MonoBehaviour
    {
        private Tilemap tilemap;

        private void Awake() => tilemap = GetComponent<Tilemap>();

        public void DrawAll(MineWorldData world)
        {
            foreach (var kvp in world.AllBlocks)
                tilemap.SetTile(new Vector3Int(kvp.Key.x, kvp.Key.y, 0), kvp.Value.Definition.tile);
        }

        public void ClearCell(Vector2Int cell) =>
            tilemap.SetTile(new Vector3Int(cell.x, cell.y, 0), null);
    }
}