using System;
using RangerRPG.Core;
using UnityEngine;

namespace RangerRPG.Grids {
    public class GridGenerator<TCell> : MonoBehaviour where TCell: HexCellInfo, new() {
        [SerializeField] private HexGrid<TCell> grid;
        [SerializeField] private CellBehavior<TCell> cellPrefab;
        [SerializeField] private Vector2 offset;
        [SerializeField] private float size = 64;
        [SerializeField] private bool autoSize;
        [SerializeField] private float totalHeight;
        [SerializeField] private float totalWidth;

        private void Awake() {
            var requiredHeight = 1.5f * grid.Config.Height;
            float requiredWidth = (float)Math.Sqrt(3) * grid.Config.Width;
            if (autoSize) {
                size = Math.Min(totalHeight / requiredHeight, totalWidth / requiredWidth);
            }
        }
        
        public void Start() {
            Log.Info("Generating Grid");
            grid.ForEachCell(cell => {
                var cellObject = Instantiate(cellPrefab, transform);
                var pos = grid.GetPosition(cell.Position, size);
                cellObject.Init(cell);
            });
        }
    }
}