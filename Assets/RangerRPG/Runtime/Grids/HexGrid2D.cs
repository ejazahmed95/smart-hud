using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RangerRPG.Grids {
    public class HexGrid2D<T> : MonoBehaviour where T: HexCellInfo, new() {
        public static readonly double Sqrt3 = Math.Sqrt(3);
        
        [SerializeField] protected Vector2Int gridDimensions;
        [SerializeField] protected CellBehavior<T> cellPrefab;
        [SerializeField] protected GameObject gridParent;
        [SerializeField] protected Vector2 cellSize;

        private List<List<CellBehavior<T>>> _cells;
        private List<List<T>> _cellsInfo;
        private Vector2 _zeroCellPosition; // Position of bottom left cell
        
        protected bool Initialized = false;
        
        public Vector2Int GridDimensions => gridDimensions;
        public List<List<T>> CellsInfo => _cellsInfo;

        public static List<Vector2Int> DirectionVectors = new List<Vector2Int> {
            new(-1, 1), new(0, 1), new(1, 1),
            new(-1, 0), new(1, 0),
            new(-1, -1), new(0, -1), new(1, -1)
        };
        
        protected void Awake() {
            _cells = new List<List<CellBehavior<T>>>();
            _cellsInfo = new List<List<T>>();
        }

        public void InitializeGrid() {
            for (var row = 0; row < gridDimensions.y; row++) {
                List<T> rowCells = new List<T>();
                var offset = IndexToAxial(row, 0).Q;
                for (var col = 0; col < gridDimensions.x; col++) {
                    var cell = new T();
                    cell.Init(new AxialPosition(offset + col, row));
                    rowCells.Add(cell);
                }
                _cellsInfo.Add(rowCells);
            }
            
            
            
            _zeroCellPosition = - (cellSize*gridDimensions)/2 + (cellSize / 2);
            Initialized = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AxialPosition IndexToAxial(int row, int col) {
            return new AxialPosition(col - row / 2, row);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector2Int AxialToIndex(int q, int r) {
            return new Vector2Int(r, q + r / 2);
        }

        public void ForEachCell(Action<T> cbAction) {
            foreach (var rowCells in _cellsInfo) {
                foreach (var t in rowCells) {
                    cbAction(t);
                }
            }
        }
        
        public void CreateView(Transform parent, CellBehavior<T> prefab = null) {
            if (!Initialized) return;
            if (prefab == null) prefab = cellPrefab;
            for (var y = 0; y < gridDimensions.y; y++) {
                var row = new List<CellBehavior<T>>();
                for (var x = 0; x < gridDimensions.x; x++) {
                    CellBehavior<T> cell = Instantiate(prefab, parent).Init(_cellsInfo[y][x]);
                    cell.transform.localPosition = _zeroCellPosition + new Vector2(x * cellSize.x, y * cellSize.y);
                    row.Add(cell);
                }
                _cells.Add(row);
            }
        }

        public bool IsValid(Vector2Int cellIndex) {
            return (cellIndex.x >= 0 && cellIndex.x < gridDimensions.x) && (cellIndex.y >= 0 && cellIndex.y < gridDimensions.y);
        }

        public T GetCell(Vector2Int index) {
            if (!IsValid(index)) return null;
            return _cellsInfo[index.y][index.x];
        }

        public Vector2 GetCellPosition(Vector2Int index) {
            return _zeroCellPosition + index * cellSize;
        }
        public Vector2 GetPosition(AxialPosition coords, float size) {
            return new Vector2((float)(Sqrt3 * coords.Q + (Sqrt3 / 2) * coords.R) * size, (float)(1.5 * -coords.R) * size);
        }
    }
}