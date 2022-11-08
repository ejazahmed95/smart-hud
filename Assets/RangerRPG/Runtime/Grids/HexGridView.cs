using System.Collections.Generic;
using UnityEngine;

namespace RangerRPG.Grids {

    public class HexGridView<T> where T: HexCellInfo, new() {

        private HexGrid2D<T> _grid;
        private ViewData _config;
        private List<List<CellBehavior<T>>> _cellsInfo = new();

        public HexGridView<T> Init(HexGrid2D<T> grid, ViewData viewData) {
            _grid = grid;
            _config = viewData;
            CreateView();
            return this;
        }
        
        private void CreateView() {
            for (var row = 0; row < _grid.GridDimensions.y; row++) {
                var rowCells = new List<CellBehavior<T>>();
                for (var col = 0; col < _grid.GridDimensions.x; col++) {
                    if (_config.proceduralGeneration) {
                        rowCells.Add(null);
                        continue;
                    }
                    var cInfo = _grid.CellsInfo[row][col];
                    CellBehavior<T> cell = CellBehavior<T>.Instantiate(_config.prefab, _config.parent).Init(cInfo);
                    cell.transform.localPosition = HexUtils.GetPosition(cInfo.Position, _config.cellSize, _config.xzPlane);
                    rowCells.Add(cell);
                }
                _cellsInfo.Add(rowCells);
            }
        }
        
        public void GenerateView(HexCellInfo referenceCell) {
            GetCellView(referenceCell.Position);
            GetCellView(referenceCell.Position.Next(HexDirection.Q));
            GetCellView(referenceCell.Position.Next(HexDirection.QNeg));
            GetCellView(referenceCell.Position.Next(HexDirection.R));
            GetCellView(referenceCell.Position.Next(HexDirection.RNeg));
            GetCellView(referenceCell.Position.Next(HexDirection.S));
            GetCellView(referenceCell.Position.Next(HexDirection.SNeg));
        }

        public CellBehavior<T> GetCellView(AxialPosition pos) {
            var index = HexUtils.AxialToIndex(pos.Q, pos.R);
            if (_grid.IsValid(index) == false) {
                return null;
            }
            return _cellsInfo[index.y][index.x] == null ? CreateCellView(index) : _cellsInfo[index.y][index.x];
        }
        
        private CellBehavior<T> CreateCellView(Vector2Int index) {
            var cInfo = _grid.CellsInfo[index.y][index.x];
            CellBehavior<T> cell = CellBehavior<T>.Instantiate(_config.prefab, _config.parent).Init(cInfo);
            cell.transform.localPosition = HexUtils.GetPosition(cInfo.Position, _config.cellSize, _config.xzPlane);
            _cellsInfo[index.y][index.x] = cell;
            return cell;
        }

        public struct ViewData {
            public Transform parent;
            public CellBehavior<T> prefab;
            public float cellSize;
            public bool proceduralGeneration;
            public bool xzPlane;
        }
        
    }
}